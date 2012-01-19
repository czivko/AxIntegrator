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
		public void GetItemSalesUoM()
		{
			var service = new OrderService();
			var uom = service.GetItemSalesUoM("618632-aa");
			uom.ShouldBe("12 COUNT");
		}
	}
}
