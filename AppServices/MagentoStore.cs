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
			var mUser = System.Configuration.ConfigurationManager.AppSettings["MagentoUserName"];
			var mPass = System.Configuration.ConfigurationManager.AppSettings["MagentoUserPass"];
			var mUrl = System.Configuration.ConfigurationManager.AppSettings["MagentoWebServiceUrl"];
			if(mUser == null || mPass == null)
				throw new ArgumentNullException("MagentoUserName or MagentoUserPass is missing from <appsettings> in the config file");
			
			if(mUrl == null)
				throw new ArgumentNullException(@"MagentoWebServiceUrl is missing from <appsettings> in the config file. Sample: 'https://www.candydirect.com/index.php/api/v2_soap/index/'");
			
			
			_mservice = new MagentoService();
			_mservice.Url = mUrl;
			_mlogin = _mservice.login(mUser, mPass);
			
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
			order.CustomerEmail = magentoOrder.customer_email;
			order.BillToCustomerName = magentoOrder.customer_firstname + " " + magentoOrder.customer_lastname;
			if(string.IsNullOrWhiteSpace(order.BillToCustomerName) && magentoOrder.billing_address != null)
			   order.BillToCustomerName = magentoOrder.billing_address.firstname + " " + magentoOrder.billing_address.lastname;
			
			order.DeliveryCustomerName = magentoOrder.shipping_address.firstname + " " + magentoOrder.shipping_address.lastname;
			order.DeliveryStreet = magentoOrder.shipping_address.street;
			order.DeliveryCity = magentoOrder.shipping_address.city;
			order.DeliveryState = magentoOrder.shipping_address.region;
			order.DeliveryZip = magentoOrder.shipping_address.postcode; 
			order.DeliveryCountry = magentoOrder.shipping_address.country_id;
			SetBillingAddress(order, magentoOrder);
			DateTime utcSalesDate = DateTime.SpecifyKind(DateTime.Parse(magentoOrder.created_at), DateTimeKind.Utc);
			order.StoreCreatedAt =utcSalesDate.ToLocalTime();
			DateTime utcUpdateDate = DateTime.SpecifyKind(DateTime.Parse(magentoOrder.updated_at), DateTimeKind.Utc);
			order.StoreUpdatedAt = utcUpdateDate.ToLocalTime();
			order.StoreStatus = magentoOrder.status;
			
			var customFields = GetMagentoOrderCustomFields(int.Parse(magentoOrder.order_id));
			order.GiftMessageFrom = customFields.gift_message_sender;
			order.GiftMessageTo = customFields.gift_message_recipient;
			order.GiftMessageBody = customFields.gift_message;
			order.CustomerOrderComment = customFields.customer_comment;
			order.ShippingChargeCode = FreeShip(customFields.coupon_code,magentoOrder.shipping_method ) ? "FREESHIP" :"FREIGHT"; // or FREESHIP
			order.ShippingChargeAmount = FreeShip(customFields.coupon_code,magentoOrder.shipping_method) ? 0.0m : Decimal.Parse(magentoOrder.shipping_amount);
			
			order.EndDiscount = magentoOrder.customer_group_id.Trim() == "2" ? "RWC" : ""; // this is the whole sale group on magento
			order.PaymentMethod = MapPaymentMethod( magentoOrder);
			order.DeliveryMode = MapDeliveryMethod(magentoOrder);
			if(!string.IsNullOrWhiteSpace(magentoOrder.shipping_description))
				order.ShippingMethodDescription = ScrubShippingMethodDescription(magentoOrder.shipping_description);
			OrderService orderService = new OrderService();
			foreach(var line in magentoOrder.items)
			{
				order.AddLineItem(line.sku, line.name, Decimal.Parse(line.qty_ordered),
				                  Decimal.Parse(line.price),
				                  Decimal.Parse(line.row_total),
				                  orderService.GetItemSalesUoM(line.sku),
				                  orderService.GetItemPrice(line.sku),
				                  Decimal.Parse(line.discount_amount));
			}
			
			return order;
		}
		
		public string ScrubShippingMethodDescription(string method)
		{
			string scrubbed = method.Trim();
			if(scrubbed.StartsWith("-"))
				scrubbed = scrubbed.Remove(0,1).Trim();
			
			return scrubbed;
		}
		public void SetBillingAddress(SalesOrder order, salesOrderEntity magentoOrder)
		{
			var billing = magentoOrder.billing_address;
			var shipping = magentoOrder.shipping_address;
			
			if( !billing.firstname.Equals(shipping.firstname, StringComparison.OrdinalIgnoreCase) ||
			    !billing.lastname.Equals(shipping.lastname,StringComparison.OrdinalIgnoreCase) ||
				!billing.street.Equals(shipping.street,StringComparison.OrdinalIgnoreCase) ||
				!billing.city.Equals(shipping.city,StringComparison.OrdinalIgnoreCase) ||
				!billing.postcode.Equals(shipping.postcode,StringComparison.OrdinalIgnoreCase) ||
				!billing.region.Equals(shipping.region,StringComparison.OrdinalIgnoreCase)
			
			)
			{
				order.BillToCustomerName = billing.firstname + " " + billing.lastname;
				order.BillToStreet =  billing.street;
				order.BillToCity =  billing.city;
				order.BillToState =  billing.region;
				order.BillToZip =  billing.postcode; 
				order.BillToCountry =  billing.country_id;
			}
		}
		
		public bool FreeShip(string couponCode,string shippingMethod)
		{
			if(!string.IsNullOrWhiteSpace(couponCode))
				return couponCode.ToLower() == "free_shipping_now";
			if(!string.IsNullOrWhiteSpace(shippingMethod))
				return shippingMethod.ToLower() == "freeshipping_freeshipping";
			
			return false;
		}
		
		public string MapPaymentMethod(salesOrderEntity magentoOrder)
		{
			switch(magentoOrder.payment.method) {
				case "authorizenet":
					if(magentoOrder.payment.cc_type.Trim().ToUpper() == "AE")
						return "Amex";
					if(magentoOrder.payment.cc_type.Trim().ToUpper() == "MC" ||
					  magentoOrder.payment.cc_type.Trim().ToUpper() == "VI" ||
					  magentoOrder.payment.cc_type.Trim().ToUpper() == "DI")
							return "Visa/MC/DS";
					return magentoOrder.payment.cc_type;
				case "googlecheckout":
					return "Google Chk";
				case "paypal_standard": 
					return "Paypal";
				default:
					return magentoOrder.payment.method; 
			} 
		}
		
		public string MapDeliveryMethod(salesOrderEntity magentoOrder)
		{
			var method = magentoOrder.shipping_method;
			var orderId = magentoOrder.increment_id;
			 
			switch (method) {
				case "ups_01":
					return AxShippingMethods.UpsNextDay;
				case "ups_02":
					return AxShippingMethods.s2Day;
				case "ups_03":
					return AxShippingMethods.s5_8Day;
				//case "ups_11":
				//	return AxShippingMethods.Standard;
				case "ups_12":
					return AxShippingMethods.s3_4Day;
				case "ups_13":
					return AxShippingMethods.UpsNxtDaySaver;
				case "ups_14":
					return AxShippingMethods.UpsNextDayAm;
				case "usps_Priority Mail Large Flat-Rate Box":
					return AxShippingMethods.UspsFlateRate;
				case "freeshipping_freeshipping":
					return AxShippingMethods.FreeShipping;
				default:
					NLog.LogManager.GetCurrentClassLogger().Error("No Magento Order: {0} shipping method for {1} and description {2}", orderId,method, magentoOrder.shipping_description);
					return AxShippingMethods.TBD; 
					 
			}
		}
		public List<salesOrderEntity> GetNewMagentoOrders()
		{
			var table = new ProcessedOrders();
			var lastOrder = table.All(where: "where store = @0", args: "Magento", orderBy: "storeentityid DESC", limit: 1).First();
 			 	
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
		
		public List<salesOrderEntity> GetUpdateMagentoOrders()
		{
			var table = new ProcessedOrders();
			var lastUpdatedOrder = table.All(where: "where store = @0", args: "Magento", orderBy: "storeupdatedat DESC", limit: 1).First();
			var d = lastUpdatedOrder.StoreUpdatedAt.AddSeconds(-2).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
			
			filters mf = new filters();
           	complexFilter[] cpf = new complexFilter[1];
           	complexFilter mcpf = new complexFilter();
           	mcpf.key = "updated_at"; 
           	associativeEntity mas = new associativeEntity();
           	mas.key = "gt";
           	mas.value =  lastUpdatedOrder.StoreUpdatedAt.AddSeconds(-2).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
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
