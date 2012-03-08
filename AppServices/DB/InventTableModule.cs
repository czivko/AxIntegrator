using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class InventTableModule:DynamicModel 
	{
	        //you don't have to specify the connection - Massive will use the first one it finds in your config
	    	public InventTableModule():base("CandyDirectAx", "InventTableModule","RecId") {}
	
	}
	

}
