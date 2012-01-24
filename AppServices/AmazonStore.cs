using System;
using System.Collections.Generic;
using System.Linq;

using CandyDirect.AppServices.DB;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Fetcher;
using MarketplaceWebServiceOrders.Model;

namespace CandyDirect.AppServices
{
	public class AmazonStore
	{
		MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
        String accessKeyId = "AKIAIZAIBEDEFFEWL3OA";
        String secretAccessKey = "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";
        String merchantId = "A3FFNVBB2U6OUI";
        String marketplaceId = "ATVPDKIKX0DER";
        const string applicationName = "CandyDirect.AxIntegrator";
        const string applicationVersion = "1.0";
        MarketplaceWebServiceOrders.MarketplaceWebServiceOrders service;
        
		public AmazonStore()
		{
			config.ServiceURL = "https://mws.amazonservices.com/Orders/2011-01-01";
			service = new MarketplaceWebServiceOrdersClient(
                applicationName, applicationVersion, accessKeyId, secretAccessKey, config);
		}
		
		public List<SalesOrder> GetNewOrders()
		{
            var orders = new List<SalesOrder>();
			/* looks like just getting the date last updated is find since it gets that date on create to right away
			foreach(var amazonOrder in GetNewAmazonOrdersViaFecther())
			{
				orders.Add(MapOrderFromStore(amazonOrder));
			}
			*/
			foreach(var amazonOrder in GetUpdatedAmazonOrdersViaFecther())
			{
				orders.Add(MapOrderFromStore(amazonOrder));
			}
			
			
			return orders;
		}
		
		public List<SalesOrder> GetUnprocessedOrders()
		{
			var salesOrder = new List<SalesOrder>();
			var table = new AmazonOrders();
			var unprocessedOrders = table.Query(@"select ao.* from CandyDirectAmazonOrders ao 
											left join CandyDirectProcessedOrders po on ao.ordernumber = po.ordernumber 
											where po.id is null 
											and ao.StoreStatus <> 'Canceled' and ao.StoreStatus <> 'Pending'");
			unprocessedOrders.ToList().ForEach(x => salesOrder.Add(MapFromAmazonCache(x)));
			salesOrder.ForEach( x => GetOrderItems(x));
			return salesOrder;
		}
		
		public void GetOrderItems(SalesOrder salesOrder)
		{
			OrderFetcher fetcher = new OrderFetcher(service, merchantId, new string[] { marketplaceId });
			var orderService = new OrderService();
            fetcher.FetchOrderItems(salesOrder.NativeId, delegate(OrderItem item)
            {
                NLog.LogManager.GetCurrentClassLogger().Info(item.ToString());
                
                salesOrder.AddLineItem(item.SellerSKU,item.Title,item.QuantityOrdered,decimal.Parse(item.ItemPrice.Amount),
                                       item.QuantityOrdered * decimal.Parse(item.ItemPrice.Amount),
                                       orderService.GetItemSalesUoM(item.SellerSKU));
            });           
		}
		
		public SalesOrder MapFromAmazonCache(dynamic amazonOrder)
		{
			var order = new SalesOrder();
			order.OrderId = amazonOrder.OrderNumber;
			order.NativeId = amazonOrder.StoreEntityId;
			order.StoreStatus = amazonOrder.StoreStatus;
			order.StoreCreatedAt = amazonOrder.StoreCreatedAt;
			order.StoreUpdatedAt = amazonOrder.StoreUpdatedAt;
			order.CustomerName = amazonOrder.CustomerName;
			order.Street =amazonOrder.ShipStreet;
			order.City = amazonOrder.ShipCity;
			order.State = amazonOrder.ShipState;
			order.Zip = amazonOrder.ShipZip;
			order.Country = amazonOrder.ShipCountry;
			
			return order;
		}
		public SalesOrder MapOrderFromStore(Order amazonOrder)
		{
			var order = new SalesOrder();
			order.OrderId = amazonOrder.AmazonOrderId;
			order.NativeId = amazonOrder.AmazonOrderId;
			order.StoreStatus = amazonOrder.OrderStatus.ToString();
			order.StoreCreatedAt = amazonOrder.PurchaseDate;
			order.StoreUpdatedAt = amazonOrder.LastUpdateDate;
	
			if(amazonOrder.IsSetShippingAddress())
			{
				order.CustomerName = amazonOrder.ShippingAddress.Name;
				order.Street = amazonOrder.ShippingAddress.AddressLine1;
				if(!String.IsNullOrWhiteSpace(amazonOrder.ShippingAddress.AddressLine2))
				   order.Street += System.Environment.NewLine + amazonOrder.ShippingAddress.AddressLine2;
				if(!String.IsNullOrWhiteSpace(amazonOrder.ShippingAddress.AddressLine3))
					order.Street += System.Environment.NewLine + amazonOrder.ShippingAddress.AddressLine3;
				order.City = amazonOrder.ShippingAddress.City;
				order.State = amazonOrder.ShippingAddress.StateOrRegion;
				order.Zip = amazonOrder.ShippingAddress.PostalCode;
				order.Country = amazonOrder.ShippingAddress.CountryCode;
			}
			
			return order;
		}
		
		public List<Order> GetNewAmazonOrdersViaFecther()
		{
			List<Order> orders = new List<Order>();
            GetFecther(orders).FetchNewOrders(DateTime.Now.Subtract(TimeSpan.FromDays(1)));
			return orders;
		}
		
		public List<Order> GetUpdatedAmazonOrdersViaFecther()
		{
			List<Order> orders = new List<Order>();
            GetFecther(orders).FetchUpatedOrders(DateTime.Now.Subtract(TimeSpan.FromDays(1)));
			return orders;
		}
		
		public OrderFetcher GetFecther(List<Order> orders)
		{
			OrderFetcher fetcher = new OrderFetcher(service, merchantId, new string[] { marketplaceId });
			fetcher.ProcessOrder += delegate(Order order)
            {
            	orders.Add(order);
                NLog.LogManager.GetCurrentClassLogger().Info(order.ToString());                
            };
			
			return fetcher;

		}
		/* can delete the fecther replaced this
		public List<Order> GetNewAmazonOrders()
		{ 
			
			 
			ListOrdersByNextTokenRequest tokenRequest = new ListOrdersByNextTokenRequest();
			 
			ListOrdersRequest request = new ListOrdersRequest(); 
			var mr = new MaxResults();
			mr.Value = 100;
			request.MaxResultsPerPage = mr;
            request.CreatedAfter = DateTime.Now.AddDays(-17);
            request.MarketplaceId = new MarketplaceIdList();
            request.MarketplaceId.Id = new List<string>(new string[] { marketplaceId });
            request.SellerId = merchantId;
			
			ListOrdersResponse response = service.ListOrders(request);
			if(response.IsSetListOrdersResult())
			{
				ListOrdersResult  listOrdersResult = response.ListOrdersResult;
			    
				var nextToken = listOrdersResult.NextToken;
				NLog.LogManager.GetCurrentClassLogger().Info(" listOrdersResult.NextToken => {0}", nextToken);
				List<Order> orderList = listOrdersResult.Orders.Order;
				if(!String.IsNullOrWhiteSpace(nextToken))
				   GetNextTokenOrders(nextToken, orderList);
				return orderList;
			}
			
			return null;
		}
		
		public void GetNextTokenOrders(string nextToken, List<Order> orderList)
		{
			ListOrdersByNextTokenRequest tokenRequest = new ListOrdersByNextTokenRequest();
			tokenRequest.NextToken = nextToken;
			tokenRequest.SellerId = merchantId;
			ListOrdersByNextTokenResponse response = service.ListOrdersByNextToken(tokenRequest);
			orderList.AddRange(response.ListOrdersByNextTokenResult.Orders.Order);
			NLog.LogManager.GetCurrentClassLogger().Info(" Totalorders => {0}  :: GetNextTokenOrders.NextToken => {1}  ",
			                                             orderList.Count, response.ListOrdersByNextTokenResult.NextToken);
			if(!String.IsNullOrWhiteSpace(response.ListOrdersByNextTokenResult.NextToken))
				   GetNextTokenOrders(nextToken, orderList);
			//return orderList;
		}
		*/
		public bool UpdateOrderAsShipped(string orderId)
		{
			return true;
		}
		
		public bool UpdateProductInventory(string sku, int available)
		{
			return true;
		}
		
		public bool CreateNewProduct()
		{
			return true;
		}
		
		
	}
	
	public static class OrderExtensions
	{
		public static string GetStringValue(Enum num)
		{
			
			return  "";
		}
	}
}
