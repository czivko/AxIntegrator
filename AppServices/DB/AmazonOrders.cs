using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class AmazonOrders:DynamicModel 
	{
	    	public AmazonOrders():base("CandyDirectAx", "CandyDirectAmazonOrders","Id") {}
	
	}
}
