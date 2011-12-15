using System;
using CandyDirect.AxIntegrator;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;
using NUnit.Framework;
using System.Collections.Generic;
using Shouldly;
using System.Xml.Serialization;
using CandyDirect.AppServices;

namespace Tests
{
	[TestFixture]
	public class AmazonOrderTests
	{
		
	   MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
        String accessKeyId = "AKIAIZAIBEDEFFEWL3OA";
        String secretAccessKey = "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";
        String merchantId = "A3FFNVBB2U6OUI";
        String marketplaceId = "ATVPDKIKX0DER";
        const string applicationName = "CandyDirect.AxIntegrator";
        const string applicationVersion = "1.0";
            
		[Test]
		public void ShouldGetNewOrders()
		{
			config.ServiceURL = "https://mws.amazonservices.com/Orders/2011-01-01";
			MarketplaceWebServiceOrders.MarketplaceWebServiceOrders service = new MarketplaceWebServiceOrdersClient(
                applicationName, applicationVersion, accessKeyId, secretAccessKey, config);
			 
			ListOrdersRequest request = new ListOrdersRequest();
            request.CreatedAfter = DateTime.Now.AddDays(-1);
            request.MarketplaceId = new MarketplaceIdList();
            request.MarketplaceId.Id = new List<string>(new string[] { marketplaceId });
            request.SellerId = merchantId;
            ListOrdersSample.InvokeListOrders(service, request);
            
			var store = new AmazonStore();
			var results = store.GetNewOrders();
			results.Count.ShouldBeGreaterThan(1);
		}
	}




    /// <summary>
    /// List Orders  Samples
    /// </summary>
    public class ListOrdersSample
    {
    
                                         
        /// <summary>
        /// ListOrders can be used to find orders that meet the specified criteria.
        /// 
        /// </summary>
        /// <param name="service">Instance of MarketplaceWebServiceOrders service</param>
        /// <param name="request">ListOrdersRequest request</param>
        public static void InvokeListOrders(MarketplaceWebServiceOrders.MarketplaceWebServiceOrders service, ListOrdersRequest request)
        {
            try 
            {
                ListOrdersResponse response = service.ListOrders(request);
                
                
                NLog.LogManager.GetCurrentClassLogger().Info ("Service Response");
                NLog.LogManager.GetCurrentClassLogger().Info ("=============================================================================");
                NLog.LogManager.GetCurrentClassLogger().Info ("");

                Console.WriteLine("        ListOrdersResponse");
                if (response.IsSetListOrdersResult())
                {
                    NLog.LogManager.GetCurrentClassLogger().Info("            ListOrdersResult");
                    ListOrdersResult  listOrdersResult = response.ListOrdersResult;
                    if (listOrdersResult.IsSetNextToken())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info("                NextToken");
                        NLog.LogManager.GetCurrentClassLogger().Info("                    {0}", listOrdersResult.NextToken);
                    }
                    if (listOrdersResult.IsSetCreatedBefore())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info("                CreatedBefore");
                        NLog.LogManager.GetCurrentClassLogger().Info("                    {0}", listOrdersResult.CreatedBefore);
                    }
                    if (listOrdersResult.IsSetLastUpdatedBefore())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info("                LastUpdatedBefore");
                        NLog.LogManager.GetCurrentClassLogger().Info("                    {0}", listOrdersResult.LastUpdatedBefore);
                    }
                    if (listOrdersResult.IsSetOrders())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info("                Orders");
                        OrderList  orders = listOrdersResult.Orders;
                        List<Order> orderList = orders.Order;
                        foreach (Order order in orderList)
                        {
                            NLog.LogManager.GetCurrentClassLogger().Info("                    Order");
                            if (order.IsSetAmazonOrderId())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        AmazonOrderId");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.AmazonOrderId);
                            }
                            if (order.IsSetSellerOrderId())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        SellerOrderId");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.SellerOrderId);
                            }
                            if (order.IsSetPurchaseDate())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        PurchaseDate");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.PurchaseDate);
                            }
                            if (order.IsSetLastUpdateDate())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        LastUpdateDate");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.LastUpdateDate);
                            }
                            if (order.IsSetOrderStatus())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        OrderStatus");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.OrderStatus);
                            }
                            if (order.IsSetFulfillmentChannel())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        FulfillmentChannel");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.FulfillmentChannel);
                            }
                            if (order.IsSetSalesChannel())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        SalesChannel");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.SalesChannel);
                            }
                            if (order.IsSetOrderChannel())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        OrderChannel");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.OrderChannel);
                            }
                            if (order.IsSetShipServiceLevel())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        ShipServiceLevel");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.ShipServiceLevel);
                            }
                            if (order.IsSetShippingAddress())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        ShippingAddress");
                                Address  shippingAddress = order.ShippingAddress;
                                if (shippingAddress.IsSetName())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            Name");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.Name);
                                }
                                if (shippingAddress.IsSetAddressLine1())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            AddressLine1");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.AddressLine1);
                                }
                                if (shippingAddress.IsSetAddressLine2())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            AddressLine2");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.AddressLine2);
                                }
                                if (shippingAddress.IsSetAddressLine3())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            AddressLine3");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.AddressLine3);
                                }
                                if (shippingAddress.IsSetCity())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            City");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.City);
                                }
                                if (shippingAddress.IsSetCounty())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            County");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.County);
                                }
                                if (shippingAddress.IsSetDistrict())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            District");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.District);
                                }
                                if (shippingAddress.IsSetStateOrRegion())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            StateOrRegion");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.StateOrRegion);
                                }
                                if (shippingAddress.IsSetPostalCode())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            PostalCode");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.PostalCode);
                                }
                                if (shippingAddress.IsSetCountryCode())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            CountryCode");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.CountryCode);
                                }
                                if (shippingAddress.IsSetPhone())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            Phone");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", shippingAddress.Phone);
                                }
                            }
                            if (order.IsSetOrderTotal())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        OrderTotal");
                                Money  orderTotal = order.OrderTotal;
                                if (orderTotal.IsSetCurrencyCode())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            CurrencyCode");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", orderTotal.CurrencyCode);
                                }
                                if (orderTotal.IsSetAmount())
                                {
                                    NLog.LogManager.GetCurrentClassLogger().Info("                            Amount");
                                    NLog.LogManager.GetCurrentClassLogger().Info("                                {0}", orderTotal.Amount);
                                }
                            }
                            if (order.IsSetNumberOfItemsShipped())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        NumberOfItemsShipped");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.NumberOfItemsShipped);
                            }
                            if (order.IsSetNumberOfItemsUnshipped())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        NumberOfItemsUnshipped");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.NumberOfItemsUnshipped);
                            }
                            if (order.IsSetMarketplaceId())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        MarketplaceId");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.MarketplaceId);
                            }
                            if (order.IsSetBuyerEmail())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        BuyerEmail");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.BuyerEmail);
                            }
                            if (order.IsSetBuyerName())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        BuyerName");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.BuyerName);
                            }
                            if (order.IsSetShipmentServiceLevelCategory())
                            {
                                NLog.LogManager.GetCurrentClassLogger().Info("                        ShipmentServiceLevelCategory");
                                NLog.LogManager.GetCurrentClassLogger().Info("                            {0}", order.ShipmentServiceLevelCategory);
                            }
                        }
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    NLog.LogManager.GetCurrentClassLogger().Info("            ResponseMetadata");
                    ResponseMetadata  responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info("                RequestId");
                        NLog.LogManager.GetCurrentClassLogger().Info("                    {0}", responseMetadata.RequestId);
                    }
                }

            } 
            catch (MarketplaceWebServiceOrdersException ex) 
            {
                NLog.LogManager.GetCurrentClassLogger().Info("Caught Exception: " + ex.Message);
                NLog.LogManager.GetCurrentClassLogger().Info("Response Status Code: " + ex.StatusCode);
                NLog.LogManager.GetCurrentClassLogger().Info("Error Code: " + ex.ErrorCode);
                NLog.LogManager.GetCurrentClassLogger().Info("Error Type: " + ex.ErrorType);
                NLog.LogManager.GetCurrentClassLogger().Info("Request ID: " + ex.RequestId);
                NLog.LogManager.GetCurrentClassLogger().Info("XML: " + ex.XML);
            }
        }
            
}

}
