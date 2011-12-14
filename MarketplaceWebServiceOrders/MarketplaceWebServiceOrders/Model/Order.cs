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
 *  Generated: Wed Oct 05 19:31:30 GMT 2011 
 * 
 */


using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;


namespace MarketplaceWebServiceOrders.Model
{
    [XmlTypeAttribute(Namespace = "https://mws.amazonservices.com/Orders/2011-01-01")]
    [XmlRootAttribute(Namespace = "https://mws.amazonservices.com/Orders/2011-01-01", IsNullable = false)]
    public class Order
    {
    
        private String amazonOrderIdField;

        private String sellerOrderIdField;

        private DateTime? purchaseDateField;

        private DateTime? lastUpdateDateField;

        private OrderStatusEnum? orderStatusField;

        private FulfillmentChannelEnum? fulfillmentChannelField;

        private String salesChannelField;

        private String orderChannelField;

        private String shipServiceLevelField;

        private  Address shippingAddressField;
        private  Money orderTotalField;
        private Decimal? numberOfItemsShippedField;

        private Decimal? numberOfItemsUnshippedField;

        private String marketplaceIdField;

        private String buyerEmailField;

        private String buyerNameField;

        private String shipmentServiceLevelCategoryField;


        /// <summary>
        /// Gets and sets the AmazonOrderId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "AmazonOrderId")]
        public String AmazonOrderId
        {
            get { return this.amazonOrderIdField ; }
            set { this.amazonOrderIdField= value; }
        }



        /// <summary>
        /// Sets the AmazonOrderId property
        /// </summary>
        /// <param name="amazonOrderId">AmazonOrderId property</param>
        /// <returns>this instance</returns>
        public Order WithAmazonOrderId(String amazonOrderId)
        {
            this.amazonOrderIdField = amazonOrderId;
            return this;
        }



        /// <summary>
        /// Checks if AmazonOrderId property is set
        /// </summary>
        /// <returns>true if AmazonOrderId property is set</returns>
        public Boolean IsSetAmazonOrderId()
        {
            return  this.amazonOrderIdField != null;

        }


        /// <summary>
        /// Gets and sets the SellerOrderId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerOrderId")]
        public String SellerOrderId
        {
            get { return this.sellerOrderIdField ; }
            set { this.sellerOrderIdField= value; }
        }



        /// <summary>
        /// Sets the SellerOrderId property
        /// </summary>
        /// <param name="sellerOrderId">SellerOrderId property</param>
        /// <returns>this instance</returns>
        public Order WithSellerOrderId(String sellerOrderId)
        {
            this.sellerOrderIdField = sellerOrderId;
            return this;
        }



        /// <summary>
        /// Checks if SellerOrderId property is set
        /// </summary>
        /// <returns>true if SellerOrderId property is set</returns>
        public Boolean IsSetSellerOrderId()
        {
            return  this.sellerOrderIdField != null;

        }


        /// <summary>
        /// Gets and sets the PurchaseDate property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PurchaseDate")]
        public DateTime PurchaseDate
        {
            get { return this.purchaseDateField.GetValueOrDefault() ; }
            set { this.purchaseDateField= value; }
        }



        /// <summary>
        /// Sets the PurchaseDate property
        /// </summary>
        /// <param name="purchaseDate">PurchaseDate property</param>
        /// <returns>this instance</returns>
        public Order WithPurchaseDate(DateTime purchaseDate)
        {
            this.purchaseDateField = purchaseDate;
            return this;
        }



        /// <summary>
        /// Checks if PurchaseDate property is set
        /// </summary>
        /// <returns>true if PurchaseDate property is set</returns>
        public Boolean IsSetPurchaseDate()
        {
            return  this.purchaseDateField.HasValue;

        }


        /// <summary>
        /// Gets and sets the LastUpdateDate property.
        /// </summary>
        [XmlElementAttribute(ElementName = "LastUpdateDate")]
        public DateTime LastUpdateDate
        {
            get { return this.lastUpdateDateField.GetValueOrDefault() ; }
            set { this.lastUpdateDateField= value; }
        }



        /// <summary>
        /// Sets the LastUpdateDate property
        /// </summary>
        /// <param name="lastUpdateDate">LastUpdateDate property</param>
        /// <returns>this instance</returns>
        public Order WithLastUpdateDate(DateTime lastUpdateDate)
        {
            this.lastUpdateDateField = lastUpdateDate;
            return this;
        }



        /// <summary>
        /// Checks if LastUpdateDate property is set
        /// </summary>
        /// <returns>true if LastUpdateDate property is set</returns>
        public Boolean IsSetLastUpdateDate()
        {
            return  this.lastUpdateDateField.HasValue;

        }


        /// <summary>
        /// Gets and sets the OrderStatus property.
        /// </summary>
        [XmlElementAttribute(ElementName = "OrderStatus")]
        public OrderStatusEnum OrderStatus
        {
            get { return this.orderStatusField.GetValueOrDefault() ; }
            set { this.orderStatusField= value; }
        }



        /// <summary>
        /// Sets the OrderStatus property
        /// </summary>
        /// <param name="orderStatus">OrderStatus property</param>
        /// <returns>this instance</returns>
        public Order WithOrderStatus(OrderStatusEnum orderStatus)
        {
            this.orderStatusField = orderStatus;
            return this;
        }



        /// <summary>
        /// Checks if OrderStatus property is set
        /// </summary>
        /// <returns>true if OrderStatus property is set</returns>
        public Boolean IsSetOrderStatus()
        {
            return this.orderStatusField.HasValue;

        }


        /// <summary>
        /// Gets and sets the FulfillmentChannel property.
        /// </summary>
        [XmlElementAttribute(ElementName = "FulfillmentChannel")]
        public FulfillmentChannelEnum FulfillmentChannel
        {
            get { return this.fulfillmentChannelField.GetValueOrDefault() ; }
            set { this.fulfillmentChannelField= value; }
        }



        /// <summary>
        /// Sets the FulfillmentChannel property
        /// </summary>
        /// <param name="fulfillmentChannel">FulfillmentChannel property</param>
        /// <returns>this instance</returns>
        public Order WithFulfillmentChannel(FulfillmentChannelEnum fulfillmentChannel)
        {
            this.fulfillmentChannelField = fulfillmentChannel;
            return this;
        }



        /// <summary>
        /// Checks if FulfillmentChannel property is set
        /// </summary>
        /// <returns>true if FulfillmentChannel property is set</returns>
        public Boolean IsSetFulfillmentChannel()
        {
            return this.fulfillmentChannelField.HasValue;

        }


        /// <summary>
        /// Gets and sets the SalesChannel property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SalesChannel")]
        public String SalesChannel
        {
            get { return this.salesChannelField ; }
            set { this.salesChannelField= value; }
        }



        /// <summary>
        /// Sets the SalesChannel property
        /// </summary>
        /// <param name="salesChannel">SalesChannel property</param>
        /// <returns>this instance</returns>
        public Order WithSalesChannel(String salesChannel)
        {
            this.salesChannelField = salesChannel;
            return this;
        }



        /// <summary>
        /// Checks if SalesChannel property is set
        /// </summary>
        /// <returns>true if SalesChannel property is set</returns>
        public Boolean IsSetSalesChannel()
        {
            return  this.salesChannelField != null;

        }


        /// <summary>
        /// Gets and sets the OrderChannel property.
        /// </summary>
        [XmlElementAttribute(ElementName = "OrderChannel")]
        public String OrderChannel
        {
            get { return this.orderChannelField ; }
            set { this.orderChannelField= value; }
        }



        /// <summary>
        /// Sets the OrderChannel property
        /// </summary>
        /// <param name="orderChannel">OrderChannel property</param>
        /// <returns>this instance</returns>
        public Order WithOrderChannel(String orderChannel)
        {
            this.orderChannelField = orderChannel;
            return this;
        }



        /// <summary>
        /// Checks if OrderChannel property is set
        /// </summary>
        /// <returns>true if OrderChannel property is set</returns>
        public Boolean IsSetOrderChannel()
        {
            return  this.orderChannelField != null;

        }


        /// <summary>
        /// Gets and sets the ShipServiceLevel property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShipServiceLevel")]
        public String ShipServiceLevel
        {
            get { return this.shipServiceLevelField ; }
            set { this.shipServiceLevelField= value; }
        }



        /// <summary>
        /// Sets the ShipServiceLevel property
        /// </summary>
        /// <param name="shipServiceLevel">ShipServiceLevel property</param>
        /// <returns>this instance</returns>
        public Order WithShipServiceLevel(String shipServiceLevel)
        {
            this.shipServiceLevelField = shipServiceLevel;
            return this;
        }



        /// <summary>
        /// Checks if ShipServiceLevel property is set
        /// </summary>
        /// <returns>true if ShipServiceLevel property is set</returns>
        public Boolean IsSetShipServiceLevel()
        {
            return  this.shipServiceLevelField != null;

        }


        /// <summary>
        /// Gets and sets the ShippingAddress property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShippingAddress")]
        public Address ShippingAddress
        {
            get { return this.shippingAddressField ; }
            set { this.shippingAddressField = value; }
        }



        /// <summary>
        /// Sets the ShippingAddress property
        /// </summary>
        /// <param name="shippingAddress">ShippingAddress property</param>
        /// <returns>this instance</returns>
        public Order WithShippingAddress(Address shippingAddress)
        {
            this.shippingAddressField = shippingAddress;
            return this;
        }



        /// <summary>
        /// Checks if ShippingAddress property is set
        /// </summary>
        /// <returns>true if ShippingAddress property is set</returns>
        public Boolean IsSetShippingAddress()
        {
            return this.shippingAddressField != null;
        }




        /// <summary>
        /// Gets and sets the OrderTotal property.
        /// </summary>
        [XmlElementAttribute(ElementName = "OrderTotal")]
        public Money OrderTotal
        {
            get { return this.orderTotalField ; }
            set { this.orderTotalField = value; }
        }



        /// <summary>
        /// Sets the OrderTotal property
        /// </summary>
        /// <param name="orderTotal">OrderTotal property</param>
        /// <returns>this instance</returns>
        public Order WithOrderTotal(Money orderTotal)
        {
            this.orderTotalField = orderTotal;
            return this;
        }



        /// <summary>
        /// Checks if OrderTotal property is set
        /// </summary>
        /// <returns>true if OrderTotal property is set</returns>
        public Boolean IsSetOrderTotal()
        {
            return this.orderTotalField != null;
        }




        /// <summary>
        /// Gets and sets the NumberOfItemsShipped property.
        /// </summary>
        [XmlElementAttribute(ElementName = "NumberOfItemsShipped")]
        public Decimal NumberOfItemsShipped
        {
            get { return this.numberOfItemsShippedField.GetValueOrDefault() ; }
            set { this.numberOfItemsShippedField= value; }
        }



        /// <summary>
        /// Sets the NumberOfItemsShipped property
        /// </summary>
        /// <param name="numberOfItemsShipped">NumberOfItemsShipped property</param>
        /// <returns>this instance</returns>
        public Order WithNumberOfItemsShipped(Decimal numberOfItemsShipped)
        {
            this.numberOfItemsShippedField = numberOfItemsShipped;
            return this;
        }



        /// <summary>
        /// Checks if NumberOfItemsShipped property is set
        /// </summary>
        /// <returns>true if NumberOfItemsShipped property is set</returns>
        public Boolean IsSetNumberOfItemsShipped()
        {
            return  this.numberOfItemsShippedField.HasValue;

        }


        /// <summary>
        /// Gets and sets the NumberOfItemsUnshipped property.
        /// </summary>
        [XmlElementAttribute(ElementName = "NumberOfItemsUnshipped")]
        public Decimal NumberOfItemsUnshipped
        {
            get { return this.numberOfItemsUnshippedField.GetValueOrDefault() ; }
            set { this.numberOfItemsUnshippedField= value; }
        }



        /// <summary>
        /// Sets the NumberOfItemsUnshipped property
        /// </summary>
        /// <param name="numberOfItemsUnshipped">NumberOfItemsUnshipped property</param>
        /// <returns>this instance</returns>
        public Order WithNumberOfItemsUnshipped(Decimal numberOfItemsUnshipped)
        {
            this.numberOfItemsUnshippedField = numberOfItemsUnshipped;
            return this;
        }



        /// <summary>
        /// Checks if NumberOfItemsUnshipped property is set
        /// </summary>
        /// <returns>true if NumberOfItemsUnshipped property is set</returns>
        public Boolean IsSetNumberOfItemsUnshipped()
        {
            return  this.numberOfItemsUnshippedField.HasValue;

        }


        /// <summary>
        /// Gets and sets the MarketplaceId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "MarketplaceId")]
        public String MarketplaceId
        {
            get { return this.marketplaceIdField ; }
            set { this.marketplaceIdField= value; }
        }



        /// <summary>
        /// Sets the MarketplaceId property
        /// </summary>
        /// <param name="marketplaceId">MarketplaceId property</param>
        /// <returns>this instance</returns>
        public Order WithMarketplaceId(String marketplaceId)
        {
            this.marketplaceIdField = marketplaceId;
            return this;
        }



        /// <summary>
        /// Checks if MarketplaceId property is set
        /// </summary>
        /// <returns>true if MarketplaceId property is set</returns>
        public Boolean IsSetMarketplaceId()
        {
            return  this.marketplaceIdField != null;

        }


        /// <summary>
        /// Gets and sets the BuyerEmail property.
        /// </summary>
        [XmlElementAttribute(ElementName = "BuyerEmail")]
        public String BuyerEmail
        {
            get { return this.buyerEmailField ; }
            set { this.buyerEmailField= value; }
        }



        /// <summary>
        /// Sets the BuyerEmail property
        /// </summary>
        /// <param name="buyerEmail">BuyerEmail property</param>
        /// <returns>this instance</returns>
        public Order WithBuyerEmail(String buyerEmail)
        {
            this.buyerEmailField = buyerEmail;
            return this;
        }



        /// <summary>
        /// Checks if BuyerEmail property is set
        /// </summary>
        /// <returns>true if BuyerEmail property is set</returns>
        public Boolean IsSetBuyerEmail()
        {
            return  this.buyerEmailField != null;

        }


        /// <summary>
        /// Gets and sets the BuyerName property.
        /// </summary>
        [XmlElementAttribute(ElementName = "BuyerName")]
        public String BuyerName
        {
            get { return this.buyerNameField ; }
            set { this.buyerNameField= value; }
        }



        /// <summary>
        /// Sets the BuyerName property
        /// </summary>
        /// <param name="buyerName">BuyerName property</param>
        /// <returns>this instance</returns>
        public Order WithBuyerName(String buyerName)
        {
            this.buyerNameField = buyerName;
            return this;
        }



        /// <summary>
        /// Checks if BuyerName property is set
        /// </summary>
        /// <returns>true if BuyerName property is set</returns>
        public Boolean IsSetBuyerName()
        {
            return  this.buyerNameField != null;

        }


        /// <summary>
        /// Gets and sets the ShipmentServiceLevelCategory property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShipmentServiceLevelCategory")]
        public String ShipmentServiceLevelCategory
        {
            get { return this.shipmentServiceLevelCategoryField ; }
            set { this.shipmentServiceLevelCategoryField= value; }
        }



        /// <summary>
        /// Sets the ShipmentServiceLevelCategory property
        /// </summary>
        /// <param name="shipmentServiceLevelCategory">ShipmentServiceLevelCategory property</param>
        /// <returns>this instance</returns>
        public Order WithShipmentServiceLevelCategory(String shipmentServiceLevelCategory)
        {
            this.shipmentServiceLevelCategoryField = shipmentServiceLevelCategory;
            return this;
        }



        /// <summary>
        /// Checks if ShipmentServiceLevelCategory property is set
        /// </summary>
        /// <returns>true if ShipmentServiceLevelCategory property is set</returns>
        public Boolean IsSetShipmentServiceLevelCategory()
        {
            return  this.shipmentServiceLevelCategoryField != null;

        }




        /// <summary>
        /// XML fragment representation of this object
        /// </summary>
        /// <returns>XML fragment for this object.</returns>
        /// <remarks>
        /// Name for outer tag expected to be set by calling method. 
        /// This fragment returns inner properties representation only
        /// </remarks>


        protected internal String ToXMLFragment() {
            StringBuilder xml = new StringBuilder();
            if (IsSetAmazonOrderId()) {
                xml.Append("<AmazonOrderId>");
                xml.Append(EscapeXML(this.AmazonOrderId));
                xml.Append("</AmazonOrderId>");
            }
            if (IsSetSellerOrderId()) {
                xml.Append("<SellerOrderId>");
                xml.Append(EscapeXML(this.SellerOrderId));
                xml.Append("</SellerOrderId>");
            }
            if (IsSetPurchaseDate()) {
                xml.Append("<PurchaseDate>");
                xml.Append(this.PurchaseDate);
                xml.Append("</PurchaseDate>");
            }
            if (IsSetLastUpdateDate()) {
                xml.Append("<LastUpdateDate>");
                xml.Append(this.LastUpdateDate);
                xml.Append("</LastUpdateDate>");
            }
            if (IsSetOrderStatus()) {
                xml.Append("<OrderStatus>");
                xml.Append(this.OrderStatus);
                xml.Append("</OrderStatus>");
            }
            if (IsSetFulfillmentChannel()) {
                xml.Append("<FulfillmentChannel>");
                xml.Append(this.FulfillmentChannel);
                xml.Append("</FulfillmentChannel>");
            }
            if (IsSetSalesChannel()) {
                xml.Append("<SalesChannel>");
                xml.Append(EscapeXML(this.SalesChannel));
                xml.Append("</SalesChannel>");
            }
            if (IsSetOrderChannel()) {
                xml.Append("<OrderChannel>");
                xml.Append(EscapeXML(this.OrderChannel));
                xml.Append("</OrderChannel>");
            }
            if (IsSetShipServiceLevel()) {
                xml.Append("<ShipServiceLevel>");
                xml.Append(EscapeXML(this.ShipServiceLevel));
                xml.Append("</ShipServiceLevel>");
            }
            if (IsSetShippingAddress()) {
                Address  shippingAddressObj = this.ShippingAddress;
                xml.Append("<ShippingAddress>");
                xml.Append(shippingAddressObj.ToXMLFragment());
                xml.Append("</ShippingAddress>");
            } 
            if (IsSetOrderTotal()) {
                Money  orderTotalObj = this.OrderTotal;
                xml.Append("<OrderTotal>");
                xml.Append(orderTotalObj.ToXMLFragment());
                xml.Append("</OrderTotal>");
            } 
            if (IsSetNumberOfItemsShipped()) {
                xml.Append("<NumberOfItemsShipped>");
                xml.Append(this.NumberOfItemsShipped);
                xml.Append("</NumberOfItemsShipped>");
            }
            if (IsSetNumberOfItemsUnshipped()) {
                xml.Append("<NumberOfItemsUnshipped>");
                xml.Append(this.NumberOfItemsUnshipped);
                xml.Append("</NumberOfItemsUnshipped>");
            }
            if (IsSetMarketplaceId()) {
                xml.Append("<MarketplaceId>");
                xml.Append(EscapeXML(this.MarketplaceId));
                xml.Append("</MarketplaceId>");
            }
            if (IsSetBuyerEmail()) {
                xml.Append("<BuyerEmail>");
                xml.Append(EscapeXML(this.BuyerEmail));
                xml.Append("</BuyerEmail>");
            }
            if (IsSetBuyerName()) {
                xml.Append("<BuyerName>");
                xml.Append(EscapeXML(this.BuyerName));
                xml.Append("</BuyerName>");
            }
            if (IsSetShipmentServiceLevelCategory()) {
                xml.Append("<ShipmentServiceLevelCategory>");
                xml.Append(EscapeXML(this.ShipmentServiceLevelCategory));
                xml.Append("</ShipmentServiceLevelCategory>");
            }
            return xml.ToString();
        }

        /**
         * 
         * Escape XML special characters
         */
        private String EscapeXML(String str) {
            StringBuilder sb = new StringBuilder();
            foreach (Char c in str)
            {
                switch (c) {
                case '&':
                    sb.Append("&amp;");
                    break;
                case '<':
                    sb.Append("&lt;");
                    break;
                case '>':
                    sb.Append("&gt;");
                    break;
                case '\'':
                    sb.Append("&#039;");
                    break;
                case '"':
                    sb.Append("&quot;");
                    break;
                default:
                    sb.Append(c);
                    break;
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (IsSetAmazonOrderId())
            {
                result.Append("Order ID: ");
                result.AppendLine(AmazonOrderId);
            }
            if (IsSetFulfillmentChannel())
            {
                result.Append("FulfillmentChannel:");
                result.AppendLine(FulfillmentChannel.ToString());
            }
            if (IsSetNumberOfItemsShipped())
            {
                result.Append("Number of Items Shipped: ");
                result.AppendLine(NumberOfItemsShipped.ToString());
            }
            if (IsSetNumberOfItemsUnshipped())
            {
                result.Append("Number of Items Not Shipped: ");
                result.AppendLine(NumberOfItemsShipped.ToString());
            }
            if (IsSetLastUpdateDate())
            {
                result.Append("Last Update Date: ");
                result.AppendLine(LastUpdateDate.ToString());
            }
            if (IsSetOrderChannel())
            {
                result.Append("Order Channel: ");
                result.AppendLine(OrderChannel);
            }
            if (IsSetOrderStatus())
            {
                result.Append("Order Status: ");
                result.AppendLine(OrderStatus.ToString());
            }
            if (IsSetOrderTotal())
            {
                result.Append("Order Total: ");
                result.AppendLine(OrderTotal.ToString());
            }
            if (IsSetPurchaseDate())
            {
                result.Append("Purchase Date: ");
                result.AppendLine(PurchaseDate.ToString());
            }
            if (IsSetSalesChannel())
            {
                result.Append("Sales Channel: ");
                result.AppendLine(SalesChannel);
            }
            if (IsSetSellerOrderId())
            {
                result.Append("Seller Order Id: ");
                result.AppendLine(SellerOrderId);
            }
            if (IsSetShipServiceLevel())
            {
                result.Append("Ship Service Level: ");
                result.AppendLine(ShipServiceLevel);
            }
            if (IsSetMarketplaceId())
            {
                result.Append("Marketplace Id: ");
                result.AppendLine(MarketplaceId);
            }
            if (IsSetBuyerEmail())
            {
                result.Append("Buyer Email: ");
                result.AppendLine(BuyerEmail);
            }
            if (IsSetShipServiceLevel())
            {
                result.Append("Buyer Name: ");
                result.AppendLine(BuyerName);
            }
            if (IsSetShipmentServiceLevelCategory())
            {
                result.Append("Shipment Service Level Category: ");
                result.AppendLine(ShipmentServiceLevelCategory);
            }
            return result.ToString();
        }


    }

}