using System;

namespace Dominio
{
	public class SaleListing
	{
		public int orderid { get; set; }
		public DateTime orderDate { get; set; }

		public string customer { get; set; }
		public string products { get; set; }
		public double totalAmount { get; set; }
	}
}