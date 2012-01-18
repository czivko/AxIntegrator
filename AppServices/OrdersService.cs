using System;
using System.Collections.Generic;
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
				throw new ArgumentNullException("AxUserName or AxUserPass is missing from <appsettings. in the config file");
			
			System.Net.NetworkCredential creds = new System.Net.NetworkCredential(
				adUser,adPass, "candydirect.com");
				ax.LogonAs("czivko","candydirect.com",creds,null,null,null,null);
	        //ax.Logon(null, null, null, null);
	        return ax;
		}
		
		public void ProcessNewOrders()
		{
			ProcessNewMagentoOrders();
		}
		
		public void ProcessNewMagentoOrders()
		{
			using(var store = new MagentoStore())
			{
				var orders = store.GetNewOrders();
				NLog.LogManager.GetCurrentClassLogger().Info("New magento orders: {0}", orders.Count);
				orders.ForEach(x => CreateAxSalesOrder(x));
			}
		}
		
		public void CreateAxSalesOrder(SalesOrder order)
		{
            try
            { 
            	using (var rec = Login().CreateAxaptaRecord("SalesTable"))
                {
                   
                    // Provide values for each of the AddressState record fields.
                    AxSalesOrder.BuildDefaults(rec);
                    
					rec.set_Field(AxSalesOrder.SalesId, order.OrderId);
					rec.set_Field(AxSalesOrder.DeliveryAddress , order.Street + System.Environment.NewLine + 
					             order.City + ", " + order.State + " " + order.Zip );
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
	            		AxSalesOrder.LineBuildDefaults(rec);
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
            	
            	CreateProcessedOrder(order, "Magento");
            	
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
			                                  	CreatedAt = DateTime.Now
			                                  });
		}
	}
}
