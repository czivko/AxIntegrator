/*
 * Created by SharpDevelop.
 * User: czivko
 * Date: 12/14/2011
 * Time: 1:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using MagentoApi;

namespace Tests
{
	[TestFixture]
	public class MagentoBasicTestsOverSoapApi
	{
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
           
         /* 
         filters mf = new filters();
           complexFilter[] cpf = new complexFilter[1];
           complexFilter mcpf = new complexFilter();
           mcpf.key = "increment_id";
           associativeEntity mas = new associativeEntity();
           mas.key = "gt";
           mas.value = "1008001";
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
                      Console.WriteLine("" + msoe.billing_firstname + " " + msoe.subtotal);
                  }
                  catch (Exception merror)
                  {
                      Console.WriteLine("" + msoe.order_id + ""+merror.ToString());
                  }
              }
          }
          */
		}
	}
}
