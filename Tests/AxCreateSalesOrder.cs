using System;
using Microsoft.Dynamics.BusinessConnectorNet;
using NUnit.Framework;
using CandyDirect.AppServices;

namespace Tests
{
	[TestFixture]
	public class AxCreateSalesOrder
	{
		[Test]
		public void CanCreateANewSalesOrder()
		{
            string tableName = "SalesTable";
             var id = "Magento6";
            try
            { 
            	using (var rec = Login().CreateAxaptaRecord(tableName))
                {
                   
                    // Provide values for each of the AddressState record fields.
                    AxSalesOrder.BuildDefaults(rec,"Magento");
                    
					rec.set_Field(AxSalesOrder.SalesId, id);
					rec.set_Field(AxSalesOrder.DeliveryAddress , @"3035 Berkeley Cir" + System.Environment.NewLine + "Los Angeles, CA 90026");
					rec.set_Field(AxSalesOrder.DeliveryName,"Charles Zivko");
					rec.set_Field(AxSalesOrder.DeliveryStreet, "3035 Berkely Cir");
					rec.set_Field(AxSalesOrder.DeliveryCity, "Los Angeles");
					rec.set_Field(AxSalesOrder.DeliveryState, "CA");
					rec.set_Field(AxSalesOrder.DeliveryZipCode, "90026");
					rec.set_Field(AxSalesOrder.DeliveryCountryRegionId, "US");

					
                    
                    // Commit the record to the database.
                    rec.Insert();
                }   
            	using(var rec = Login().CreateAxaptaRecord("SalesLine"))
            	{
            		AxSalesOrder.LineBuildDefaults(rec, "Magento");
            		
            		rec.set_Field(AxSalesOrder.SalesId, id);
            		rec.set_Field(AxSalesOrder.LineNumber, 1m);
            		rec.set_Field(AxSalesOrder.LineItemId, "1113316-AP");
            		rec.set_Field(AxSalesOrder.LineItemName, "Hershey's Kisses  Original  Green & Silver Foil, 5 pounds");
            		rec.set_Field(AxSalesOrder.LineQuantityOrdered, 1m);
            		rec.set_Field(AxSalesOrder.LineRemainSalesPhysical, 1m);
            		rec.set_Field(AxSalesOrder.LineSalesQuantity, 1.00m);
            		rec.set_Field(AxSalesOrder.LineRemainInventoryPhyscal, 1.00m);
            		rec.set_Field(AxSalesOrder.LineSealesPrice, 36.00m);
            		rec.set_Field(AxSalesOrder.LineAmount, 36.00m);
            		rec.set_Field(AxSalesOrder.LineSalesUnit, "5.0 BAG");
            		
            		
            		
            		
            		rec.Insert();
            	}
            }

            catch (Exception e)
            {
                Console.WriteLine("Error encountered: {0}", e.Message);
                // Take other error action as needed.
            }
           
		}
		
		[Test]
		public void CanCancelSalesOrder()
		{
			var ax = Login();
			using(var axRecord = ax.CreateAxaptaRecord("SalesTable"))
            {

				// Execute a query to retrieve an editable record where the name is MyState.
				axRecord.ExecuteStmt("select forupdate * from %1 where %1.SalesId == '100058480'");
				
				// If the record is found then update the name.
				if (axRecord.Found)
				{
				    // Start a transaction that can be committed.
				    ax.TTSBegin();
				    Console.WriteLine(axRecord.get_Field("SalesStatus"));
				    axRecord.set_Field("SalesStatus", 4);
				    axRecord.Update();
				
				    // Commit the transaction.
				    ax.TTSCommit();
				}
			}
		}
	
	    [Test]
		public void CanGetSalesOrderFromAx()
		{ 
            string tableName = "SalesTable";
            GetAllRecordsForAxTable(tableName);
		}
		
		public Axapta Login()
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
		public void GetAllRecordsForAxTable(string tableName)
		{
			using (var axRecord = Login().CreateAxaptaRecord(tableName))
	        {
	
	            // Execute the query on the table.
	            axRecord.ExecuteStmt("select * from %1");
	
	            // Create output with a title and column headings
	            // for the returned records.
	            Console.WriteLine("List of selected records from {0}",
	                tableName);
	            Console.WriteLine("{0}\t{1}", AxSalesOrder.SalesId, AxSalesOrder.SalesName);
	
	            // Loop through the set of retrieved records.
	            while (axRecord.Found)
	            {
	            	 
	                // Retrieve the record data for the specified fields.
	                var fieldName = axRecord.get_Field(AxSalesOrder.SalesId);
	                var fieldStateId = axRecord.get_Field(AxSalesOrder.SalesName);
	
	                // Display the retrieved data.
	                Console.WriteLine(fieldName + "\t" + fieldStateId);
	
	                // Advance to the next row.
	                axRecord.Next();
	            }
	        }
		}
	}
	

}
