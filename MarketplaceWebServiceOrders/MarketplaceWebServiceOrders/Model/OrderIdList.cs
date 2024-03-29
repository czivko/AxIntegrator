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
    public class OrderIdList
    {
    
        private List<String> idField;


        /// <summary>
        /// Gets and sets the Id property.
        /// </summary>
        [XmlElementAttribute(ElementName = "Id")]
        public List<String> Id
        {
            get
            {
                if (this.idField == null)
                {
                    this.idField = new List<String>();
                }
                return this.idField;
            }
            set { this.idField =  value; }
        }



        /// <summary>
        /// Sets the Id property
        /// </summary>
        /// <param name="list">Id property</param>
        /// <returns>this instance</returns>
        public OrderIdList WithId(params String[] list)
        {
            foreach (String item in list)
            {
                Id.Add(item);
            }
            return this;
        }          
 


        /// <summary>
        /// Checks of Id property is set
        /// </summary>
        /// <returns>true if Id property is set</returns>
        public Boolean IsSetId()
        {
            return (Id.Count > 0);
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
            List<String> idObjList  =  this.Id;
            foreach (String idObj in idObjList) { 
                xml.Append("<Id>");
                xml.Append(EscapeXML(idObj));
                xml.Append("</Id>");
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



    }

}