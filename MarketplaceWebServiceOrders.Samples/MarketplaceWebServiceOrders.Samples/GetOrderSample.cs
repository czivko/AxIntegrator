/******************************************************************************* 
 *  Copyright 2008-2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"); 
 *  
 *  You may not use this file except in compliance with the License. 
 *  You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 *  specific language governing permissions and limitations under the License.
 * ***************************************************************************** 
 *    __  _    _  ___ 
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 * 
 *  Marketplace Web Service Orders CSharp Library
 *  API Version: 2011-01-01
 *  Generated: Thu Sep 15 00:40:21 GMT 2011 
 * 
 */

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;



namespace MarketplaceWebServiceOrders.Samples
{

    /// <summary>
    /// Get Order  Samples
    /// </summary>
    public class GetOrderSample
    {
    
                                 
        /// <summary>
        /// This operation takes up to 50 order ids and returns the corresponding orders.
        /// 
        /// </summary>
        /// <param name="service">Instance of MarketplaceWebServiceOrders service</param>
        /// <param name="request">GetOrderRequest request</param>
        public static void InvokeGetOrder(MarketplaceWebServiceOrders service, GetOrderRequest request)
        {
            try 
            {
                GetOrderResponse response = service.GetOrder(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        GetOrderResponse");
                if (response.IsSetGetOrderResult())
                {
                    Console.WriteLine("            GetOrderResult");
                    GetOrderResult  getOrderResult = response.GetOrderResult;
                    if (getOrderResult.IsSetOrders())
                    {
                        Console.WriteLine("                Orders");
                        OrderList  orders = getOrderResult.Orders;
                        List<Order> orderList = orders.Order;
                        foreach (Order order in orderList)
                        {
                            Console.WriteLine("                    Order");
                            if (order.IsSetAmazonOrderId())
                            {
                                Console.WriteLine("                        AmazonOrderId");
                                Console.WriteLine("                            {0}", order.AmazonOrderId);
                            }
                            if (order.IsSetSellerOrderId())
                            {
                                Console.WriteLine("                        SellerOrderId");
                                Console.WriteLine("                            {0}", order.SellerOrderId);
                            }
                            if (order.IsSetPurchaseDate())
                            {
                                Console.WriteLine("                        PurchaseDate");
                                Console.WriteLine("                            {0}", order.PurchaseDate);
                            }
                            if (order.IsSetLastUpdateDate())
                            {
                                Console.WriteLine("                        LastUpdateDate");
                                Console.WriteLine("                            {0}", order.LastUpdateDate);
                            }
                            if (order.IsSetOrderStatus())
                            {
                                Console.WriteLine("                        OrderStatus");
                                Console.WriteLine("                            {0}", order.OrderStatus);
                            }
                            if (order.IsSetFulfillmentChannel())
                            {
                                Console.WriteLine("                        FulfillmentChannel");
                                Console.WriteLine("                            {0}", order.FulfillmentChannel);
                            }
                            if (order.IsSetSalesChannel())
                            {
                                Console.WriteLine("                        SalesChannel");
                                Console.WriteLine("                            {0}", order.SalesChannel);
                            }
                            if (order.IsSetOrderChannel())
                            {
                                Console.WriteLine("                        OrderChannel");
                                Console.WriteLine("                            {0}", order.OrderChannel);
                            }
                            if (order.IsSetShipServiceLevel())
                            {
                                Console.WriteLine("                        ShipServiceLevel");
                                Console.WriteLine("                            {0}", order.ShipServiceLevel);
                            }
                            if (order.IsSetShippingAddress())
                            {
                                Console.WriteLine("                        ShippingAddress");
                                Address  shippingAddress = order.ShippingAddress;
                                if (shippingAddress.IsSetName())
                                {
                                    Console.WriteLine("                            Name");
                                    Console.WriteLine("                                {0}", shippingAddress.Name);
                                }
                                if (shippingAddress.IsSetAddressLine1())
                                {
                                    Console.WriteLine("                            AddressLine1");
                                    Console.WriteLine("                                {0}", shippingAddress.AddressLine1);
                                }
                                if (shippingAddress.IsSetAddressLine2())
                                {
                                    Console.WriteLine("                            AddressLine2");
                                    Console.WriteLine("                                {0}", shippingAddress.AddressLine2);
                                }
                                if (shippingAddress.IsSetAddressLine3())
                                {
                                    Console.WriteLine("                            AddressLine3");
                                    Console.WriteLine("                                {0}", shippingAddress.AddressLine3);
                                }
                                if (shippingAddress.IsSetCity())
                                {
                                    Console.WriteLine("                            City");
                                    Console.WriteLine("                                {0}", shippingAddress.City);
                                }
                                if (shippingAddress.IsSetCounty())
                                {
                                    Console.WriteLine("                            County");
                                    Console.WriteLine("                                {0}", shippingAddress.County);
                                }
                                if (shippingAddress.IsSetDistrict())
                                {
                                    Console.WriteLine("                            District");
                                    Console.WriteLine("                                {0}", shippingAddress.District);
                                }
                                if (shippingAddress.IsSetStateOrRegion())
                                {
                                    Console.WriteLine("                            StateOrRegion");
                                    Console.WriteLine("                                {0}", shippingAddress.StateOrRegion);
                                }
                                if (shippingAddress.IsSetPostalCode())
                                {
                                    Console.WriteLine("                            PostalCode");
                                    Console.WriteLine("                                {0}", shippingAddress.PostalCode);
                                }
                                if (shippingAddress.IsSetCountryCode())
                                {
                                    Console.WriteLine("                            CountryCode");
                                    Console.WriteLine("                                {0}", shippingAddress.CountryCode);
                                }
                                if (shippingAddress.IsSetPhone())
                                {
                                    Console.WriteLine("                            Phone");
                                    Console.WriteLine("                                {0}", shippingAddress.Phone);
                                }
                            }
                            if (order.IsSetOrderTotal())
                            {
                                Console.WriteLine("                        OrderTotal");
                                Money  orderTotal = order.OrderTotal;
                                if (orderTotal.IsSetCurrencyCode())
                                {
                                    Console.WriteLine("                            CurrencyCode");
                                    Console.WriteLine("                                {0}", orderTotal.CurrencyCode);
                                }
                                if (orderTotal.IsSetAmount())
                                {
                                    Console.WriteLine("                            Amount");
                                    Console.WriteLine("                                {0}", orderTotal.Amount);
                                }
                            }
                            if (order.IsSetNumberOfItemsShipped())
                            {
                                Console.WriteLine("                        NumberOfItemsShipped");
                                Console.WriteLine("                            {0}", order.NumberOfItemsShipped);
                            }
                            if (order.IsSetNumberOfItemsUnshipped())
                            {
                                Console.WriteLine("                        NumberOfItemsUnshipped");
                                Console.WriteLine("                            {0}", order.NumberOfItemsUnshipped);
                            }
                            if (order.IsSetMarketplaceId())
                            {
                                Console.WriteLine("                        MarketplaceId");
                                Console.WriteLine("                            {0}", order.MarketplaceId);
                            }
                            if (order.IsSetBuyerEmail())
                            {
                                Console.WriteLine("                        BuyerEmail");
                                Console.WriteLine("                            {0}", order.BuyerEmail);
                            }
                            if (order.IsSetBuyerName())
                            {
                                Console.WriteLine("                        BuyerName");
                                Console.WriteLine("                            {0}", order.BuyerName);
                            }
                            if (order.IsSetShipmentServiceLevelCategory())
                            {
                                Console.WriteLine("                        ShipmentServiceLevelCategory");
                                Console.WriteLine("                            {0}", order.ShipmentServiceLevelCategory);
                            }
                        }
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata  responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

            } 
            catch (MarketplaceWebServiceOrdersException ex) 
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
            }
        }
                    }
}
