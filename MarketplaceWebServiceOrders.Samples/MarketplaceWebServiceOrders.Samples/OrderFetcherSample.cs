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
 *
 *  Marketplace Web Service Orders CSharp Library
 *  API Version: 2011-01-01
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

using MarketplaceWebServiceOrders.Model;

namespace MarketplaceWebServiceOrders
{
    public class OrderFetcherSample
    {
        /// <summary>
        /// Sample code to invoke the OrderFetcher to retrieve Orders and OrderItems.
        /// </summary>
        /// <param name="service">MarketplaceWebServiceOrders object.</param>
        public static void InvokeOrderFetcherSample(MarketplaceWebServiceOrders service, string sellerId, string[] marketplaceIdList)
        {
            OrderFetcher fetcher = new OrderFetcher(service, sellerId, marketplaceIdList);

            // Process each order as it comes in
            fetcher.ProcessOrder += delegate(Order order)
            {
                Console.WriteLine(order.ToString());
                // Fetch the order items in each order
                fetcher.FetchOrderItems(order.AmazonOrderId, delegate(OrderItem item)
                {
                    // Process order item here.
                    Console.WriteLine("\t" + item.ToString());
                });

                Console.WriteLine("=================================================");
                Console.WriteLine();
            };

            // Fetch all orders from 1 day ago
            fetcher.FetchOrders(DateTime.Now.Subtract(TimeSpan.FromDays(1)));
        }
    }
}
