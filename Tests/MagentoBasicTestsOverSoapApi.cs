using System;
using NUnit.Framework;
using MagentoApi;

namespace Tests
{
	[TestFixture]
	public class MagentoBasicTestsOverSoapApi
	{
		[Test]
		public void CanGetNewOrders()
		{
			MagentoService mservice = new MagentoService();
          	String mlogin = mservice.login("dynamics_ax", "dynamics_ax");
          	//mservice.salesOrderList(mlogin,null);
          	var salesInfo = mservice.salesOrderInfo(mlogin, "CDO00022570");
          	filters mf = new filters();
           	complexFilter[] cpf = new complexFilter[1];
           	complexFilter mcpf = new complexFilter();
           	mcpf.key = "entity_id"; //"increment_id";//
           	associativeEntity mas = new associativeEntity();
           	mas.key = "gt";
           	mas.value = "22901"; //"CDO00022569";
           	mcpf.value = mas;
           	cpf[0] = mcpf;
           	mf.complex_filter = cpf;
          	salesOrderEntity[] soe = mservice.salesOrderList(mlogin, mf);
          	if (soe.Length > 0)
	          {
	 
	              foreach (salesOrderEntity msoe in soe)
	              {
	                  try
	                  {
	                      Console.WriteLine(msoe.order_id  + "::" + msoe.increment_id + " :: " + msoe.billing_firstname + " " + msoe.subtotal);
	                  }
	                  catch (Exception merror)
	                  {
	                      Console.WriteLine("" + msoe.order_id + ""+merror.ToString());
	                  }
	              }
	          }
           
		}
		
		[Test]
		public void TestMethod()
		{
		 	MagentoService mservice = new MagentoService();
          	String mlogin = mservice.login("dynamics_ax", "dynamics_ax");
       
         	Console.WriteLine(mlogin);
         	var atts = new catalogProductRequestAttributes();
			atts.attributes =  new string[]{"name", "price"};
         	var info = mservice.catalogProductInfo(mlogin, "1113761-GP", null,atts);
         
          	Console.WriteLine("name: " + info.name);
          	Console.WriteLine(info.price);
           
         
		}
	}
}
