using System;
using Bongo.Models.ModelValidations;

namespace Bongo.Models
{
	[TestFixture]
	public class DateInFutureAttributeTests
	{
		[Test]
		public void DateValidator_InputExpectedDateRange_DateValidity()
		{
            DateInFutureAttribute dateInFutureAttribute = new DateInFutureAttribute();
			var result = dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(200));
			Assert.That(result, Is.EqualTo(true));
		}
        [TestCase(100, ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
		public bool DateValidator_InputExpectedDateRange_DateValidity_Multiple(int addTime)
		{
            DateInFutureAttribute dateInFutureAttribute = new DateInFutureAttribute();
			return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(addTime));
			 
		}
		[Test]
		public void DateValidator_NotValidDate_ReturnErrorMessage() {
			var result = new DateInFutureAttribute();
			Assert.That(result.ErrorMessage, Is.EqualTo("Date must be in the future"));
		}
	}
}

