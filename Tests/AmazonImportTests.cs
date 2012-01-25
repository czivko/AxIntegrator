
using System;
using System.Linq;
using CandyDirect.AppServices;
using CandyDirect.AppServices.DB;
using Microsoft.Dynamics.BusinessConnectorNet;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
	[TestFixture]
	public class AmazonImportTests
	{
		[Test]
		public void CreateInventoryMovementJournal()
		{
			TempCreateInventJournalTrans();
		}
		
		[Test]
		public void CreateMissingInventory()
		{
			var db = Massive.DynamicModel.Open("CandyDirectAx");
			//this stuff has inventory on the shelf but was not imported ... should be one time shot
			var hasInventoryButNotIntiallyImport = @"select jt.ITEMID as Id, gp.itemDesc as Name, gp.itmclscd as GroupId 
									from InventJournalTrans jt
									left join INVENTTABLE i on jt.ITEMID = i.ITEMID
									left join cdgp01.cdi.dbo.iv00101 gp on jt.itemid = gp.itemnmbr
									where i.ITEMID is null ";
			
			var newInventoryAddedInGPSinceLastImport = @"select gpInvent.itemnmbr as Id, gpInvent.itemDesc as Name, gpInvent.itmclscd as GroupId, gpInvent.creatddt CreatedAt
														from INVENTTABLE ax
														right join cdgp01.cdi.dbo.iv00101 gpInvent on ax.ITEMID = gpInvent.itemnmbr 
														where  
															ax.ITEMID is null and
															  gpInvent.creatddt > '8/1/2011'
															  and gpInvent.itemnmbr <> '111' 
														order by gpInvent.creatddt
														 ";
			
			var items = db.Query(newInventoryAddedInGPSinceLastImport);
			//Assert.That(items.ToList().Count(), Is.EqualTo(127));
			CreateInventory(items.ToList());
		}
		
		 
		public void CreateInventory(List<dynamic> items)
		{
			foreach (var item in items) 
			{			
	            try
	            { 
	            	using(var ax = Login())
	            	{
	            		ax.TTSBegin();
	            		
		            	using (var rec = ax.CreateAxaptaRecord("InventTable"))
		                {   
		            		rec.set_Field("ItemGroupId",item.GroupId);
							rec.set_Field("ItemId" , item.Id);
							rec.set_Field("ItemName", item.Name);
							
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
		}
		
		public void TempCreateInventJournalTrans()
		{
			var table = new CandyDirectInventoryCount2012();
			var items = table.All(where: "where QtyOnShelf <> 0");
			Assert.That(items.ToList().Count(), Is.EqualTo(1593));
			decimal lineNum = 1m;
			foreach (var item in items) 
			{			
	            try
	            { 
	            	using(var ax = Login())
	            	{
	            		ax.TTSBegin();
	            		
		            	using (var rec = ax.CreateAxaptaRecord("InventJournalTrans"))
		                {   
							rec.set_Field("JournalId", "000012_059");
							rec.set_Field("LineNum" , lineNum);
							rec.set_Field("TransDate", DateTime.Now);
							rec.set_Field("JournalType",0);
							rec.set_Field("ItemId",item.sku);
							rec.set_Field("Qty", item.QtyOnShelf);
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
	            	lineNum += 1m;
	            }
	
	            catch (Exception e)
	            {                
	                NLog.LogManager.GetCurrentClassLogger().Error(e);
	            }
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
