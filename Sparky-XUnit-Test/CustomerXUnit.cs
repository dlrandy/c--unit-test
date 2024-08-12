using System;
using Sparky_lib;

namespace Sparky_XUnit_Test
{

	public class CustomerXUnit
	{

		private Customer customer;
		 
		public CustomerXUnit() {

			customer = new Customer();
		}
		[Fact]
		public void  CombineName_InputFirstAndLastName_ReturnFullName()
		{
 

            customer.GreetAndCombineNames("Ben", "Spark");

			Assert.Multiple(() => {
                Assert.Equal("Hello, Ben Spark",customer.GreetMessage);
                //Assert.Contains("ben Spark",customer.GreetMessage);
				Console.WriteLine("still....");
                Assert.StartsWith("Hello, ", customer.GreetMessage );
                //Assert.That(customer.GreetMessage, Does.EndWith("Spark1"));
                Assert.EndsWith("Spark",customer.GreetMessage );
                Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
            });
 
		}

		[Fact]
		public void GreetMessage_NotGreeted_ReturnNull() {
			//var customer = new Customer();
			//customer.GreetAndCombineNames("Ben","Spark");
			Assert.Null(customer.GreetMessage);
		}

		[Fact]
		public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange() {

			int result = customer.Discount;
			Assert.InRange(result,10, 25);
		}

		[Fact]
		public void GreetMessage_GreetedWithoutLstName_ReturnsNotNull() {
			customer.GreetAndCombineNames("ben","");
			Assert.NotNull(customer.GreetMessage);
			Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
		}

		[Fact]
		public void GreetChecker_EmptyFirstName_ThrowsException() {

			var exceptionDetails = Assert.Throws<ArgumentException>(()=> customer.GreetAndCombineNames("","Spark"));
			Assert.Equal("Empty First Name", exceptionDetails.Message);
			Assert.Throws<ArgumentException>(()=> customer.GreetAndCombineNames("","Spark"));
		}

		[Fact]
		public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer() {

			customer.OrderTotal = 10;
			var result = customer.GetCustomerDetails();
			Assert.IsType<BasicCustomer>(result);
		}

		[Fact]
		public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer() {

			customer.OrderTotal = 110;
			var result = customer.GetCustomerDetails();
			Assert.IsType<PlatinumCustomer>(result);
		}

	}
}

