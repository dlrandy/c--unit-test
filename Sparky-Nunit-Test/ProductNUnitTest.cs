﻿using System;
using Moq;
using Sparky_lib;

namespace Sparky_Nunit_Test
{
	[TestFixture]
	public class ProductNUnitTest
	{
		[Test]
		public void GetProductPrice_PlatinumCustomer_ReturnPriceWith20Discount()
		{
			Product product = new Product() {
				Price = 50,
			};
			// 只为了使用mock框架，实现一个icustomer接口，不是较好的方式；如果应用需要接口和实现逻辑
			var result = product.GetPrice(new Customer() {IsPlatinum = true });
			Assert.That(result, Is.EqualTo(40));
		}
		[Test]
		public void GetProductPriceMockAbuse_PlatinumCustomer_ReturnPriceWith20Discount()
		{
			Product product = new Product()
			{
				Price = 50,
			};
			var customer = new Mock<ICustomer>();
			customer.Setup(u => u.IsPlatinum).Returns(true);
			// 只为了使用mock框架，实现一个icustomer接口，不是较好的方式；如果应用需要接口和实现逻辑
			var result = product.GetPrice(customer.Object);
			Assert.That(result, Is.EqualTo(40));
		}
	}
}

