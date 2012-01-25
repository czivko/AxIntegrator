using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class CandyDirectInventoryCount2012:DynamicModel 
	{
	        //you don't have to specify the connection - Massive will use the first one it finds in your config
	    	public CandyDirectInventoryCount2012():base("CandyDirectAx", "CandyDirectInventoryCount2012","sku") {}
	
	}
}
