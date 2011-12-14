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
 *  Generated: Wed Jan 26 18:07:39 UTC 2011 
 * 
 */

using System;
using System.Xml.Serialization;
using System.Collections.Generic;

using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Mock;
using MarketplaceWebServiceOrders.Model;

namespace MarketplaceWebServiceOrders.Samples
{
    /// <summary>
    /// Marketplace Web Service Orders  Samples
    /// </summary>
    public class MarketplaceWebServiceOrdersSamples
    {
        /**
         * Samples for Marketplace Web Service Orders functionality
         */
        public static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Welcome to Marketplace Web Service Orders Samples!");
            Console.WriteLine("===========================================");

            Console.WriteLine("To get started:");
            Console.WriteLine("===========================================");
            Console.WriteLine("  - Fill in your MWS credentials");
            Console.WriteLine("  - Uncomment sample you're interested in trying");
            Console.WriteLine("  - Set request with desired parameters");
            Console.WriteLine("  - Hit F5 to run!");
            Console.WriteLine();

            Console.WriteLine("===========================================");
            Console.WriteLine("Samples Output");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            /************************************************************************
             * Access Key ID and Secret Access Key ID
             ***********************************************************************/
            String accessKeyId = "AKIAIZAIBEDEFFEWL3OA";
            String secretAccessKey = "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";
            String merchantId = "A3FFNVBB2U6OUI";
            String marketplaceId = "ATVPDKIKX0DER";

            /************************************************************************
             * The application name and version are included in each MWS call's
             * HTTP User-Agent field.
             ***********************************************************************/
            const string applicationName = "<Your Application Name>";
            const string applicationVersion = "<Your Application Version>";

            /************************************************************************
            * Uncomment to try advanced configuration options. Available options are:
            *
            *  - Signature Version
            *  - Proxy Host and Proxy Port
            *  - Service URL
            *  - User Agent String to be sent to Marketplace Web Service Orders  service
            *
            ***********************************************************************/
            MarketplaceWebServiceOrdersConfig config = new MarketplaceWebServiceOrdersConfig();
            //
            // IMPORTANT: Uncomment out the appropiate line for the country you wish 
            // to sell in:
            // 
            // United States:
            config.ServiceURL = "https://mws.amazonservices.com/Orders/2011-01-01";
            //
            // Canada:
            // config.ServiceURL = "https://mws.amazonservices.ca/Orders/2011-01-01";
            //
            // United Kingdom:
            // config.ServiceURL = "https://mws.amazonservices.co.uk/Orders/2011-01-01";
            //
            // Germany:
            // config.ServiceURL = "https://mws.amazonservices.de/Orders/2011-01-01";
            //
            // France:
            // config.ServiceURL = "https://mws.amazonservices.fr/Orders/2011-01-01";
            //
            // Italy:
            // config.ServiceURL = "https://mws.amazonservices.it/Orders/2011-01-01";
            //
            // Japan:
            // config.ServiceURL = "https://mws.amazonservices.jp/Orders/2011-01-01";
            //
            // China:
            // config.ServiceURL = "https://mws.amazonservices.com.cn/Orders/2011-01-01";
            //

            /************************************************************************
            * Instantiate  Implementation of Marketplace Web Service Orders 
            ***********************************************************************/
            MarketplaceWebServiceOrders service = new MarketplaceWebServiceOrdersClient(
                applicationName, applicationVersion, accessKeyId, secretAccessKey, config);

            /************************************************************************
            * Uncomment to try out Mock Service that simulates Marketplace Web Service Orders 
            * responses without calling Marketplace Web Service Orders  service.
            *
            * Responses are loaded from local XML files. You can tweak XML files to
            * experiment with various outputs during development
            *
            * XML files available under MarketplaceWebServiceOrders\Mock tree
            *
            ***********************************************************************/
            // MarketplaceWebServiceOrders service = new MarketplaceWebServiceOrdersMock();


            /************************************************************************
            * Uncomment to invoke List Orders By Next Token Action
            ***********************************************************************/
            //ListOrdersByNextTokenRequest request = new ListOrdersByNextTokenRequest();
            //// @TODO: set request parameters here
            //request.SellerId = merchantId;
            //request.NextToken = "NEXT_TOKEN";
            //ListOrdersByNextTokenSample.InvokeListOrdersByNextToken(service, request);

            /************************************************************************
            * Uncomment to invoke List Order Items By Next Token Action
            ***********************************************************************/
            //ListOrderItemsByNextTokenRequest request = new ListOrderItemsByNextTokenRequest();
            //// @TODO: set request parameters here
            //request.SellerId = merchantId;
            //request.NextToken = "NEXT_TOKEN";
            //ListOrderItemsByNextTokenSample.InvokeListOrderItemsByNextToken(service, request);
            
            /************************************************************************
            * Uncomment to invoke Get Order Action
            ***********************************************************************/
            //GetOrderRequest request = new GetOrderRequest();
            //// @TODO: set request parameters here
            //request.SellerId = merchantId;
            //request.AmazonOrderId = new OrderIdList();
            //request.AmazonOrderId.Id = new List<string>(new string[] { "ORDER_1", "ORDER_2" });
            //GetOrderSample.InvokeGetOrder(service, request);
            
            /************************************************************************
            * Uncomment to invoke List Order Items Action
            ***********************************************************************/
            //ListOrderItemsRequest request = new ListOrderItemsRequest();
            //// @TODO: set request parameters here
            //request.SellerId = merchantId;
            //request.AmazonOrderId = "ORDER_ID";
            // ListOrderItemsSample.InvokeListOrderItems(service, request);

            /************************************************************************
            * Uncomment to invoke List Orders Action
            ***********************************************************************/
            ListOrdersRequest request = new ListOrdersRequest();
            // @TODO: set request parameters here
            request.CreatedAfter = DateTime.Now.AddDays(-1);
            request.MarketplaceId = new MarketplaceIdList();
            request.MarketplaceId.Id = new List<string>(new string[] { marketplaceId });
            request.SellerId = merchantId;
            ListOrdersSample.InvokeListOrders(service, request);

            /************************************************************************
            * Uncomment to invoke Get Service Status Action
            ***********************************************************************/
            // GetServiceStatusRequest request = new GetServiceStatusRequest();
            // request.SellerId = merchantId;
            //// @TODO: set request parameters here
            // GetServiceStatusSample.InvokeGetServiceStatus(service, request);

            /************************************************************************
            * Uncomment to invoke OrderFetcher Sample
            ***********************************************************************/
            // OrderFetcherSample.InvokeOrderFetcherSample(service, merchantId, new string [] { marketplaceId } );

            /************************************************************************
            * Uncomment to invoke FetchOrderUpdatesJob Sample - A sample
            * implementation of continuously retrieving orders over a given
            * time interval.
            ***********************************************************************/
            // FetchOrderUpdatesJob.InvokeOrderFetcherSample(service, merchantId, new string[] { marketplaceId });

            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine("End of output. You can close this window");
            Console.WriteLine("===========================================");

            System.Threading.Thread.Sleep(50000);
        }

    }
}
