using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using CandyDirect.AppServices.DB;
using Microsoft.Dynamics.BusinessConnectorNet;

namespace CandyDirect.AppServices
{
	public class OrderService
	{
		private Axapta Login()
		{		
			var ax = new Axapta();
			var adUser = System.Configuration.ConfigurationManager.AppSettings["AxUserName"];
			var adPass = System.Configuration.ConfigurationManager.AppSettings["AxUserPass"];
			var aos = System.Configuration.ConfigurationManager.AppSettings["AxObjectServer"];
			if(adUser == null || adPass == null)
				throw new ArgumentNullException("AxUserName or AxUserPass is missing from <appsettings> in the config file");
			
			if(aos == null)
				throw new ArgumentNullException("AxObjectServer is missing from <appsettings> in the config file. Sample: 'company1@AOS:2713'");
			
			System.Net.NetworkCredential creds = new System.Net.NetworkCredential(
				adUser,adPass, "candydirect.com");
				ax.LogonAs(adUser,"candydirect.com",creds,null,null,aos,null);
	        //ax.Logon(null, null, null, null);
	        return ax;
		}
		
		public void ProcessNewOrders()
		{
			ProcessNewMagentoOrders();
			ProcessAmazonOrders();
		}
		
		public void ProcessAmazonOrders()
		{
			var store = new AmazonStore();
			
			var newOrders = store.GetNewOrders();
			// also need to update exiting pendings ...!!!
			NLog.LogManager.GetCurrentClassLogger().Info("New amazon orders: {0}", newOrders.Count);
			newOrders.ForEach(x => CreateAmazonOrder(x));
			
			var unprocessedOrders = store.GetUnprocessedOrders();
			unprocessedOrders.ForEach(x => CreateAxSalesOrder(x, "Amazon"));
			
		}
		
		public void ProcessNewMagentoOrders()
		{
			using(var store = new MagentoStore())
			{
				var orders = store.GetNewOrders();
				NLog.LogManager.GetCurrentClassLogger().Info("New magento orders: {0}", orders.Count);
				//don't insert edited orders in AX
				orders.Where(o => !(o.OrderId.Contains("-"))).ToList().ForEach(x => CreateAxSalesOrder(x, "Magento"));
				//still log the edited orders though
				orders.Where(o => o.OrderId.Contains("-")).ToList().ForEach(x => CreateProcessedOrder(x, "Magento"));
			}
		}
		
		public string GetItemSalesUoM(string sku)
		{
			dynamic table = new InventTableModule();
			var rec = table.First(ItemId:sku, ModuleType:2);
			 
			if(rec != null)
				return rec.UNITID.ToString().Trim();
			
			return null;
		 
		}
		
		public decimal GetItemPrice(string sku)
		{
			dynamic table = new InventTableModule();
			var rec = table.First(ItemId:sku, ModuleType:2);
			 
			if(rec != null)
				return rec.PRICE;
			
			return 0;
		 
		}
		
		public long GetSalesOrderRecId(string salesId)
		{
			dynamic table = new SalesTable();
			var rec = table.First(salesId:salesId);
			
			if(rec != null)
				return rec.RECID;
			
			return 0;
		}
		
		public void CreateAxSalesOrder(SalesOrder order,string storeName)
		{
            try
            { 
            	using(var ax = Login())
            	{
            		ax.TTSBegin();
            	
	            	using (var rec = ax.CreateAxaptaRecord("SalesTable"))
	                {
	                   
	                    // Provide values for each of the AddressState record fields.
	                    AxSalesOrder.BuildDefaults(rec,storeName);
	                    
						rec.set_Field(AxSalesOrder.SalesId, order.OrderId);
						rec.set_Field(AxSalesOrder.SalesName, order.BillToCustomerName);
						if(!string.IsNullOrWhiteSpace(order.CustomerEmail))
							rec.set_Field(AxSalesOrder.Email, order.CustomerEmail);
						
						rec.set_Field(AxSalesOrder.DeliveryAddress , order.DeliveryStreet + System.Environment.NewLine + 
						             order.DeliveryCity + ", " + order.DeliveryState + " " + order.DeliveryZip );
						rec.set_Field(AxSalesOrder.DeliveryName,order.DeliveryCustomerName);
						rec.set_Field(AxSalesOrder.DeliveryStreet, order.DeliveryStreet);
						rec.set_Field(AxSalesOrder.DeliveryCity, order.DeliveryCity);
						rec.set_Field(AxSalesOrder.DeliveryState, order.DeliveryState);
						rec.set_Field(AxSalesOrder.DeliveryZipCode, order.DeliveryZip);
						rec.set_Field(AxSalesOrder.DeliveryCountryRegionId, order.DeliveryCountry);
						//newly added
						rec.set_Field(AxSalesOrder.OrderDate, order.StoreCreatedAt);
						if(!string.IsNullOrWhiteSpace(order.GiftMessageFrom))
							rec.set_Field(AxSalesOrder.GiftMessageFrom, order.GiftMessageFrom);
						if(!string.IsNullOrWhiteSpace(order.GiftMessageTo))
							rec.set_Field(AxSalesOrder.GiftMessageTo, order.GiftMessageTo);
						if(!string.IsNullOrWhiteSpace(order.GiftMessageBody))
							rec.set_Field(AxSalesOrder.GiftMessageBody, order.GiftMessageBody);
						if(!string.IsNullOrWhiteSpace(order.CustomerOrderComment))
							rec.set_Field(AxSalesOrder.CustomerOrderComment, order.CustomerOrderComment);
						if(!string.IsNullOrWhiteSpace(order.DeliveryMode))
							rec.set_Field(AxSalesOrder.DeliveryMode, order.DeliveryMode);
						if(!string.IsNullOrWhiteSpace(order.EndDiscount))
							rec.set_Field(AxSalesOrder.EndDiscount, order.EndDiscount);
	
	                    rec.Insert();
	                    
	                }   
	            	foreach (var line in order.LineItems) 
	            	{
	            		using(var rec = ax.CreateAxaptaRecord("SalesLine"))
	            		{
		            		AxSalesOrder.LineBuildDefaults(rec, storeName);
		            		// ToDo: need to verfiy what get from the store is the same in Ax or send alert
		            		rec.set_Field(AxSalesOrder.SalesId, order.OrderId);
		            		rec.set_Field(AxSalesOrder.LineNumber, line.LineNumber);
		            		rec.set_Field(AxSalesOrder.LineItemId, line.ItemSku);
		            		rec.set_Field(AxSalesOrder.LineItemName, line.ItemName);
		            		rec.set_Field(AxSalesOrder.LineQuantityOrdered, line.Quantity);
		            		rec.set_Field(AxSalesOrder.LineRemainSalesPhysical, line.Quantity);
		            		rec.set_Field(AxSalesOrder.LineSalesQuantity, line.Quantity);
		            		rec.set_Field(AxSalesOrder.LineRemainInventoryPhyscal, line.Quantity);
		            		rec.set_Field(AxSalesOrder.LineSealesPrice, line.Price);
		            		rec.set_Field(AxSalesOrder.LineAmount, line.StoreTotal);
		            		rec.set_Field(AxSalesOrder.LineSalesMarkup, line.SalesMarkup);
		            		if(!String.IsNullOrWhiteSpace(line.UnitOfMeasure))
		            			rec.set_Field(AxSalesOrder.LineSalesUnit, line.UnitOfMeasure);
		            		
		            		rec.set_Field(AxSalesOrder.LineDiscount,line.LineDiscount);
			            		
		            		rec.Insert();
	            		}
	            	}
	            	
	            	ax.TTSCommit();
	            	
	            	ax.TTSBegin();
	            	
	            	using(var rec = ax.CreateAxaptaRecord("MarkupTrans"))
	            	{
	            		AxSalesOrder.MarkupTransBuildDefaults(rec);
	            		var recid = GetSalesOrderRecId(order.OrderId);
	            		rec.set_Field(AxSalesOrder.TransRecId, recid);
	            		rec.set_Field(AxSalesOrder.MarkUpCode, order.ShippingChargeCode);
	            		var txt = order.ShippingChargeCode == "FREESHIP" ? "Free_Shipping_Now" : "Freight";
	            		rec.set_Field(AxSalesOrder.MarkupTransTxt, txt );
	            		rec.set_Field(AxSalesOrder.MarkupTransValue, order.ShippingChargeAmount);
	            		rec.Insert();
	            		
	            	}
	            	ax.TTSCommit();
	            	
	            	if(!string.IsNullOrWhiteSpace(order.BillToStreet))
	            	{
	            		ax.TTSBegin();
	            		using(var rec = ax.CreateAxaptaRecord("Address"))
		            	{
	            			var recid = GetSalesOrderRecId(order.OrderId);
	            			rec.set_Field(AxSalesOrder.AddrTableId,366);
	            			rec.set_Field(AxSalesOrder.AddrRecId,recid);
	            			rec.set_Field(AxSalesOrder.AddressType,1);
	            			
		            		rec.set_Field(AxSalesOrder.AddressFullAddress , order.BillToStreet + System.Environment.NewLine + 
							             order.BillToCity + ", " + order.BillToState + " " + order.BillToZip );
							 
							rec.set_Field(AxSalesOrder.AddressName,order.BillToCustomerName);
							rec.set_Field(AxSalesOrder.AddressStreet, order.BillToStreet);
							rec.set_Field(AxSalesOrder.AddressCity, order.BillToCity);
							rec.set_Field(AxSalesOrder.AddressState, order.BillToState);
							rec.set_Field(AxSalesOrder.AddressZipCode, order.BillToZip);
							rec.set_Field(AxSalesOrder.AddressCountryRegionId, order.BillToCountry);
							
							rec.Insert();
	            		}
	            		ax.TTSCommit();
	            	}
	            	
            	}
            	
            	CreateProcessedOrder(order, storeName);
            	
            }

            catch (Exception e)
            {                
                NLog.LogManager.GetCurrentClassLogger().Error(e);
            }
		}	
		
		
		public void CreateProcessedOrder(SalesOrder order, string store)
		{
			var processedOrder = new ProcessedOrders();
			var newId = processedOrder.Insert(new {
			                                  	Store = store, 
			                                  	StoreEntityId = order.NativeId, 
			                                  	OrderNumber = order.OrderId, 
			                                  	CreatedAt = DateTime.Now,
			                                  	StoreCreatedAt = order.StoreCreatedAt,
			                                  	StoreStatus = order.StoreStatus,
			                                  	ShipStreet = order.DeliveryStreet
			                                  });
		}
		
		public void CreateAmazonOrder(SalesOrder order)
		{
			dynamic amazonOrder = new AmazonOrders();
			var existingOrder = amazonOrder.First(OrderNumber: order.OrderId);
			dynamic map = new ExpandoObject();
			
			                                  	map.Store = "Amazon";
			                                  	map.StoreEntityId = order.NativeId;
			                                  	map.OrderNumber = order.OrderId; 			                           
			                                  	map.StoreCreatedAt = order.StoreCreatedAt;
			                                  	map.StoreUpdatedAt = order.StoreUpdatedAt;
			                                  	map.StoreStatus = order.StoreStatus;
			                                  	map.CustomerName = order.DeliveryCustomerName;
			                                  	map.ShipStreet = order.DeliveryStreet;
			                                  	map.ShipCity = order.DeliveryCity;
			                                  	map.ShipState = order.DeliveryState;
			                                  	map.ShipZip = order.DeliveryZip;
			                                  	map.ShipCountry = order.DeliveryCountry;
			                                   
			if(existingOrder == null)
			{
				NLog.LogManager.GetCurrentClassLogger().Warn("Create amazon order: {0}", order.OrderId);
				map.CreatedAt = DateTime.Now;
				var newId = amazonOrder.Insert(map);
			}
			else if(existingOrder.StoreStatus != order.StoreStatus)
			{
				NLog.LogManager.GetCurrentClassLogger().Warn("Update amazon order: {0}", order.OrderId);
				map.Updatedat = DateTime.Now;
				amazonOrder.Update(map,existingOrder.Id);
			}
		}
	}
}
