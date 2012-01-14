using System;
using System.Linq;
using CandyDirect.AppServices;
using CandyDirect.AppServices.DB;
using MagentoApi;
using NUnit.Framework;
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
	}
}
