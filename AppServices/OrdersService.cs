using System;
using System.Collections.Generic;
using System.Dynamic;

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
			if(adUser == null || adPass == null)
				throw new ArgumentNullException("AxUserName or AxUserPass is missing from <appsettings> in the config file");
		
			System.Net.NetworkCredential creds = new System.Net.NetworkCredential(
				adUser,adPass, "candydirect.com");
				ax.LogonAs("czivko","candydirect.com",creds,null,null,null,null);
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
				orders.ForEach(x => CreateAxSalesOrder(x, "Magento"));
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
		public void CreateAxSalesOrder(SalesOrder order,string storeName)
		{
            try
            { 
            	using (var rec = Login().CreateAxaptaRecord("SalesTable"))
                {
                   
                    // Provide values for each of the AddressState record fields.
                    AxSalesOrder.BuildDefaults(rec,storeName);
                    
					rec.set_Field(AxSalesOrder.SalesId, order.OrderId);
					rec.set_Field(AxSalesOrder.DeliveryAddress , order.Street + System.Environment.NewLine + 
					             order.City + ", " + order.State + " " + order.Zip );
					rec.set_Field(AxSalesOrder.SalesName, order.CustomerName);
					rec.set_Field(AxSalesOrder.DeliveryName,order.CustomerName);
					rec.set_Field(AxSalesOrder.DeliveryStreet, order.Street);
					rec.set_Field(AxSalesOrder.DeliveryCity, order.City);
					rec.set_Field(AxSalesOrder.DeliveryState, order.State);
					rec.set_Field(AxSalesOrder.DeliveryZipCode, order.Zip);
					rec.set_Field(AxSalesOrder.DeliveryCountryRegionId, order.Country);

					
                    
                    // Commit the record to the database.
                    rec.Insert();
                }   
            	foreach (var line in order.LineItems) 
            	{
            		using(var rec = Login().CreateAxaptaRecord("SalesLine"))
            		{
	            		AxSalesOrder.LineBuildDefaults(rec, storeName);
	            		// ToDo: need to verfiy what get from the store is the same in Ax or send alert
	            		rec.set_Field(AxSalesOrder.SalesId, order.OrderId);
	            		rec.set_Field(AxSalesOrder.LineNumber, line.LineNumber);
	            		rec.set_Field(AxSalesOrder.LineItemId, line.ItemSku);
	            		rec.set_Field(AxSalesOrder.LineItemName, line.ItemSku);
	            		rec.set_Field(AxSalesOrder.LineQuantityOrdered, line.Quantity);
	            		rec.set_Field(AxSalesOrder.LineRemainSalesPhysical, line.Quantity);
	            		rec.set_Field(AxSalesOrder.LineSalesQuantity, line.Quantity);
	            		rec.set_Field(AxSalesOrder.LineRemainInventoryPhyscal, line.Quantity);
	            		rec.set_Field(AxSalesOrder.LineSealesPrice, line.Price);
	            		rec.set_Field(AxSalesOrder.LineAmount, line.StoreTotal);
	            		// need to look up in ax rec.set_Field(AxSalesOrder.LineSalesUnit, "5.0 BAG");
		            		
	            		rec.Insert();
            		}
            	}
            	
            	CreateProcessedOrder(order, storeName);
            	
            }

            catch (Exception e)
            {
                Console.WriteLine("Error encountered: {0}", e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e);
                // Take other error action as needed.
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
			                                  	ShipStreet = order.Street
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
			                                  	map.StoreStatus = order.StoreStatus;
			                                  	map.CustomerName = order.CustomerName;
			                                  	map.ShipStreet = order.Street;
			                                  	map.ShipCity = order.City;
			                                  	map.ShipState = order.State;
			                                  	map.ShipZip = order.Zip;
			                                  	map.ShipCountry = order.Country;
			                                   
			if(existingOrder == null)
			{
				map.CreatedAt = DateTime.Now;
				var newId = amazonOrder.Insert(map);
			}
			else if(existingOrder.StoreStatus != order.StoreStatus)
			{
				map.Updatedat = DateTime.Now;
				amazonOrder.Update(map,existingOrder.Id);
			}
		}
	}
}
