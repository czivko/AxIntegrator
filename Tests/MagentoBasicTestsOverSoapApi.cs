using System;
using System.Linq;
using MagentoApi;
using NUnit.Framework;
using CandyDirect.AppServices.DB;

namespace Tests
{
	[TestFixture]
	public class MagentoBasicTestsOverSoapApi
	{
		[Test]
		public void CanGetNewOrders()
		{
			var table = new ProcessedOrders();
 			var orders = table.All();
 			Console.WriteLine(orders.Count());
 			
			MagentoService mservice = new MagentoService();
          	String mlogin = mservice.login("dynamics_ax", "dynamics_ax");
          	
          	filters mf = new filters();
           	complexFilter[] cpf = new complexFilter[1];
           	complexFilter mcpf = new complexFilter();
           	mcpf.key = "entity_id"; //"increment_id";//
           	associativeEntity mas = new associativeEntity();
           	mas.key = "gt";
           	mas.value = orders.First().StoreEntityId; //"CDO00022569";
           	mcpf.value = mas;
           	cpf[0] = mcpf;
           	mf.complex_filter = cpf;
          	salesOrderEntity[] soe = mservice.salesOrderList(mlogin, mf);
          	if (soe.Length > 0)
	          {
	 
	              foreach (salesOrderEntity msoe in soe)
	              {
	                  	var orderInfo = mservice.salesOrderInfo(mlogin, msoe.increment_id);
	                      Console.WriteLine(orderInfo.order_id  + "::" + orderInfo.increment_id + " :: " 
	                  	                  + orderInfo.billing_firstname + " " + orderInfo.subtotal + "Number of items: " 
	                  	                  + orderInfo.items.Length);
	                  	orderInfo.items.ToList().ForEach(PrintOrderItem);
	              }
	          } 
		}
		
		private void PrintOrderItem(salesOrderItemEntity item)
		{
			Console.WriteLine("   ___  " + item.sku + " Name: " + item.name);
			
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
