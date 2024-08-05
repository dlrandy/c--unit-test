using System;
using Sparky_lib;

namespace Sparky_Nunit_Test
{
	[TestFixture]
	public class CustomerNUnit
	{

		private Customer customer;
		[SetUp]
		public void SetUp() {

			customer = new Customer();
		}
		[Test]
		public void  CombineName_InputFirstAndLastName_ReturnFullName()
		{
			//var customer = new Customer();


            customer.GreetAndCombineNames("Ben", "Spark");

			Assert.Multiple(() => {
                Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
                Assert.That(customer.GreetMessage, Does.Contain("ben Spark").IgnoreCase);
				Console.WriteLine("still....");
                Assert.That(customer.GreetMessage, Does.StartWith("hello, ").IgnoreCase);
                //Assert.That(customer.GreetMessage, Does.EndWith("Spark1"));
                Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
                Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            });
   //         Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ben Spark"));
			//Assert.That(customer.GreetMessage, Does.Contain("1ben Spark").IgnoreCase);
			//Assert.That(customer.GreetMessage, Does.StartWith("hello, ").IgnoreCase);
			//Assert.That(customer.GreetMessage, Does.EndWith("Spark"));
			//Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));

		}

		[Test]
		public void GreetMessage_NotGreeted_ReturnNull() {
			//var customer = new Customer();
			//customer.GreetAndCombineNames("Ben","Spark");
			Assert.IsNull(customer.GreetMessage);
		}

		[Test]
		public void DiscountCheck_DefaultCustomer_ReturnDiscountInRange() {

			int result = customer.Discount;
			Assert.That(result, Is.InRange(10, 25));
		}

		[Test]
		public void GreetMessage_GreetedWithoutLstName_ReturnsNotNull() {
			customer.GreetAndCombineNames("ben","");
			Assert.IsNotNull(customer.GreetMessage);
			Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
		}

		[Test]
		public void GreetChecker_EmptyFirstName_ThrowsException() {

			var exceptionDetails = Assert.Throws<ArgumentException>(()=> customer.GreetAndCombineNames("","Spark"));
            Assert.That(exceptionDetails.Message, Is.EqualTo("Empty First Name"));
			Assert.That(()=>customer.GreetAndCombineNames("","Spark"),Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));


			Assert.Throws<ArgumentException>(()=> customer.GreetAndCombineNames("","Spark"));

			Assert.That(()=>customer.GreetAndCombineNames("","Spark"),Throws.ArgumentException);


		}

		[Test]
		public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer() {

			customer.OrderTotal = 10;
			var result = customer.GetCustomerDetails();
			Assert.That(result,Is.TypeOf<BasicCustomer>());
		}

		[Test]
		public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer() {

			customer.OrderTotal = 110;
			var result = customer.GetCustomerDetails();
			Assert.That(result,Is.TypeOf<PlatinumCustomer>());
		}

	}
}

