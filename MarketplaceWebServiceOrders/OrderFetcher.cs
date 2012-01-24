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

namespace MarketplaceWebServiceOrders.Fetcher
{    
	public enum OrderDateType
	{
		New = 1,
		Updated
	}
    /// <summary>
    /// Sample helper class to Fetch Orders and OrderItems using the Amazon MWS Orders API.
    /// </summary>
    public class OrderFetcher
    {
        public delegate void RetriableMethodCall();
        public delegate void ProcessOrderHandler(Order order);
        public delegate void ProcessOrderItemHandler(OrderItem orderItem);

        /// <summary>
        /// Default amount of time, in milliseconds, to sleep if a request is throttled; default to 1 per 10 minutes.
        /// </summary>
        public const int DEFAULT_THROTTLED_WAIT_TIMEOUT = 1 * 60 * 1000;

        /// <summary>
        /// Default throttling limit for ListOrders calls; default to 1 per 12 seconds.
        /// </summary>
        private const int LIST_ORDERS_DEFAULT_THROTTLE_LIMIT = 2 * 60 * 1000;

        /// <summary>
        /// Default throttling limit for ListOrderItems calls; default to 1 per 10 minutes.
        /// </summary>
        private const int LIST_ORDER_ITEMS_DEFAULT_THROTTLE_LIMIT = 1 * 60 * 1000;

        private MarketplaceWebServiceOrders mwsService;
        private string mwsSellerId;
        private string[] mwsMarketplaceIdList;
        private DateTime lastServiceCall = DateTime.MinValue;

        private ProcessOrderHandler _processOrder;
        /// <summary>
        /// Event called when an order is received for processing.
        /// </summary>
        public event ProcessOrderHandler ProcessOrder
        {
            add { _processOrder += value; }
            remove { _processOrder -= value; }
        }

        /// <summary>
        /// Creates a new instance of the OrderFetcherSample class.
        /// </summary>
        /// <param name="service"></param>
        public OrderFetcher(MarketplaceWebServiceOrders service, string sellerId, string[] marketplaceIdList)
        {
            mwsService = service;
            mwsSellerId = sellerId;
            mwsMarketplaceIdList = marketplaceIdList;
        }

        /// <summary>
        /// Fetches all orders created between the starting time and the server's
        /// local system time minus two minutes.
        /// <param name="startTime">The starting time period of orders to fetch.</param>
        public void FetchNewOrders(DateTime startTime)
        {
            FetchOrders(startTime, OrderDateType.New);
        }
        
        public void FetchUpatedOrders(DateTime startTime)
        {
            FetchOrders(startTime, OrderDateType.Updated);
        }

        /// <summary>
        /// Fetches all orders created in the given time period and processes them locally.        
        /// <param name="startTime">The starting time period of orders to fetch.</param>
        /// <param name="endTime">The ending time period of orders to fetch.</param>
        public void FetchOrders(DateTime time, OrderDateType type)
        {
            ListOrdersRequest request = new ListOrdersRequest();
            if(type == OrderDateType.New)
            	request.CreatedAfter = time;
            else 
            	request.LastUpdatedAfter = time;
            
            request.SellerId = mwsSellerId;
            request.MarketplaceId = new MarketplaceIdList();
            request.MarketplaceId.Id = new List<string>();
            foreach (string marketplaceId in mwsMarketplaceIdList)
            {
                request.MarketplaceId.Id.Add(marketplaceId);
            }

            List<Order> orderList = new List<Order>();
            ListOrdersResponse response = null;
            OrderFetcher.InvokeRetriable(LIST_ORDERS_DEFAULT_THROTTLE_LIMIT, delegate()
            {
                response = mwsService.ListOrders(request);
                ProcessOrders(response.ListOrdersResult.Orders.Order);
            });

            while (!string.IsNullOrEmpty(response.ListOrdersResult.NextToken))
            {
                // If NextToken is set, continue looping through the orders.
                ListOrdersByNextTokenRequest nextRequest = new ListOrdersByNextTokenRequest();
                nextRequest.NextToken = response.ListOrdersResult.NextToken;
                nextRequest.SellerId = mwsSellerId;

                ListOrdersByNextTokenResponse nextResponse = null;
                OrderFetcher.InvokeRetriable(LIST_ORDERS_DEFAULT_THROTTLE_LIMIT, delegate()
                {
                    nextResponse = mwsService.ListOrdersByNextToken(nextRequest);
                    ProcessOrders(nextResponse.ListOrdersByNextTokenResult.Orders.Order);
                });
            }
        }

        /// <summary>
        /// Method called by the FetchOrders method to process the orders.
        /// </summary>
        /// <param name="orders">List of orders returned by FetchOrders</param>
        protected virtual void ProcessOrders(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                if (_processOrder != null)
                {
                    _processOrder(order);
                }
            }
        }

        /// <summary>
        /// Fetches the OrderItems for the specified orderId.
        /// </summary>
        public void FetchOrderItems(string orderId, ProcessOrderItemHandler handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");

            ListOrderItemsRequest request = new ListOrderItemsRequest();
            request.SellerId = mwsSellerId;
            request.AmazonOrderId = orderId;

            ListOrderItemsResponse response = null;
            OrderFetcher.InvokeRetriable(LIST_ORDER_ITEMS_DEFAULT_THROTTLE_LIMIT, delegate()
            {
                response = mwsService.ListOrderItems(request);
                foreach (OrderItem orderItem in response.ListOrderItemsResult.OrderItems.OrderItem)
                {
                    handler(orderItem);
                }
            });

            while (!string.IsNullOrEmpty(response.ListOrderItemsResult.NextToken))
            {
                // If NextToken is set, continue looping through the orders.
                ListOrderItemsByNextTokenRequest nextRequest = new ListOrderItemsByNextTokenRequest();
                nextRequest.NextToken = response.ListOrderItemsResult.NextToken;
                nextRequest.SellerId = mwsSellerId;

                ListOrderItemsByNextTokenResponse nextResponse = null;
                OrderFetcher.InvokeRetriable(LIST_ORDER_ITEMS_DEFAULT_THROTTLE_LIMIT, delegate()
                {
                    nextResponse = mwsService.ListOrderItemsByNextToken(nextRequest);
                    foreach (OrderItem orderItem in nextResponse.ListOrderItemsByNextTokenResult.OrderItems.OrderItem)
                    {                        
                        handler(orderItem);
                    }
                });
            }
        }

        /// <summary>
        /// Invokes a method in a retriable fashion.
        /// </summary>
        /// <param name="throttledWaitTime">The amount of time to wait if the request is throttled.</param>
        /// <param name="method">The method to invoke.</param>
        public static void InvokeRetriable(RetriableMethodCall method)
        {
            InvokeRetriable(DEFAULT_THROTTLED_WAIT_TIMEOUT, method);
        }

        /// <summary>
        /// Invokes a method in a retriable fashion.
        /// </summary>
        /// <param name="throttledWaitTime">The amount of time to wait if the request is throttled.</param>
        /// <param name="method">The method to invoke.</param>
        public static void InvokeRetriable(int throttledWaitTime, RetriableMethodCall method)
        {
            bool retryRequest = false;

            do
            {
                try
                {
                	retryRequest = false;
                    // Perform some action
                    method.Invoke();
                }
                catch (MarketplaceWebServiceOrdersException ordersErr)
                {
                    // If the request is throttled, wait and try again.
                    if (ordersErr.ErrorCode == "RequestThrottled")
                    {                        
						NLog.LogManager.GetCurrentClassLogger().Warn("Request is throttled; waiting for {0}", throttledWaitTime / 1000);                        
                        retryRequest = true;
                        System.Threading.Thread.Sleep(throttledWaitTime);
                    }
                    else
                    {
                        // On any other error, re-throw the exception to be handled by the caller
                        throw;
                    }
                }
            } while (retryRequest);
        }
    }
}
