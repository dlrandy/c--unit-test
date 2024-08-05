using System;
using Moq;
using Sparky_lib;

namespace Sparky_Nunit_Test
{
	[TestFixture]
	public class BankAccountNUnitTests
	{
		private BankAccount account;

		[SetUp]
		public void SetUp()
		{
			 
		}
		[Test]
		public void BankDeposit_Add100_returnTrue() {
			var bankAccount = new BankAccount(new LogBook());
			var result = bankAccount.Deposit(100);
			Assert.IsTrue(result);
			Assert.That(bankAccount.GetBalance,Is.EqualTo(100));
		}

		[Test]
		public void BankDepositWithMock_Add100_returnTrue() {
			var logMock = new Mock<ILogBook>();
			logMock.Setup(x => x.Message("Deposit invoked in testing...."));
			var bankAccount = new BankAccount(logMock.Object);
			var result = bankAccount.Deposit(100);
			Assert.IsTrue(result);
			Assert.That(bankAccount.GetBalance,Is.EqualTo(100));
		}
	}
}

