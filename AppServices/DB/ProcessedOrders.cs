using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class ProcessedOrders:DynamicModel 
	{
        //you don't have to specify the connection - Massive will use the first one it finds in your config
    	public ProcessedOrders():base("CandyDirectAx", "CandyDirectProcessedOrders","Id") {}

	}
}
