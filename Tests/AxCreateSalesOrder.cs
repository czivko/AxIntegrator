using System;
using Microsoft.Dynamics.BusinessConnectorNet;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class AxCreateSalesOrder
	{
		[Test]
		public void CanCreateANewSalesOrder()
		{
			 Axapta ax;
            AxaptaRecord axRecord;
            string tableName = "SalesTable";
            GetAllRecordsForAxTable(tableName);
            /*
            // The AddressState field names for calls to
            // the AxRecord.get_field method.
            string strNameField = "NAME";
            string strStateIdField = "STATEID";

            // The output variables for calls to the 
            // AxRecord.get_Field method.
            object fieldName, fieldStateId;

            try
            {
                // Login to Microsoft Dynamics AX.
                ax = new Axapta();
                ax.Logon(null, null, null, null);

                AxaptaRecord axRecord1;
                // Create a new AddressState table record.
                using (axRecord1 = ax.CreateAxaptaRecord(tableName)) 
                {
                     
                    // Provide values for each of the AddressState record fields.
                    axRecord1.set_Field("NAME", "MyState2");
                    axRecord1.set_Field("STATEID", "MyState2");
                    axRecord1.set_Field("COUNTRYREGIONID", "US");
                    axRecord1.set_Field("INTRASTATCODE", "");

                    // Commit the record to the database.
                    //axRecord1.Insert();
                }
                 

                
            }

            catch (Exception e)
            {
                Console.WriteLine("Error encountered: {0}", e.Message);
                // Take other error action as needed.
            }
            */
		}
	
	    
		public Axapta Login()
		{		
			var ax = new Axapta();
	        ax.Logon(null, null, null, null);
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
	
	public static class AxSalesOrder
	{
		public static string SalesId = "SalesId";
		public static string SalesName = "SalesName";
	}
}
