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
					                  order.DeliveryCustomerName + " " +
					                  order.DeliveryStreet + " " +
					                  order.DeliveryCity + " " +
					                  order.DeliveryState + " " +
					                  order.DeliveryZip + " " +
					                  order.DeliveryCountry + " " +
					                  order.StoreCreatedAt + " " +
					                  order.DeliveryMode
					                  
					                 );
					foreach (var line in order.LineItems) {
						Console.WriteLine("    -  " + line.ItemSku  + " " +
						                 line.ItemName  + " " +
						                 line.LineNumber  + " " +
						                 line.Price  + " " +
						                 line.Quantity  + " " +
						                 line.StoreTotal  + " " + 
						                 line.UnitOfMeasure + " " +
						                 line.LineDiscount
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
		public void CanGetOrderCommentsFromApi()
		{
			MagentoService mservice = new MagentoService();
			mservice.Url = "https://www.candydirect.com/index.php/api/v2_soap/index/";
			String mlogin = mservice.login("dynamics_ax", "dynamics_ax");
			//var result = mservice.orderCommentsGetOrderComments(mlogin,"100048351");
			var result1 = mservice.salesOrderCustomerComment(mlogin,55206);
			 
			Console.WriteLine(result1.gift_message_sender);
			Console.WriteLine(result1.gift_message_recipient);
			Console.WriteLine(result1.gift_message);
			Console.WriteLine(result1.customer_comment);
			Console.WriteLine(result1.coupon_code);
			
		}
		
		[Test]
		public void TestMethod()
		{
		 	MagentoService mservice = new MagentoService();
		 	// need to get prod working 
		 	mservice.Url = "https://www.candydirect.com/index.php/api/v2_soap/index/";
          	String mlogin = mservice.login("dynamics_ax", "dynamics_ax");
       
         	Console.WriteLine(mlogin);
         	var atts = new catalogProductRequestAttributes();
			atts.attributes =  new string[]{"name", "price"};
         	//var info = mservice.catalogProductInfo(mlogin, "1113761-GP", null,atts);
         	var sales = mservice.salesOrderInfo(mlogin,"100055500");
         	
          	//Console.WriteLine("name: " + info.name);
          	//Console.WriteLine(info.price);
          	Console.WriteLine(sales.shipping_address.street);
          	Console.WriteLine(sales.billing_address.street);
          	Console.WriteLine(sales.created_at);
          	DateTime convertedDate = DateTime.SpecifyKind(
				    DateTime.Parse(sales.created_at),
				    DateTimeKind.Utc);
				
          	Console.WriteLine(convertedDate.Kind);
          	Console.WriteLine(convertedDate.ToLocalTime());
         
		}
		
		
 
	}
}
