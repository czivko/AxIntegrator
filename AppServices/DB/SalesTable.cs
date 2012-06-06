using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class SalesTable:DynamicModel
	{
		public SalesTable():base("CandyDirectAx", "SalesTable","RecId"){}
	}
}

 