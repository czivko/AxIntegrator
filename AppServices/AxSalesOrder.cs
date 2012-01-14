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
		public string CustomerName {get; set;}
		public string Street {get; set;}
		public string City {get; set;}
		public string State {get; set;}
		public string Zip {get; set;}
		public string Country {get; set;}
		
		public List<SalesLine> LineItems { get {return _lineItems;}}
		
		public void AddLineItem(string itemSku, string itemName, decimal quantity, decimal price, decimal storeTotal, string unitOfMeasure)
		{
			_lineItems.Add(new SalesLine{ 
			               	OrderId = this.OrderId,
			               	LineNumber = ((decimal)(this._lineItems.Count + 1m)),
			               	ItemSku = itemSku,
			               	ItemName = itemSku,
			               	Quantity = quantity,
			               	Price = price,
			               	StoreTotal = storeTotal,
			               	UnitOfMeasure = unitOfMeasure
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
		public decimal CalculatedTotal  {get {return Quantity * Price;}}
		public string UnitOfMeasure {get; set;}
			
	}
	public static class AxSalesOrder
	{
		public static void BuildDefaults(AxaptaRecord rec)
		{
			
            rec.set_Field(AxSalesOrder.SalesName, "Charles Zivko");
            rec.set_Field(AxSalesOrder.CustomerAccountId, "magento");
            rec.set_Field(AxSalesOrder.InvoiceAccountId, "magento");
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
		
		public static void LineBuildDefaults(AxaptaRecord rec)
		{
			rec.set_Field(AxSalesOrder.SalesStatus, 1);
    		rec.set_Field(AxSalesOrder.CustomerGroup, "Magento");
    		rec.set_Field(AxSalesOrder.CustomerAccountId, "magetno");
    		rec.set_Field(AxSalesOrder.LineInvetoryDimId, "000001");
    		rec.set_Field(AxSalesOrder.CurrencyCode, "USD");
    		rec.set_Field(AxSalesOrder.SalesType, 3);
    		rec.set_Field(AxSalesOrder.ShippingDateRequested, DateTime.Now);
    		rec.set_Field(AxSalesOrder.LinePriceUnit, 1.00m);
		}
		
		public static string SalesId = "SalesId";
		public static string SalesName = "SalesName";
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
		
		
		
	}
}
