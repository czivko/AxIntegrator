using System;
using System.Collections.Generic;

namespace CandyDirect.AppServices
{
	/// <summary>
	/// Description of AmazonStore.
	/// </summary>
	public class AmazonStore
	{
		public AmazonStore()
		{
		}
		
		public List<string> GetNewOrders()
		{
			return new List<string>();
			
		}
		
		public bool UpdateOrderAsShipped(string orderId)
		{
			return true;
		}
		
		public bool UpdateProductInventory(string sku, int available)
		{
			return true;
		}
		
		public bool CreateNewProduct()
		{
			return true;
		}
		
		
	}
}
