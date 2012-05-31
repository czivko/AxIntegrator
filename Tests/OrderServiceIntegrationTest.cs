using System;
using System.Linq;
using CandyDirect.AppServices;
using CandyDirect.AppServices.DB;
using MagentoApi;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
	[TestFixture]
	public class OrderServiceIntegrationTest
	{
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
	}
}
