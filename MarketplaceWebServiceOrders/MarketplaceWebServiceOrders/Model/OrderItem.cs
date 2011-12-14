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
    public class OrderItem
    {
    
        private String ASINField;

        private String sellerSKUField;

        private String orderItemIdField;

        private String titleField;

        private Decimal? quantityOrderedField;

        private Decimal? quantityShippedField;

        private  Money itemPriceField;
        private  Money shippingPriceField;
        private  Money giftWrapPriceField;
        private  Money itemTaxField;
        private  Money shippingTaxField;
        private  Money giftWrapTaxField;
        private  Money shippingDiscountField;
        private  Money promotionDiscountField;
        private  PromotionIdList promotionIdsField;
        private String giftMessageTextField;

        private String giftWrapLevelField;


        /// <summary>
        /// Gets and sets the ASIN property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ASIN")]
        public String ASIN
        {
            get { return this.ASINField ; }
            set { this.ASINField= value; }
        }



        /// <summary>
        /// Sets the ASIN property
        /// </summary>
        /// <param name="ASIN">ASIN property</param>
        /// <returns>this instance</returns>
        public OrderItem WithASIN(String ASIN)
        {
            this.ASINField = ASIN;
            return this;
        }



        /// <summary>
        /// Checks if ASIN property is set
        /// </summary>
        /// <returns>true if ASIN property is set</returns>
        public Boolean IsSetASIN()
        {
            return  this.ASINField != null;

        }


        /// <summary>
        /// Gets and sets the SellerSKU property.
        /// </summary>
        [XmlElementAttribute(ElementName = "SellerSKU")]
        public String SellerSKU
        {
            get { return this.sellerSKUField ; }
            set { this.sellerSKUField= value; }
        }



        /// <summary>
        /// Sets the SellerSKU property
        /// </summary>
        /// <param name="sellerSKU">SellerSKU property</param>
        /// <returns>this instance</returns>
        public OrderItem WithSellerSKU(String sellerSKU)
        {
            this.sellerSKUField = sellerSKU;
            return this;
        }



        /// <summary>
        /// Checks if SellerSKU property is set
        /// </summary>
        /// <returns>true if SellerSKU property is set</returns>
        public Boolean IsSetSellerSKU()
        {
            return  this.sellerSKUField != null;

        }


        /// <summary>
        /// Gets and sets the OrderItemId property.
        /// </summary>
        [XmlElementAttribute(ElementName = "OrderItemId")]
        public String OrderItemId
        {
            get { return this.orderItemIdField ; }
            set { this.orderItemIdField= value; }
        }



        /// <summary>
        /// Sets the OrderItemId property
        /// </summary>
        /// <param name="orderItemId">OrderItemId property</param>
        /// <returns>this instance</returns>
        public OrderItem WithOrderItemId(String orderItemId)
        {
            this.orderItemIdField = orderItemId;
            return this;
        }



        /// <summary>
        /// Checks if OrderItemId property is set
        /// </summary>
        /// <returns>true if OrderItemId property is set</returns>
        public Boolean IsSetOrderItemId()
        {
            return  this.orderItemIdField != null;

        }


        /// <summary>
        /// Gets and sets the Title property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Title")]
        public String Title
        {
            get { return this.titleField ; }
            set { this.titleField= value; }
        }



        /// <summary>
        /// Sets the Title property
        /// </summary>
        /// <param name="title">Title property</param>
        /// <returns>this instance</returns>
        public OrderItem WithTitle(String title)
        {
            this.titleField = title;
            return this;
        }



        /// <summary>
        /// Checks if Title property is set
        /// </summary>
        /// <returns>true if Title property is set</returns>
        public Boolean IsSetTitle()
        {
            return  this.titleField != null;

        }


        /// <summary>
        /// Gets and sets the QuantityOrdered property.
        /// </summary>
        [XmlElementAttribute(ElementName = "QuantityOrdered")]
        public Decimal QuantityOrdered
        {
            get { return this.quantityOrderedField.GetValueOrDefault() ; }
            set { this.quantityOrderedField= value; }
        }



        /// <summary>
        /// Sets the QuantityOrdered property
        /// </summary>
        /// <param name="quantityOrdered">QuantityOrdered property</param>
        /// <returns>this instance</returns>
        public OrderItem WithQuantityOrdered(Decimal quantityOrdered)
        {
            this.quantityOrderedField = quantityOrdered;
            return this;
        }



        /// <summary>
        /// Checks if QuantityOrdered property is set
        /// </summary>
        /// <returns>true if QuantityOrdered property is set</returns>
        public Boolean IsSetQuantityOrdered()
        {
            return  this.quantityOrderedField.HasValue;

        }


        /// <summary>
        /// Gets and sets the QuantityShipped property.
        /// </summary>
        [XmlElementAttribute(ElementName = "QuantityShipped")]
        public Decimal QuantityShipped
        {
            get { return this.quantityShippedField.GetValueOrDefault() ; }
            set { this.quantityShippedField= value; }
        }



        /// <summary>
        /// Sets the QuantityShipped property
        /// </summary>
        /// <param name="quantityShipped">QuantityShipped property</param>
        /// <returns>this instance</returns>
        public OrderItem WithQuantityShipped(Decimal quantityShipped)
        {
            this.quantityShippedField = quantityShipped;
            return this;
        }



        /// <summary>
        /// Checks if QuantityShipped property is set
        /// </summary>
        /// <returns>true if QuantityShipped property is set</returns>
        public Boolean IsSetQuantityShipped()
        {
            return  this.quantityShippedField.HasValue;

        }


        /// <summary>
        /// Gets and sets the ItemPrice property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ItemPrice")]
        public Money ItemPrice
        {
            get { return this.itemPriceField ; }
            set { this.itemPriceField = value; }
        }



        /// <summary>
        /// Sets the ItemPrice property
        /// </summary>
        /// <param name="itemPrice">ItemPrice property</param>
        /// <returns>this instance</returns>
        public OrderItem WithItemPrice(Money itemPrice)
        {
            this.itemPriceField = itemPrice;
            return this;
        }



        /// <summary>
        /// Checks if ItemPrice property is set
        /// </summary>
        /// <returns>true if ItemPrice property is set</returns>
        public Boolean IsSetItemPrice()
        {
            return this.itemPriceField != null;
        }




        /// <summary>
        /// Gets and sets the ShippingPrice property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShippingPrice")]
        public Money ShippingPrice
        {
            get { return this.shippingPriceField ; }
            set { this.shippingPriceField = value; }
        }



        /// <summary>
        /// Sets the ShippingPrice property
        /// </summary>
        /// <param name="shippingPrice">ShippingPrice property</param>
        /// <returns>this instance</returns>
        public OrderItem WithShippingPrice(Money shippingPrice)
        {
            this.shippingPriceField = shippingPrice;
            return this;
        }



        /// <summary>
        /// Checks if ShippingPrice property is set
        /// </summary>
        /// <returns>true if ShippingPrice property is set</returns>
        public Boolean IsSetShippingPrice()
        {
            return this.shippingPriceField != null;
        }




        /// <summary>
        /// Gets and sets the GiftWrapPrice property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GiftWrapPrice")]
        public Money GiftWrapPrice
        {
            get { return this.giftWrapPriceField ; }
            set { this.giftWrapPriceField = value; }
        }



        /// <summary>
        /// Sets the GiftWrapPrice property
        /// </summary>
        /// <param name="giftWrapPrice">GiftWrapPrice property</param>
        /// <returns>this instance</returns>
        public OrderItem WithGiftWrapPrice(Money giftWrapPrice)
        {
            this.giftWrapPriceField = giftWrapPrice;
            return this;
        }



        /// <summary>
        /// Checks if GiftWrapPrice property is set
        /// </summary>
        /// <returns>true if GiftWrapPrice property is set</returns>
        public Boolean IsSetGiftWrapPrice()
        {
            return this.giftWrapPriceField != null;
        }




        /// <summary>
        /// Gets and sets the ItemTax property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ItemTax")]
        public Money ItemTax
        {
            get { return this.itemTaxField ; }
            set { this.itemTaxField = value; }
        }



        /// <summary>
        /// Sets the ItemTax property
        /// </summary>
        /// <param name="itemTax">ItemTax property</param>
        /// <returns>this instance</returns>
        public OrderItem WithItemTax(Money itemTax)
        {
            this.itemTaxField = itemTax;
            return this;
        }



        /// <summary>
        /// Checks if ItemTax property is set
        /// </summary>
        /// <returns>true if ItemTax property is set</returns>
        public Boolean IsSetItemTax()
        {
            return this.itemTaxField != null;
        }




        /// <summary>
        /// Gets and sets the ShippingTax property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShippingTax")]
        public Money ShippingTax
        {
            get { return this.shippingTaxField ; }
            set { this.shippingTaxField = value; }
        }



        /// <summary>
        /// Sets the ShippingTax property
        /// </summary>
        /// <param name="shippingTax">ShippingTax property</param>
        /// <returns>this instance</returns>
        public OrderItem WithShippingTax(Money shippingTax)
        {
            this.shippingTaxField = shippingTax;
            return this;
        }



        /// <summary>
        /// Checks if ShippingTax property is set
        /// </summary>
        /// <returns>true if ShippingTax property is set</returns>
        public Boolean IsSetShippingTax()
        {
            return this.shippingTaxField != null;
        }




        /// <summary>
        /// Gets and sets the GiftWrapTax property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GiftWrapTax")]
        public Money GiftWrapTax
        {
            get { return this.giftWrapTaxField ; }
            set { this.giftWrapTaxField = value; }
        }



        /// <summary>
        /// Sets the GiftWrapTax property
        /// </summary>
        /// <param name="giftWrapTax">GiftWrapTax property</param>
        /// <returns>this instance</returns>
        public OrderItem WithGiftWrapTax(Money giftWrapTax)
        {
            this.giftWrapTaxField = giftWrapTax;
            return this;
        }



        /// <summary>
        /// Checks if GiftWrapTax property is set
        /// </summary>
        /// <returns>true if GiftWrapTax property is set</returns>
        public Boolean IsSetGiftWrapTax()
        {
            return this.giftWrapTaxField != null;
        }




        /// <summary>
        /// Gets and sets the ShippingDiscount property.
        /// </summary>
        [XmlElementAttribute(ElementName = "ShippingDiscount")]
        public Money ShippingDiscount
        {
            get { return this.shippingDiscountField ; }
            set { this.shippingDiscountField = value; }
        }



        /// <summary>
        /// Sets the ShippingDiscount property
        /// </summary>
        /// <param name="shippingDiscount">ShippingDiscount property</param>
        /// <returns>this instance</returns>
        public OrderItem WithShippingDiscount(Money shippingDiscount)
        {
            this.shippingDiscountField = shippingDiscount;
            return this;
        }



        /// <summary>
        /// Checks if ShippingDiscount property is set
        /// </summary>
        /// <returns>true if ShippingDiscount property is set</returns>
        public Boolean IsSetShippingDiscount()
        {
            return this.shippingDiscountField != null;
        }




        /// <summary>
        /// Gets and sets the PromotionDiscount property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PromotionDiscount")]
        public Money PromotionDiscount
        {
            get { return this.promotionDiscountField ; }
            set { this.promotionDiscountField = value; }
        }



        /// <summary>
        /// Sets the PromotionDiscount property
        /// </summary>
        /// <param name="promotionDiscount">PromotionDiscount property</param>
        /// <returns>this instance</returns>
        public OrderItem WithPromotionDiscount(Money promotionDiscount)
        {
            this.promotionDiscountField = promotionDiscount;
            return this;
        }



        /// <summary>
        /// Checks if PromotionDiscount property is set
        /// </summary>
        /// <returns>true if PromotionDiscount property is set</returns>
        public Boolean IsSetPromotionDiscount()
        {
            return this.promotionDiscountField != null;
        }




        /// <summary>
        /// Gets and sets the PromotionIds property.
        /// </summary>
        [XmlElementAttribute(ElementName = "PromotionIds")]
        public PromotionIdList PromotionIds
        {
            get { return this.promotionIdsField ; }
            set { this.promotionIdsField = value; }
        }



        /// <summary>
        /// Sets the PromotionIds property
        /// </summary>
        /// <param name="promotionIds">PromotionIds property</param>
        /// <returns>this instance</returns>
        public OrderItem WithPromotionIds(PromotionIdList promotionIds)
        {
            this.promotionIdsField = promotionIds;
            return this;
        }



        /// <summary>
        /// Checks if PromotionIds property is set
        /// </summary>
        /// <returns>true if PromotionIds property is set</returns>
        public Boolean IsSetPromotionIds()
        {
            return this.promotionIdsField != null;
        }




        /// <summary>
        /// Gets and sets the GiftMessageText property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GiftMessageText")]
        public String GiftMessageText
        {
            get { return this.giftMessageTextField ; }
            set { this.giftMessageTextField= value; }
        }



        /// <summary>
        /// Sets the GiftMessageText property
        /// </summary>
        /// <param name="giftMessageText">GiftMessageText property</param>
        /// <returns>this instance</returns>
        public OrderItem WithGiftMessageText(String giftMessageText)
        {
            this.giftMessageTextField = giftMessageText;
            return this;
        }



        /// <summary>
        /// Checks if GiftMessageText property is set
        /// </summary>
        /// <returns>true if GiftMessageText property is set</returns>
        public Boolean IsSetGiftMessageText()
        {
            return  this.giftMessageTextField != null;

        }


        /// <summary>
        /// Gets and sets the GiftWrapLevel property.
        /// </summary>
        [XmlElementAttribute(ElementName = "GiftWrapLevel")]
        public String GiftWrapLevel
        {
            get { return this.giftWrapLevelField ; }
            set { this.giftWrapLevelField= value; }
        }



        /// <summary>
        /// Sets the GiftWrapLevel property
        /// </summary>
        /// <param name="giftWrapLevel">GiftWrapLevel property</param>
        /// <returns>this instance</returns>
        public OrderItem WithGiftWrapLevel(String giftWrapLevel)
        {
            this.giftWrapLevelField = giftWrapLevel;
            return this;
        }



        /// <summary>
        /// Checks if GiftWrapLevel property is set
        /// </summary>
        /// <returns>true if GiftWrapLevel property is set</returns>
        public Boolean IsSetGiftWrapLevel()
        {
            return  this.giftWrapLevelField != null;

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
            if (IsSetASIN()) {
                xml.Append("<ASIN>");
                xml.Append(EscapeXML(this.ASIN));
                xml.Append("</ASIN>");
            }
            if (IsSetSellerSKU()) {
                xml.Append("<SellerSKU>");
                xml.Append(EscapeXML(this.SellerSKU));
                xml.Append("</SellerSKU>");
            }
            if (IsSetOrderItemId()) {
                xml.Append("<OrderItemId>");
                xml.Append(EscapeXML(this.OrderItemId));
                xml.Append("</OrderItemId>");
            }
            if (IsSetTitle()) {
                xml.Append("<Title>");
                xml.Append(EscapeXML(this.Title));
                xml.Append("</Title>");
            }
            if (IsSetQuantityOrdered()) {
                xml.Append("<QuantityOrdered>");
                xml.Append(this.QuantityOrdered);
                xml.Append("</QuantityOrdered>");
            }
            if (IsSetQuantityShipped()) {
                xml.Append("<QuantityShipped>");
                xml.Append(this.QuantityShipped);
                xml.Append("</QuantityShipped>");
            }
            if (IsSetItemPrice()) {
                Money  itemPriceObj = this.ItemPrice;
                xml.Append("<ItemPrice>");
                xml.Append(itemPriceObj.ToXMLFragment());
                xml.Append("</ItemPrice>");
            } 
            if (IsSetShippingPrice()) {
                Money  shippingPriceObj = this.ShippingPrice;
                xml.Append("<ShippingPrice>");
                xml.Append(shippingPriceObj.ToXMLFragment());
                xml.Append("</ShippingPrice>");
            } 
            if (IsSetGiftWrapPrice()) {
                Money  giftWrapPriceObj = this.GiftWrapPrice;
                xml.Append("<GiftWrapPrice>");
                xml.Append(giftWrapPriceObj.ToXMLFragment());
                xml.Append("</GiftWrapPrice>");
            } 
            if (IsSetItemTax()) {
                Money  itemTaxObj = this.ItemTax;
                xml.Append("<ItemTax>");
                xml.Append(itemTaxObj.ToXMLFragment());
                xml.Append("</ItemTax>");
            } 
            if (IsSetShippingTax()) {
                Money  shippingTaxObj = this.ShippingTax;
                xml.Append("<ShippingTax>");
                xml.Append(shippingTaxObj.ToXMLFragment());
                xml.Append("</ShippingTax>");
            } 
            if (IsSetGiftWrapTax()) {
                Money  giftWrapTaxObj = this.GiftWrapTax;
                xml.Append("<GiftWrapTax>");
                xml.Append(giftWrapTaxObj.ToXMLFragment());
                xml.Append("</GiftWrapTax>");
            } 
            if (IsSetShippingDiscount()) {
                Money  shippingDiscountObj = this.ShippingDiscount;
                xml.Append("<ShippingDiscount>");
                xml.Append(shippingDiscountObj.ToXMLFragment());
                xml.Append("</ShippingDiscount>");
            } 
            if (IsSetPromotionDiscount()) {
                Money  promotionDiscountObj = this.PromotionDiscount;
                xml.Append("<PromotionDiscount>");
                xml.Append(promotionDiscountObj.ToXMLFragment());
                xml.Append("</PromotionDiscount>");
            } 
            if (IsSetPromotionIds()) {
                PromotionIdList  promotionIdsObj = this.PromotionIds;
                xml.Append("<PromotionIds>");
                xml.Append(promotionIdsObj.ToXMLFragment());
                xml.Append("</PromotionIds>");
            } 
            if (IsSetGiftMessageText()) {
                xml.Append("<GiftMessageText>");
                xml.Append(EscapeXML(this.GiftMessageText));
                xml.Append("</GiftMessageText>");
            }
            if (IsSetGiftWrapLevel()) {
                xml.Append("<GiftWrapLevel>");
                xml.Append(EscapeXML(this.GiftWrapLevel));
                xml.Append("</GiftWrapLevel>");
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
            if (IsSetTitle())
            {
                result.Append("Title: ");
                result.AppendLine(Title);
            }
            if (IsSetASIN())
            {
                result.Append("ASIN: ");
                result.AppendLine(ASIN);
            }
            if (IsSetSellerSKU())
            {
                result.Append("SKU: ");
                result.AppendLine(SellerSKU);
            } 
            if (IsSetItemPrice())
            {
                result.Append("Price: ");
                result.AppendLine(ItemPrice.ToString());
            }
            if (IsSetItemTax())
            {
                result.Append("Item Tax: ");
                result.AppendLine(ItemTax.ToString());
            }
            if (IsSetPromotionDiscount())
            {
                result.Append("Promotions Discount: ");
                result.AppendLine(PromotionDiscount.ToString());
            }
            if (IsSetPromotionIds())
            {
                if (PromotionIds.PromotionId != null)
                {
                    foreach (string promotionId in PromotionIds.PromotionId)
                    {
                        result.Append("\tPromotion Id: ");
                        result.AppendLine(promotionId);
                    }
                }
            }
            if (IsSetShippingDiscount())
            {
                result.Append("Shipping Discount: ");
                result.AppendLine(ShippingDiscount.ToString());
            }
            if (IsSetQuantityOrdered())
            {
                result.Append("Quantity: ");
                result.AppendLine(QuantityOrdered.ToString());
            }
            if (IsSetQuantityShipped())
            {
                result.Append("Quantity Shipped: ");
                result.AppendLine(QuantityShipped.ToString());
            }
            if (IsSetGiftMessageText())
            {
                result.Append("Gift Message Text: ");
                result.AppendLine(GiftMessageText);
            }
            if (IsSetGiftWrapPrice())
            {
                result.Append("Gift Wrap Price: ");
                result.AppendLine(GiftWrapPrice.ToString());
            }
            if (IsSetGiftWrapLevel())
            {
                result.Append("Gift Wrap Level: ");
                result.AppendLine(GiftWrapLevel.ToString());
            }
            return result.ToString();
        }

    }

}