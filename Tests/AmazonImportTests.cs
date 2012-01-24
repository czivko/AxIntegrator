
using System;
using CandyDirect.AppServices;
using Microsoft.Dynamics.BusinessConnectorNet;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class AmazonImportTests
	{
		[Test]
		public void CanCreateInventoryMovementJournal()
		{
			TempCreateInventJournalTrans();
		}
		
		
		public void TempCreateInventJournalTrans()
		{
            try
            { 
            	using(var ax = Login())
            	{
            		ax.TTSBegin();
            		
	            	using (var rec = ax.CreateAxaptaRecord("InventJournalTrans"))
	                {   
						rec.set_Field("JournalId", "000012_059");
						rec.set_Field("LineNum" , 1m);
						rec.set_Field("TransDate", DateTime.Now);
						rec.set_Field("JournalType",0);
						rec.set_Field("ItemId","1");
						rec.set_Field("Qty", 2m);						
						rec.set_Field("PriceUnit", 1m);
						rec.set_Field("LedgerAccountIdOffset", "9998-00");
						rec.set_Field("InventDimId", "000001");
						
						/* setting these two in sql script
						 * rec.set_Field("CostPrice", 1m);
						 * rec.set_Field("CostAmount", 1m);
						 */
	
	                    rec.Insert();
	            	}           	
	            	
	            	ax.TTSCommit();
            	}
            }

            catch (Exception e)
            {                
                NLog.LogManager.GetCurrentClassLogger().Error(e);
            }
		}
		
		private Axapta Login()
		{		
			var ax = new Axapta();
			var adUser = "czivko";
			var adPass = "injectMyCandy99";
			if(adUser == null || adPass == null)
				throw new ArgumentNullException("AxUserName or AxUserPass is missing from <appsettings> in the config file");
		
			System.Net.NetworkCredential creds = new System.Net.NetworkCredential(
				adUser,adPass, "candydirect.com");
				ax.LogonAs("czivko","candydirect.com",creds,null,null,null,null);
	        //ax.Logon(null, null, null, null);
	        return ax;
		}
	}
	
	
	
}
