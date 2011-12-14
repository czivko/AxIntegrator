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
 *  Generated: Thu Jan 20 20:21:00 UTC 2011 
 * 
 */

using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using MarketplaceWebServiceOrders.Model;
using MarketplaceWebServiceOrders;


namespace MarketplaceWebServiceOrders
{
   /**
    *
    * MarketplaceWebServiceOrdersClient is an implementation of MarketplaceWebServiceOrders
    *
    */
    public class MarketplaceWebServiceOrdersClient : MarketplaceWebServiceOrders
    {
        private String awsAccessKeyId = null;
        private String awsSecretAccessKey = null;
        private MarketplaceWebServiceOrdersConfig config = null;

        private const String REQUEST_THROTTLED_ERROR_CODE = "RequestThrottled";

        /// <summary>
        /// Constructs MarketplaceWebServiceOrdersClient with AWS Access Key ID and AWS Secret Key
        /// </summary>

        /// <param name="applicationName">Your application's name, e.g. "MyMWSApp"</param>

        /// <param name="applicationVersion">Your application's version, e.g. "1.0"</param>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="config">configuration</param>
        public MarketplaceWebServiceOrdersClient(

            String applicationName,

            String applicationVersion,
            String awsAccessKeyId, 
            String awsSecretAccessKey, 
            MarketplaceWebServiceOrdersConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;

            config.SetUserAgent(applicationName, applicationVersion);
        }


        // Public API ------------------------------------------------------------//

        
        /// <summary>
        /// List Orders By Next Token 
        /// </summary>
        /// <param name="request">List Orders By Next Token  request</param>
        /// <returns>List Orders By Next Token  Response from the service</returns>
        /// <remarks>
        /// If ListOrders returns a nextToken, thus indicating that there are more orders
        /// than returned that matched the given filter criteria, ListOrdersByNextToken
        /// can be used to retrieve those other orders using that nextToken.
        /// 
        /// </remarks>
        public ListOrdersByNextTokenResponse ListOrdersByNextToken(ListOrdersByNextTokenRequest request)
        {
            return Invoke<ListOrdersByNextTokenResponse>(ConvertListOrdersByNextToken(request));
        }

        
        /// <summary>
        /// List Order Items By Next Token 
        /// </summary>
        /// <param name="request">List Order Items By Next Token  request</param>
        /// <returns>List Order Items By Next Token  Response from the service</returns>
        /// <remarks>
        /// If ListOrderItems cannot return all the order items in one go, it will
        /// provide a nextToken.  That nextToken can be used with this operation to
        /// retrive the next batch of items for that order.
        /// 
        /// </remarks>
        public ListOrderItemsByNextTokenResponse ListOrderItemsByNextToken(ListOrderItemsByNextTokenRequest request)
        {
            return Invoke<ListOrderItemsByNextTokenResponse>(ConvertListOrderItemsByNextToken(request));
        }

        
        /// <summary>
        /// Get Order 
        /// </summary>
        /// <param name="request">Get Order  request</param>
        /// <returns>Get Order  Response from the service</returns>
        /// <remarks>
        /// This operation takes up to 50 order ids and returns the corresponding orders.
        /// 
        /// </remarks>
        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            return Invoke<GetOrderResponse>(ConvertGetOrder(request));
        }

        
        /// <summary>
        /// List Order Items 
        /// </summary>
        /// <param name="request">List Order Items  request</param>
        /// <returns>List Order Items  Response from the service</returns>
        /// <remarks>
        /// This operation can be used to list the items of the order indicated by the
        /// given order id (only a single Amazon order id is allowed).
        /// 
        /// </remarks>
        public ListOrderItemsResponse ListOrderItems(ListOrderItemsRequest request)
        {
            return Invoke<ListOrderItemsResponse>(ConvertListOrderItems(request));
        }

        
        /// <summary>
        /// List Orders 
        /// </summary>
        /// <param name="request">List Orders  request</param>
        /// <returns>List Orders  Response from the service</returns>
        /// <remarks>
        /// ListOrders can be used to find orders that meet the specified criteria.
        /// 
        /// </remarks>
        public ListOrdersResponse ListOrders(ListOrdersRequest request)
        {
            return Invoke<ListOrdersResponse>(ConvertListOrders(request));
        }

        
        /// <summary>
        /// Get Service Status 
        /// </summary>
        /// <param name="request">Get Service Status  request</param>
        /// <returns>Get Service Status  Response from the service</returns>
        /// <remarks>
        /// Returns the service status of a particular MWS API section. The operation
        /// takes no input.
        /// All API sections within the API are required to implement this operation.
        /// 
        /// </remarks>
        public GetServiceStatusResponse GetServiceStatus(GetServiceStatusRequest request)
        {
            return Invoke<GetServiceStatusResponse>(ConvertGetServiceStatus(request));
        }

        // Private API ------------------------------------------------------------//

        /**
         * Configure HttpClient with set of defaults as well as configuration
         * from MarketplaceWebServiceOrdersConfig instance
         */
        private HttpWebRequest ConfigureWebRequest(int contentLength)
        {
            HttpWebRequest request = WebRequest.Create(config.ServiceURL) as HttpWebRequest;

            if (config.IsSetProxyHost())
            {
                request.Proxy = new WebProxy(config.ProxyHost, config.ProxyPort);
            }
            request.UserAgent = config.UserAgent;
            request.Method = "POST";
            request.Timeout = 50000;
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.ContentLength = contentLength;

            return request;
        }

        /**
         * Invoke request and return response
         */
        private T Invoke<T>(IDictionary<String, String> parameters)
        {
            String actionName = parameters["Action"];
            T response = default(T);
            String responseBody = null;
            HttpStatusCode statusCode = default(HttpStatusCode);

            if (String.IsNullOrEmpty(config.ServiceURL))
            {
                throw new MarketplaceWebServiceOrdersException(
                    new ArgumentException(
                        "Missing serviceURL configuration value. You may obtain a list of valid MWS URLs by consulting the MWS Developer's Guide, or reviewing the sample code published along side this library."));
            }

            /* Add required request parameters */
            AddRequiredParameters(parameters);

            String queryString = GetParametersAsString(parameters);

            byte[] requestData = Encoding.UTF8.GetBytes(queryString);
            bool shouldRetry = true;
            int retries = 0;
            do
            {
                HttpWebRequest request = ConfigureWebRequest(requestData.Length);
                /* Submit the request and read response body */
                try
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                    }
                    using (HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse)
                    {
                        statusCode = httpResponse.StatusCode;
                        using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd();
                        }
                    }
                    /* Attempt to deserialize response into <Action> Response type */
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (StringReader responseReader = new StringReader(responseBody))
                    {
                        response = (T)serializer.Deserialize(responseReader);
                    }
                    shouldRetry = false;
                }
                /* Web exception is thrown on unsucessful responses */
                catch (WebException we)
                {
                    shouldRetry = false;
                    using (HttpWebResponse httpErrorResponse = (HttpWebResponse)we.Response as HttpWebResponse)
                    {
                        if (httpErrorResponse == null)
                        {
                            throw new MarketplaceWebServiceOrdersException(we);
                        }
                        statusCode = httpErrorResponse.StatusCode;
                        using (StreamReader reader = new StreamReader(httpErrorResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd();
                        }
                    }

                    /* Attempt to deserialize response into ErrorResponse type */
                    using (StringReader responseReader = new StringReader(responseBody))
                    {
                        try
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(ErrorResponse));
                            ErrorResponse errorResponse = (ErrorResponse)serializer.Deserialize(responseReader);
                            Error error = errorResponse.Error[0];

                            bool retriableError =
                                (statusCode == HttpStatusCode.InternalServerError ||
                                 statusCode == HttpStatusCode.ServiceUnavailable);

                            retriableError = retriableError && !(REQUEST_THROTTLED_ERROR_CODE.Equals(error.Code));
                            if (retriableError && retries < config.MaxErrorRetry)
                            {
                                shouldRetry = true;
                                PauseOnRetry(++retries, statusCode);
                                continue;
                            }
                            else
                            {
                                shouldRetry = false;
                            }

                            /* Throw formatted exception with information available from the error response */
                            throw new MarketplaceWebServiceOrdersException(
                                error.Message,
                                statusCode,
                                error.Code,
                                error.Type,
                                errorResponse.RequestId,
                                errorResponse.ToXML());
                        }
                        catch (MarketplaceWebServiceOrdersException mwsErr)
                        {
                            throw mwsErr;
                        }
                        catch (Exception e)
                        {
                            throw ReportAnyErrors(responseBody, statusCode, e);
                        }
                    }
                }

                /* Catch other exceptions, attempt to convert to formatted exception,
                 * else rethrow wrapped exception */
                catch (Exception e)
                {
                    throw new MarketplaceWebServiceOrdersException(e.Message, statusCode, e, responseBody);
                }
            } while (shouldRetry);

            return response;
        }


        /**
         * Look for additional error strings in the response and return formatted exception
         */
        private MarketplaceWebServiceOrdersException ReportAnyErrors(String responseBody, HttpStatusCode status, Exception e)
        {

            MarketplaceWebServiceOrdersException ex = null;

            if (responseBody != null && responseBody.StartsWith("<"))
            {
                Match errorMatcherOne = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error>" +
                        "<Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
                Match errorMatcherTwo = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)" +
                        "</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);

                if (errorMatcherOne.Success)
                {
                    String requestId = errorMatcherOne.Groups[1].Value;
                    String code = errorMatcherOne.Groups[2].Value;
                    String message = errorMatcherOne.Groups[3].Value;

                    ex = new MarketplaceWebServiceOrdersException(message, status, code, "Unknown", requestId, responseBody, e);

                }
                else if (errorMatcherTwo.Success)
                {
                    String code = errorMatcherTwo.Groups[1].Value;
                    String message = errorMatcherTwo.Groups[2].Value;
                    String requestId = errorMatcherTwo.Groups[4].Value;

                    ex = new MarketplaceWebServiceOrdersException(message, status, code, "Unknown", requestId, responseBody, e);
                }
                else
                {
                    ex = new MarketplaceWebServiceOrdersException("Internal Error", status, e, responseBody);
                }
            }
            else
            {
                ex = new MarketplaceWebServiceOrdersException("Internal Error", status, e, responseBody);
            }
            return ex;
        }

        /**
         * Exponential sleep on failed request
         */
        private void PauseOnRetry(int retries, HttpStatusCode status)
        {
            if (retries <= config.MaxErrorRetry)
            {
                int delay = (int)Math.Pow(4, retries) * 100;
                System.Threading.Thread.Sleep(delay);
            }
            else
            {
                throw new MarketplaceWebServiceOrdersException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
        }

        /**
         * Add authentication related and version parameters
         */
        private void AddRequiredParameters(IDictionary<String, String> parameters)
        {
            parameters.Add("AWSAccessKeyId", this.awsAccessKeyId);
            parameters.Add("Timestamp", GetFormattedTimestamp());
            parameters.Add("Version", config.ServiceVersion);
            parameters.Add("SignatureVersion", config.SignatureVersion);
            parameters.Add("Signature", SignParameters(parameters, this.awsSecretAccessKey));
        }

        /**
         * Convert Dictionary of paremeters to Url encoded query string
         */
        private string GetParametersAsString(IDictionary<String, String> parameters)
        {
            StringBuilder data = new StringBuilder();
            foreach (String key in (IEnumerable<String>)parameters.Keys)
            {
                String value = parameters[key];
                if (!String.IsNullOrEmpty(value))
                {
                    data.Append(key);
                    data.Append('=');
                    data.Append(UrlEncode(value, false));
                    data.Append('&');
                }
            }
            String result = data.ToString();
            return result.Remove(result.Length - 1);
        }

        /**
         * Computes RFC 2104-compliant HMAC signature for request parameters
         * Implements AWS Signature, as per following spec:
         *
         * If Signature Version is 2, string to sign is based on following:
         *
         *    1. The HTTP Request Method followed by an ASCII newline (%0A)
         *    2. The HTTP Host header in the form of lowercase host, followed by an ASCII newline.
         *    3. The URL encoded HTTP absolute path component of the URI
         *       (up to but not including the query string parameters);
         *       if this is empty use a forward '/'. This parameter is followed by an ASCII newline.
         *    4. The concatenation of all query string components (names and values)
         *       as UTF-8 characters which are URL encoded as per RFC 3986
         *       (hex characters MUST be uppercase), sorted using lexicographic byte ordering.
         *       Parameter names are separated from their values by the '=' character
         *       (ASCII character 61), even if the value is empty.
         *       Pairs of parameter and values are separated by the '&' character (ASCII code 38).
         *
         */
        private String SignParameters(IDictionary<String, String> parameters, String key)
        {
            String signatureVersion = parameters["SignatureVersion"];

            KeyedHashAlgorithm algorithm = new HMACSHA1();

            String stringToSign = null;
            if ("2".Equals(signatureVersion))
            {
                String signatureMethod = config.SignatureMethod;
                algorithm = KeyedHashAlgorithm.Create(signatureMethod.ToUpper());
                parameters.Add("SignatureMethod", signatureMethod);
                stringToSign = CalculateStringToSignV2(parameters);
            }
            else
            {
                throw new ArgumentException("SignatureVersion", "Invalid Signature Version specified");
            }

            return Sign(stringToSign, key, algorithm);
        }

        private String CalculateStringToSignV2(IDictionary<String, String> parameters)
        {
            StringBuilder data = new StringBuilder();
            IDictionary<String, String> sorted =
                  new SortedDictionary<String, String>(parameters, StringComparer.Ordinal);
            data.Append("POST");
            data.Append("\n");
            Uri endpoint = new Uri(config.ServiceURL);

            data.Append(endpoint.Host);
            if (endpoint.Port != 80 && endpoint.Port != 443)
            {
                data.Append(":");
                data.Append(endpoint.Port);
            }
            data.Append("\n");
            String uri = endpoint.AbsolutePath;
            if (String.IsNullOrEmpty(uri))
            {
                uri = "/";
            }
            data.Append(UrlEncode(uri, true));
            data.Append("\n");
            foreach (KeyValuePair<String, String> pair in sorted)
            {
                if (pair.Value != null)
                {
                    data.Append(UrlEncode(pair.Key, false));
                    data.Append("=");
                    data.Append(UrlEncode(pair.Value, false));
                    data.Append("&");
                }

            }

            String result = data.ToString();
            return result.Remove(result.Length - 1);
        }

        private String UrlEncode(String data, bool path)
        {
            StringBuilder encoded = new StringBuilder();
            String unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~" + (path ? "/" : "");

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
            }
            else
            {
                    encoded.Append("%" + String.Format("{0:X2}", (int)symbol));
            }
            }

            return encoded.ToString();

        }

        /**
         * Computes RFC 2104-compliant HMAC signature.
         */
        private String Sign(String data, String key, KeyedHashAlgorithm algorithm)
        {
            Encoding encoding = new UTF8Encoding();
            algorithm.Key = encoding.GetBytes(key);
            return Convert.ToBase64String(algorithm.ComputeHash(
                encoding.GetBytes(data.ToCharArray())));
        }


        /**
         * Formats date as ISO 8601 timestamp
         */
        private String GetFormattedTimestamp()
        {
            DateTime dateTime = DateTime.Now;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                 dateTime.Hour, dateTime.Minute, dateTime.Second,
                                 dateTime.Millisecond
                                 , DateTimeKind.Local
                               ).ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z",
                                CultureInfo.InvariantCulture);
        }

        /**
         * Formats date as ISO 8601 timestamp
         */
        private String GetFormattedTimestamp(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
                                 dateTime.Hour, dateTime.Minute, dateTime.Second,
                                 dateTime.Millisecond
                                 , DateTimeKind.Local
                               ).ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z",
                                CultureInfo.InvariantCulture);
        }

                                                
        /**
         * Convert ListOrdersByNextTokenRequest to name value pairs
         */
        private IDictionary<String, String> ConvertListOrdersByNextToken(ListOrdersByNextTokenRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "ListOrdersByNextToken");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }
            if (request.IsSetNextToken())
            {
                parameters.Add("NextToken", request.NextToken);
            }

            return parameters;
        }
        
                                                
        /**
         * Convert ListOrderItemsByNextTokenRequest to name value pairs
         */
        private IDictionary<String, String> ConvertListOrderItemsByNextToken(ListOrderItemsByNextTokenRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "ListOrderItemsByNextToken");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }
            if (request.IsSetNextToken())
            {
                parameters.Add("NextToken", request.NextToken);
            }

            return parameters;
        }
        
                                                
        /**
         * Convert GetOrderRequest to name value pairs
         */
        private IDictionary<String, String> ConvertGetOrder(GetOrderRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "GetOrder");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }
            if (request.IsSetAmazonOrderId())
            {
                OrderIdList  getOrderRequestAmazonOrderId = request.AmazonOrderId;
                List<String> amazonOrderIdIdList  =  getOrderRequestAmazonOrderId.Id;
                int amazonOrderIdIdListIndex = 1;
                foreach  (String amazonOrderIdId in amazonOrderIdIdList)
                {
                    parameters.Add("AmazonOrderId" + "." + "Id" + "."  + amazonOrderIdIdListIndex, amazonOrderIdId);
                    amazonOrderIdIdListIndex++;
                }
            }

            return parameters;
        }
        
                                                
        /**
         * Convert ListOrderItemsRequest to name value pairs
         */
        private IDictionary<String, String> ConvertListOrderItems(ListOrderItemsRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "ListOrderItems");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }
            if (request.IsSetAmazonOrderId())
            {
                parameters.Add("AmazonOrderId", request.AmazonOrderId);
            }

            return parameters;
        }
        
                                                
        /**
         * Convert ListOrdersRequest to name value pairs
         */
        private IDictionary<String, String> ConvertListOrders(ListOrdersRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "ListOrders");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }
            if (request.IsSetCreatedAfter())
            {
                parameters.Add("CreatedAfter", GetFormattedTimestamp(request.CreatedAfter));
            }
            if (request.IsSetCreatedBefore())
            {
                parameters.Add("CreatedBefore", GetFormattedTimestamp(request.CreatedBefore));
            }
            if (request.IsSetLastUpdatedAfter())
            {
                parameters.Add("LastUpdatedAfter", GetFormattedTimestamp(request.LastUpdatedAfter));
            }
            if (request.IsSetLastUpdatedBefore())
            {
                parameters.Add("LastUpdatedBefore", GetFormattedTimestamp(request.LastUpdatedBefore));
            }
            if (request.IsSetOrderStatus())
            {
                OrderStatusList  listOrdersRequestOrderStatus = request.OrderStatus;
                List<OrderStatusEnum> orderStatusStatusList  =  listOrdersRequestOrderStatus.Status;
                int orderStatusStatusListIndex = 1;
                foreach  (OrderStatusEnum orderStatusStatus in orderStatusStatusList)
                {
                    parameters.Add("OrderStatus" + "." + "Status" + "."  + orderStatusStatusListIndex, orderStatusStatus + "");
                    orderStatusStatusListIndex++;
                }
            }
            if (request.IsSetMarketplaceId())
            {
                MarketplaceIdList  listOrdersRequestMarketplaceId = request.MarketplaceId;
                List<String> marketplaceIdIdList  =  listOrdersRequestMarketplaceId.Id;
                int marketplaceIdIdListIndex = 1;
                foreach  (String marketplaceIdId in marketplaceIdIdList)
                {
                    parameters.Add("MarketplaceId" + "." + "Id" + "."  + marketplaceIdIdListIndex, marketplaceIdId);
                    marketplaceIdIdListIndex++;
                }
            }
            if (request.IsSetFulfillmentChannel())
            {
                FulfillmentChannelList  listOrdersRequestFulfillmentChannel = request.FulfillmentChannel;
                List<FulfillmentChannelEnum> fulfillmentChannelChannelList  =  listOrdersRequestFulfillmentChannel.Channel;
                int fulfillmentChannelChannelListIndex = 1;
                foreach  (FulfillmentChannelEnum fulfillmentChannelChannel in fulfillmentChannelChannelList)
                {
                    parameters.Add("FulfillmentChannel" + "." + "Channel" + "."  + fulfillmentChannelChannelListIndex, fulfillmentChannelChannel + "");
                    fulfillmentChannelChannelListIndex++;
                }
            }
            if (request.IsSetBuyerEmail())
            {
                parameters.Add("BuyerEmail", request.BuyerEmail);
            }
            if (request.IsSetSellerOrderId())
            {
                parameters.Add("SellerOrderId", request.SellerOrderId);
            }
            if (request.IsSetMaxResultsPerPage())
            {
                parameters.Add("MaxResultsPerPage", request.MaxResultsPerPage + "");
            }

            return parameters;
        }
        
                                                
        /**
         * Convert GetServiceStatusRequest to name value pairs
         */
        private IDictionary<String, String> ConvertGetServiceStatus(GetServiceStatusRequest request)
        {            
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Action", "GetServiceStatus");
            if (request.IsSetSellerId())
            {
                parameters.Add("SellerId", request.SellerId);
            }

            return parameters;
        }
    }
}