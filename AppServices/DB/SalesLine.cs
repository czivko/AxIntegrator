using System;
using Massive;

namespace CandyDirect.AppServices.DB
{
	public class SalesLine:DynamicModel
	{
		public SalesLine():base("CandyDirectAx", "SalesLine","RecId"){}
	}
}
