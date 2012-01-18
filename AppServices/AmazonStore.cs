using System;
using System.Collections.Generic;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;

namespace CandyDirect.AppServices
{
	/// <summary>
	/// Description of AmazonStore.
	/// </summary>
	public class AmazonStore
	{
		MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
        String accessKeyId = "AKIAIZAIBEDEFFEWL3OA";
        String secretAccessKey = "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";
        String merchantId = "A3FFNVBB2U6OUI";
        String marketplaceId = "ATVPDKIKX0DER";
        const string applicationName = "CandyDirect.AxIntegrator";
        const string applicationVersion = "1.0";
        
		public AmazonStore()
		{
		}
		
		public List<SalesOrder> GetNewOrders()
		{
            var orders = new List<SalesOrder>();
			foreach(var amazonOrder in GetNewAmazonOrders())
			{
				orders.Add(MapOrderFromStore(amazonOrder));//GetAmazonOrderDetails(magentoOrder.increment_id)));
			}
			
			return orders;
		}
		
		public SalesOrder MapOrderFromStore(Order amazonOrder)
		{
			var order = new SalesOrder();
			order.OrderId = amazonOrder.SellerOrderId;
			order.NativeId = amazonOrder.AmazonOrderId;
			if(amazonOrder.IsSetShippingAddress())
			{
			order.CustomerName = amazonOrder.ShippingAddress.Name;
			order.Street =amazonOrder.ShippingAddress.AddressLine1 + " " + amazonOrder.ShippingAddress.AddressLine2 + " " + amazonOrder.ShippingAddress.AddressLine3;
			order.City = amazonOrder.ShippingAddress.City;
			order.State = amazonOrder.ShippingAddress.StateOrRegion;
			order.Zip = amazonOrder.ShippingAddress.PostalCode;
			order.Country = amazonOrder.ShippingAddress.CountryCode;
			}
			else
				order.Street = "na";
			/*
			foreach(var line in magentoOrder.items)
			{
				order.AddLineItem(line.sku, line.name, Decimal.Parse(line.qty_ordered),
				                  Decimal.Parse(line.price),Decimal.Parse(line.row_total), "");
			}
			*/
			
			return order;
		}
		
		public List<Order> GetNewAmazonOrders()
		{ 
			config.ServiceURL = "https://mws.amazonservices.com/Orders/2011-01-01";
			MarketplaceWebServiceOrders.MarketplaceWebServiceOrders service = new MarketplaceWebServiceOrdersClient(
                applicationName, applicationVersion, accessKeyId, secretAccessKey, config);
			 
			ListOrdersRequest request = new ListOrdersRequest();
            request.CreatedAfter = DateTime.Now.AddDays(-1);
            request.MarketplaceId = new MarketplaceIdList();
            request.MarketplaceId.Id = new List<string>(new string[] { marketplaceId });
            request.SellerId = merchantId;
			
			ListOrdersResponse response = service.ListOrders(request);
			if(response.IsSetListOrdersResult())
			{
				ListOrdersResult  listOrdersResult = response.ListOrdersResult;
				var nextToken = listOrdersResult.NextToken;
				return listOrdersResult.Orders.Order;
			}
			
			return null;
		}
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
}
