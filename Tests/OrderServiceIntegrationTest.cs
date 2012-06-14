using System;
using System.Linq;
using CandyDirect.AppServices;
using CandyDirect.AppServices.DB;
using MagentoApi;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace Tests
{
	[TestFixture]
	public class OrderServiceIntegrationTest
	{
		
		[Test]
		public void ListAnyStartsWith()
		{
			var orders = new List<SalesOrder>(){
				new SalesOrder{OrderId = "1"},
				new SalesOrder{OrderId = "2"},
				new SalesOrder{OrderId = "2-1"},
				new SalesOrder{OrderId = "3-1"},
			};
			orders.Any(x => x.OrderId == "1").ShouldBe(true);
			orders.Any(x => x.OrderId == "4").ShouldBe(false);
			orders.Any(x => x.OrderId.StartsWith( "4")).ShouldBe(false);
			orders.Any(x => x.OrderId.StartsWith( "1")).ShouldBe(true);
			orders.Any(x => x.OrderId.StartsWith( "3")).ShouldBe(true);
		}
		[Test]
		public void InsertNewMagentoOrdersIntoAx()
		{
			var service = new OrderService();
			service.ProcessNewMagentoOrders();
		}
		
		[Test] 
		public void ProcessUpdatedMagentoOrdersTest()
		{
			var service = new OrderService();
			var store = new MagentoStore();
			service.ProcessUpdatedMagentoOrders(store.GetUpdatedOrders());
		}
		
		[Test]
		public void ProcessAmazonOrders()
		{
			//things to fix, address white space for addtional lines, net amount not set, UoM
			var orderService = new OrderService();
			orderService.ProcessAmazonOrders();
		}
		
		[Test]
		public void GetItemSalesUoM()
		{
			var service = new OrderService();
			var uom = service.GetItemSalesUoM("618632-aa");
			uom.ShouldBe("12 COUNT");
		}
		
		[Test]
		public void CanGetSalesLineRecordsWithMasive()
		{
			
			dynamic table = new CandyDirect.AppServices.DB.SalesLine();
			var recs = table.FindBy(SalesId:"100058005");
		}
	}
}
