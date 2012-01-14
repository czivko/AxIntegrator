using System;
using System.Linq;
using CandyDirect.AppServices;
using CandyDirect.AppServices.DB;
using MagentoApi;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class MagentoBasicTestsOverSoapApi
	{
		[Test]
		public void CanGetNewMagentoOrders()
		{ 
			using(var store = new MagentoStore())
			{
				var soe = store.GetNewMagentoOrders();
          	 
		          foreach (salesOrderEntity msoe in soe)
		          {
		              	var orderInfo = store.GetMagentoOrderDetails(msoe.increment_id);
		                  Console.WriteLine(orderInfo.order_id  + "::" + orderInfo.increment_id + " :: " 
		              	                  + orderInfo.billing_firstname + " " + orderInfo.subtotal + "Number of items: " 
		              	                  + orderInfo.items.Length);
		              	orderInfo.items.ToList().ForEach(PrintOrderItem);
		          }
			}
	          
		}
		
		[Test]
		public void CanGetSalesOrdersMappedFromMagento()
		{
			using(var store = new MagentoStore())
			{
				var orders = store.GetNewOrders();
				foreach (var order in orders) 
				{
					Console.WriteLine(order.OrderId + " " + order.NativeId  + " " +
					                  order.CustomerName + " " +
					                  order.Street + " " +
					                  order.City + " " +
					                  order.State + " " +
					                  order.Zip + " " +
					                  order.Country
					                 );
					foreach (var line in order.LineItems) {
						Console.WriteLine("    -  " + line.ItemSku  + " " +
						                 line.ItemName  + " " +
						                 line.LineNumber  + " " +
						                 line.Price  + " " +
						                 line.Quantity  + " " +
						                 line.StoreTotal  + " " +
						                 line.CalculatedTotal  + " " +
						                 line.UnitOfMeasure
						                 );
					}
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
