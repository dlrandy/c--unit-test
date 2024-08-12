using System;
using Sparky_lib;

namespace Sparky_XUnit_Test
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public double GetPrice(Customer customer) {
			if (customer.IsPlatinum)
			{
				return Price * .8;
			}
			return Price;

		}
		public double GetPrice(ICustomer customer) {
			if (customer.IsPlatinum)
			{
				return Price * .8;
			}
			return Price;

		}
	}
}

