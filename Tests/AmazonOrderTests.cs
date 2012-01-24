using System;
using CandyDirect.AxIntegrator;
using MarketplaceWebServiceOrders;
using MarketplaceWebServiceOrders.Model;
using NUnit.Framework;
using System.Collections.Generic;
using Shouldly;
using System.Xml.Serialization;
using CandyDirect.AppServices;
using System.Linq;

namespace Tests
{
	[TestFixture]
	public class AmazonOrderTests
	{
		[Test]
		public void ShouldGetNewOrders()
		{
			var store = new AmazonStore();
			var results = store.GetNewOrders();
			 
			results.Count.ShouldBeGreaterThan(1);
			results.ForEach(x => Console.WriteLine(x.OrderId));
			var orderService = new OrderService();
			//results.ForEach(x => orderService.CreateAmazonOrder(x));
		}
		
		[Test] 
		public void ShouldGetUpdatedOrders()
		{
			var store = new AmazonStore();
			var results = store.GetUpdatedAmazonOrdersViaFecther();
			
			results.Count.ShouldBeGreaterThan(1);
		}
		
		[Test]
		public void GetUnprocessedOrders()
		{
			var store = new AmazonStore();
			var results = store.GetUnprocessedOrders();
			results.Count.ShouldBeGreaterThan(1);
			var twoLines = results.Where(x => x.OrderId == "103-1460378-9201802");
			twoLines.Count().ShouldBe(1);
			twoLines.First().LineItems.Count.ShouldBe(2);
		}
		
		
	}

}
