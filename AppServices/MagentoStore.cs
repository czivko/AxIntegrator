using System;
using System.Collections.Generic;
using System.Linq;

using CandyDirect.AppServices.DB;
using MagentoApi;

namespace CandyDirect.AppServices
{
	/// <summary>
	/// Description of AmazonStore.
	/// </summary>
	public class MagentoStore : IDisposable
	{
		MagentoService _mservice; 
		string _mlogin;		
		
		public MagentoStore()
		{
			_mservice = new MagentoService();
			_mlogin = _mservice.login("dynamics_ax", "dynamics_ax");
			
		}
		
		public List<SalesOrder> GetNewOrders()
		{
			var orders = new List<SalesOrder>();
			foreach(var magentoOrder in GetNewMagentoOrders())
			{
				orders.Add(MapOrderFromStore(GetMagentoOrderDetails(magentoOrder.increment_id)));
			}
			
			return orders;
		}
		
		public SalesOrder MapOrderFromStore(salesOrderEntity magentoOrder)
		{
			var order = new SalesOrder();
			order.OrderId = magentoOrder.increment_id;
			order.NativeId = magentoOrder.order_id;
			order.CustomerName = magentoOrder.customer_firstname + " " + magentoOrder.customer_lastname;
			order.Street = magentoOrder.shipping_address.street;
			order.City = magentoOrder.shipping_address.city;
			order.State = magentoOrder.shipping_address.region;
			order.Zip = magentoOrder.shipping_address.postcode;
			order.Country = magentoOrder.shipping_address.country_id;
			
			foreach(var line in magentoOrder.items)
			{
				order.AddLineItem(line.sku, line.name, Decimal.Parse(line.qty_ordered),
				                  Decimal.Parse(line.price),Decimal.Parse(line.row_total), "");
			}
			
			return order;
		}
		public List<salesOrderEntity> GetNewMagentoOrders()
		{
			var table = new ProcessedOrders();
			var lastOrder = table.All(where: "where store = @0", args: "Magento", orderBy: "id DESC", limit: 1).First();
 			 	
          	filters mf = new filters();
           	complexFilter[] cpf = new complexFilter[1];
           	complexFilter mcpf = new complexFilter();
           	mcpf.key = "entity_id"; //"increment_id";//
           	associativeEntity mas = new associativeEntity();
           	mas.key = "gt";
           	mas.value = lastOrder.StoreEntityId; //"CDO00022569";
           	mcpf.value = mas;
           	cpf[0] = mcpf;
           	mf.complex_filter = cpf;
          	salesOrderEntity[] soe = _mservice.salesOrderList(_mlogin, mf);
          	return soe.ToList();
         
		}
		
		public salesOrderEntity GetMagentoOrderDetails(string id)
		{ 
			return _mservice.salesOrderInfo(_mlogin, id);

		}
		public  bool UpdateOrderAsShipped(string orderId)
		{
			return true;
		}
		
		public  bool UpdateProductInventory(string sku, int available)
		{
			return true;
		}
		
		public bool CreateNewProduct()
		{
			return true;
		}
		
		
		
		public void Dispose()
		{
			_mservice.endSession(_mlogin);
			_mservice = null;
		}
	}
	

}
