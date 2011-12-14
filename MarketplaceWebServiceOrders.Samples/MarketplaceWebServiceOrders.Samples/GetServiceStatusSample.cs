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
    /// Get Service Status  Samples
    /// </summary>
    public class GetServiceStatusSample
    {
    
                                             
        /// <summary>
        /// Returns the service status of a particular MWS API section. The operation
        /// takes no input.
        /// All API sections within the API are required to implement this operation.
        /// 
        /// </summary>
        /// <param name="service">Instance of MarketplaceWebServiceOrders service</param>
        /// <param name="request">GetServiceStatusRequest request</param>
        public static void InvokeGetServiceStatus(MarketplaceWebServiceOrders service, GetServiceStatusRequest request)
        {
            try 
            {
                GetServiceStatusResponse response = service.GetServiceStatus(request);
                
                
                Console.WriteLine ("Service Response");
                Console.WriteLine ("=============================================================================");
                Console.WriteLine ();

                Console.WriteLine("        GetServiceStatusResponse");
                if (response.IsSetGetServiceStatusResult())
                {
                    Console.WriteLine("            GetServiceStatusResult");
                    GetServiceStatusResult  getServiceStatusResult = response.GetServiceStatusResult;
                    if (getServiceStatusResult.IsSetStatus())
                    {
                        Console.WriteLine("                Status");
                        Console.WriteLine("                    {0}", getServiceStatusResult.Status);
                    }
                    if (getServiceStatusResult.IsSetTimestamp())
                    {
                        Console.WriteLine("                Timestamp");
                        Console.WriteLine("                    {0}", getServiceStatusResult.Timestamp);
                    }
                    if (getServiceStatusResult.IsSetMessageId())
                    {
                        Console.WriteLine("                MessageId");
                        Console.WriteLine("                    {0}", getServiceStatusResult.MessageId);
                    }
                    if (getServiceStatusResult.IsSetMessages())
                    {
                        Console.WriteLine("                Messages");
                        MessageList  messages = getServiceStatusResult.Messages;
                        List<Message> messageList = messages.Message;
                        foreach (Message message in messageList)
                        {
                            Console.WriteLine("                    Message");
                            if (message.IsSetLocale())
                            {
                                Console.WriteLine("                        Locale");
                                Console.WriteLine("                            {0}", message.Locale);
                            }
                            if (message.IsSetText())
                            {
                                Console.WriteLine("                        Text");
                                Console.WriteLine("                            {0}", message.Text);
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
