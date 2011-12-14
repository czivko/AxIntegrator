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
using System.Threading;

using MarketplaceWebServiceOrders.Model;

namespace MarketplaceWebServiceOrders
{
    public class FetchNewOrdersJob
    {
        private volatile bool isRunning;
        private OrderFetcher orderFetcher;

        private TimeSpan _checkOrdersInterval = TimeSpan.FromMinutes(15.0);
        /// <summary>
        /// Gets or sets the order check interval.  Defaults to 15 minutes.
        /// </summary>
        public TimeSpan CheckOrdersInterval
        {
            get { return _checkOrdersInterval; }
            set { _checkOrdersInterval = value; }
        }

        /// <summary>
        /// Internal method to handle an order.
        /// </summary>
        protected virtual void HandleOrder(Order order)
        {
            Console.WriteLine("Processing Order:");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(order.ToString());
            // Fetch the order items in each order
            orderFetcher.FetchOrderItems(order.AmazonOrderId, delegate(OrderItem item)
            {
                Console.WriteLine("\tProcessing Order Item");
                Console.WriteLine("\t---------------------------------------------------");                // Process order item here.
                Console.WriteLine("\t" + item.ToString().Replace("\n", "\n\t"));
            });

            Console.WriteLine("=================================================");
            Console.WriteLine();
        }

        /// <summary>
        /// Method to continuously check orders over an interval, and list OrderItems for those Orders.
        /// </summary>
        private void OrdersJobThread(object obj)
        {
            orderFetcher.ProcessOrder += HandleOrder;

            if (this.CheckOrdersInterval == TimeSpan.MinValue)
            {
                throw new ArgumentException("The CheckOrdersInterval TimeSpan cannot be zero.", "CheckOrdersInterval");
            }

            DateTime startCheckInterval = DateTime.Now.Subtract(CheckOrdersInterval);

            // Continue forever until the isRunning flag is cleared.
            while (isRunning)
            {
                try
                {
                    // Check the orders for this interval.
                    DateTime checkInterval = startCheckInterval;
                    startCheckInterval = DateTime.Now.Subtract(TimeSpan.FromMinutes(3.0));

                    Console.WriteLine("Fetching orders from " + checkInterval.ToString() + " to " + startCheckInterval.ToString());
                    orderFetcher.FetchOrders(checkInterval, startCheckInterval);

                    // Wait for the next interval.
                    Console.WriteLine("Fetch complete.  Sleeping until next interval.");
                    while (isRunning && DateTime.Now.Subtract(startCheckInterval) < CheckOrdersInterval)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch(Exception err)
                {
                    Console.WriteLine("Error: " + err.Message + ".  Orders job thread is exiting.");
                    isRunning = false;
                }
            }
        }

        /// <summary>
        /// Sample code to invoke the OrderFetcher.
        /// </summary>
        /// <param name="service">MarketplaceWebServiceOrders object.</param>
        /// <param name="sellerId">The seller Id.</param>
        /// <param name="marketplaceIdList">List of marketplaces passed in to the GetOrders call.</param>
        public static void InvokeOrderFetcherSample(MarketplaceWebServiceOrders service, string sellerId, string [] marketplaceIdList)
        {
            // Create a FetchOrderUpdates job with the default time span.
            FetchNewOrdersJob job = new FetchNewOrdersJob();
            job.isRunning = true;
            job.orderFetcher = new OrderFetcher(service, sellerId, marketplaceIdList);

            Thread jobThread = new Thread(job.OrdersJobThread);
            jobThread.IsBackground = true;
            jobThread.Start();

            // Pause on the main thread for one hour or until the thread exits, then end the job.
            jobThread.Join(1000 * 60 * 60);
            job.isRunning = false;

            // Block until the thread terminates to prevent any requests in progress from being aborted.
            while (jobThread.IsAlive)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
