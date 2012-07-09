using System;
using System.Collections.Generic;
using Microsoft.Dynamics.BusinessConnectorNet;

namespace CandyDirect.AppServices
{
	public class SalesOrder
	{
		List<SalesLine> _lineItems;
		
		public SalesOrder()
		{
			_lineItems = new List<SalesLine>();
		}
		
		public string OrderId {get; set;}
		public string NativeId {get; set;}
		public string DeliveryCustomerName {get; set;}
		public string DeliveryStreet {get; set;}
		public string DeliveryCity {get; set;}
		public string DeliveryState {get; set;}
		public string DeliveryZip {get; set;}
		public string DeliveryCountry {get; set;}
		public string BillToCustomerName {get; set;}
		public string CustomerEmail {get; set;}
		public string BillToStreet {get; set;}
		public string BillToCity {get; set;}
		public string BillToState {get; set;}
		public string BillToZip {get; set;}
		public string BillToCountry {get; set;}
		public string StoreStatus {get; set;}
		public DateTime StoreCreatedAt {get; set;}
		public DateTime StoreUpdatedAt {get; set;}
		 
		public string GiftMessageFrom {get; set;}
		public string GiftMessageTo {get; set;}
		public string GiftMessageBody {get; set;}
		public string CustomerOrderComment {get; set;}
		public string DeliveryMode {get; set;}
		public string ShippingMethodDescription {get; set;}
		public string CouponCode {get; set;}
		public string EndDiscount {get; set;} // RWC if "Registered Wholesales Customer" on Magento
		public string ShippingChargeCode {get;set;}
		public decimal ShippingChargeAmount {get; set;}
		public string PaymentMethod {get; set;}
				
		public List<SalesLine> LineItems { get {return _lineItems;}}
		
		public void AddLineItem(string itemSku, string itemName, decimal quantity, decimal salesPrice, decimal storeTotal, string unitOfMeasure, decimal axPrice, decimal storeDiscountPrice = 0.0m)
		{
			var price = axPrice > 0 ? axPrice : salesPrice;
			//in magento store discount is total for line, in AX its per item
			//var discPerItem = storeDiscountPrice / quantity;
			
			//If store base price is not equal to AX base price then set a discount so total matches tore
			var discount = (price - salesPrice);
			//only add store discount if they are Not wholesale customer, AX needs to calc wholesale incase amounts change
			if(this.EndDiscount != "RWC")
				discount += storeDiscountPrice / quantity;
			 
			var lineTotal = this.EndDiscount == "RWC" ? storeTotal : storeTotal - storeDiscountPrice;
			
			var calcTotal = ((axPrice - discount) * quantity);
			if(calcTotal != storeTotal)
				NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Price calc error for orderId: {0} itemId: {1} calcTotal: {2} storeTotal: {3} storeDiscount: {4}", OrderId, itemSku, calcTotal, storeTotal, storeDiscountPrice));
			_lineItems.Add(new SalesLine{ 
			               	OrderId = this.OrderId,
			               	LineNumber = ((decimal)(this._lineItems.Count + 1m)),
			               	ItemSku = itemSku,
			               	ItemName = itemName,
			               	Quantity = quantity,
			               	Price = price ,
			               	StoreTotal = lineTotal,
			               	UnitOfMeasure = unitOfMeasure,
			               	LineDiscount = discount 
			               	//don't use this as it doesn't calc correctly for partial shipments SalesMarkup = storeDiscountPrice * -1
			               });
		}
		
	}
	
	public class SalesLine
	{
		public string OrderId {get; set;}
		public decimal LineNumber {get; set;}
		public string ItemSku {get; set;}
		public string ItemName {get; set;}
		public decimal Quantity {get; set;}
		public decimal Price {get; set;}
		public decimal StoreTotal {get; set;}
		//for this to be right needs to take into account discounts use storeTotal public decimal CalculatedTotal  {get {return Quantity * Price;}}
		public string UnitOfMeasure {get; set;}
		public decimal LinePercent {get; set;}
		public decimal LineDiscount {get;set;}
		public decimal SalesMarkup {get; set;}
			
	}
	public static class AxSalesOrder
	{
		public static void BuildDefaults(AxaptaRecord rec, string storeName)
		{
			
            
            rec.set_Field(AxSalesOrder.CustomerAccountId, storeName);
            rec.set_Field(AxSalesOrder.InvoiceAccountId, storeName);
            rec.set_Field(AxSalesOrder.CurrencyCode, "USD");
            rec.set_Field(AxSalesOrder.Payment, "CBS");
            rec.set_Field(AxSalesOrder.CustomerGroup, "Magento");
            rec.set_Field(AxSalesOrder.SalesType, 3);
            rec.set_Field(AxSalesOrder.InventoryLocationId, "CV");
			rec.set_Field(AxSalesOrder.InventorySiteId, "CV");
			rec.set_Field(AxSalesOrder.SalesStatus, 1);
			rec.set_Field(AxSalesOrder.ShippingDateRequested,DateTime.Now);
			rec.set_Field(AxSalesOrder.LanguageId, "EN-US");
		}
		
		public static void LineBuildDefaults(AxaptaRecord rec, string storeName)
		{
			rec.set_Field(AxSalesOrder.SalesStatus, 1);
    		rec.set_Field(AxSalesOrder.CustomerGroup, storeName);
    		rec.set_Field(AxSalesOrder.CustomerAccountId, storeName);
    		rec.set_Field(AxSalesOrder.LineInvetoryDimId, "000001");
    		rec.set_Field(AxSalesOrder.CurrencyCode, "USD");
    		rec.set_Field(AxSalesOrder.SalesType, 3);
    		rec.set_Field(AxSalesOrder.ShippingDateRequested, DateTime.Now);
    		rec.set_Field(AxSalesOrder.LinePriceUnit, 1.00m);
		}
		
		public static void MarkupTransBuildDefaults(AxaptaRecord rec)
		{
			rec.set_Field(AxSalesOrder.TransTableId, 366);
    		rec.set_Field(AxSalesOrder.LineNum, 1.0);
    		rec.set_Field(AxSalesOrder.CurrencyCode, "USD");
    		rec.set_Field(AxSalesOrder.ModuleType, 1);
    		
		}
		
		
		
		public static string SalesId = "SalesId";
		public static string SalesName = "SalesName";
		public static string Email = "Email";
		public static string CustomerAccountId = "CustAccount";
		public static string InvoiceAccountId = "InvoiceAccount";
		public static string DeliveryAddress = "DeliveryAddress";
		public static string CurrencyCode = "CurrencyCode";
		public static string Payment = "Payment";//CBS
		public static string CustomerGroup = "CustGroup"; //Magento or Amazon
		public static string SalesType = "SalesType"; // 3
		public static string InventoryLocationId = "InventLocationId"; //CV
		public static string InventorySiteId = "InventSiteId";//CV
		public static string SalesStatus = "SalesStatus"; //1
		public static string LanguageId = "LanguageId";//EN-US
		public static string ShippingDateRequested = "ShippingDateRequested";
		public static string DeliveryZipCode = "DeliveryZipCode";
		public static string DeliveryCountryRegionId = "DeliveryCountryRegionId";
		public static string DeliveryState = "DeliveryState";
		public static string DeliveryName = "DeliveryName";
		public static string DeliveryCity = "DeliveryCity";
		public static string DeliveryStreet = "DeliveryStreet";
		//new fields
		public static string OrderDate = "OrderDate";
		public static string GiftMessageFrom ="GiftMessageFrom";
		public static string GiftMessageTo = "GiftMessageTo";
		public static string GiftMessageBody = "GiftMessageBody";
		public static string CustomerOrderComment = "CustomerOrderComment";
		public static string DeliveryMode = "DlvMode";
		public static string ShippingMethodDescription = "ShippingMethodDescription";
		public static string EndDiscount = "EndDisc"; // RWC if "Registered Wholesales Customer" on Magento
		public static string PaymentMethod = "PaymMode";
		
		public static string LineNumber = "LineNum";
		public static string LineItemId = "ItemId";
		public static string LineItemName = "Name";
		public static string LineQuantityOrdered = "QtyOrdered";
		public static string LineRemainSalesPhysical = "RemainSalesPhysical";
		public static string LineInvetoryDimId = "InventDimId"; // 000001
		public static string LineSealesPrice = "SalesPrice";
		public static string LineAmount = "LineAmount";
		public static string LineSalesUnit = "SalesUnit";
		public static string LinePriceUnit = "PriceUnit";
		public static string LineRemainInventoryPhyscal = "RemainInventPhysical";
		public static string LineSalesQuantity = "SalesQty";
		//new fields
		public static string LinePercent = "LinePercent"; // discount percent
		public static string LineDiscount = "LineDisc"; //discount
		public static string LineSalesMarkup = "SalesMarkup";
		
		//Misc Charges table MarkupTrans
		public static string TransTableId = "TransTableId"; //366
		public static string TransRecId = "TransRecId"; // RecId of the sales order header
		public static string LineNum = "LineNum"; //1
		public static string MarkUpCode = "MarkUpCode"; // FREIGHT, FREESHIP
		//public static string CurrencyCode = "CurrencyCode"; //USD
		public static string MarkupTransValue = "Value"; 		
		public static string MarkupTransTxt = "Txt"; //Freight, Free_Shipping_now
		public static string ModuleType = "ModuleType"; // 1
			
		//Address in the globabl address table
		public static string AddrTableId = "AddrTableId"; //related table 366 for SalesTable
		public static string AddrRecId = "AddrRecId"; //RecId of sales order header
		public static string AddressType = "Type"; //invoice = 1
		
		public static string AddressName = "Name";
		public static string AddressFullAddress = "Address";
		public static string AddressZipCode = "ZipCode";
		public static string AddressCountryRegionId = "CountryRegionId";
		public static string AddressState = "State";
		public static string AddressCity = "City";
		public static string AddressStreet = "Street";
	}
	
	public static class AxShippingMethods
	{
		public static string s2Day = "2 Day";
		public static string s3_4Day = "3-4";
		public static string s5_8Day = "5-8";
		public static string LTL = "LTL";
		public static string Standard = "Standard";
		public static string UpsNextDay = "UPSNext";
		public static string UpsNextDayAm = "UPSNxtAM";
		public static string UpsNxtDaySaver = "UPSNxtSav";
		public static string UspsFlateRate = "USPS Pr";
		public static string FreeShipping = "Free";
		public static string TBD = "TBD";
	}
}
