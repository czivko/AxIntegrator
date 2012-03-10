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
			if(string.IsNullOrWhiteSpace(order.CustomerName) && magentoOrder.billing_address != null)
			   order.CustomerName = magentoOrder.billing_address.firstname + " " + magentoOrder.billing_address.lastname;
			order.Street = magentoOrder.shipping_address.street;
			order.City = magentoOrder.shipping_address.city;
			order.State = magentoOrder.shipping_address.region;
			order.Zip = magentoOrder.shipping_address.postcode; 
			order.Country = magentoOrder.shipping_address.country_id;
			order.StoreCreatedAt = DateTime.Parse(magentoOrder.created_at);
			
			var customFields = GetMagentoOrderCustomFields(int.Parse(magentoOrder.order_id));
			order.GiftMessageFrom = customFields.gift_message_sender;
			order.GiftMessageTo = customFields.gift_message_recipient;
			order.GiftMessageBody = customFields.gift_message;
			order.CustomerOrderComment = customFields.customer_comment;
			order.ShippingChargeCode = FreeShip(customFields.coupon_code) ? "FREESHIP" :"FREIGHT"; // or FREESHIP
			order.ShippingChargeAmount = FreeShip(customFields.coupon_code) ? 0.0m : Decimal.Parse(magentoOrder.shipping_amount);
			
			order.DeliveryMode = MapDeliveryMethod(magentoOrder.shipping_method);
			OrderService orderService = new OrderService();
			foreach(var line in magentoOrder.items)
			{
				order.AddLineItem(line.sku, line.name, Decimal.Parse(line.qty_ordered),
				                  Decimal.Parse(line.price),Decimal.Parse(line.row_total), 
				                  orderService.GetItemSalesUoM(line.sku),
				                  orderService.GetItemPrice(line.sku));
			}
			
			return order;
		}
		
		public bool FreeShip(string couponCode)
		{
			if(!string.IsNullOrWhiteSpace(couponCode))
				return couponCode.ToLower() == "free_shipping_now";
			
			return false;
		}
		
		public string MapDeliveryMethod(string method)
		{
			switch (method) {
				case "ups_01":
					return AxShippingMethods.UpsNextDay;
				case "ups_02":
					return AxShippingMethods.s2Day;
				case "ups_03":
					return AxShippingMethods.s5_8Day;
				case "ups_12":
					return AxShippingMethods.s3_4Day;
				case "ups_13":
					return AxShippingMethods.UpsNxtDaySaver;
				case "ups_14":
					return AxShippingMethods.UpsNextDayAm;
				case "usps_Priority Mail Large Flat-Rate Box":
					return AxShippingMethods.UpsNextDayAm;
				default:
					return AxShippingMethods.Standard;
					 
			}
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
		
		public customerCommentDetail GetMagentoOrderCustomFields(int orderEntityId)
		{
			return _mservice.salesOrderCustomerComment(_mlogin, orderEntityId);
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
