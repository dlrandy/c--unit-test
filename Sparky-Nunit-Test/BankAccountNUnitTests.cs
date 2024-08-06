﻿using System;
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

        [Test]
		[TestCase(200,100) ]
		[TestCase(200,160) ]
        public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
        {
			var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x=> x>0 ))).Returns(true);
            //logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsAny<int>())).Returns(true);
            var bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
			var result = bankAccount.Withdraw(withdraw);
			Assert.IsTrue(result);
      
        }

		[Test]
		[TestCase(200,360) ]
        public void BankWithdraw_Withdraw360With200Balance_ReturnsFalse(int balance, int withdraw)
        {
			var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);
			//logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x=> x>0 ))).Returns(false);
			//logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x=> x<=0 ))).Returns(true);

			logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            //logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsAny<int>())).Returns(true);
            var bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
			var result = bankAccount.Withdraw(withdraw);
			Assert.IsFalse(result);
      
        }


		[Test]
		public void BankLogDummy_LogMockString_ReturnTrue() {
			var logMock = new Mock<ILogBook>();
			string desiredOuput = "HELLOEWWW";

			//logMock.Setup(x => x.MessageWithReturnString("hi")).Returns((string str) => str.ToUpper());
			logMock.Setup(x => x.MessageWithReturnString(It.IsAny<string>())).Returns((string str) => str.ToUpper());

			Assert.That(logMock.Object.MessageWithReturnString("HeLLOEwww"),Is.EqualTo(desiredOuput));
		}


        [Test]
        public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOuput = "hello";

            logMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(), out desiredOuput)).Returns(true);
			string result = "";
			Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOuput));
        }


        [Test]
        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
			Customer customer = new();
			Customer customerNotUsed = new();

            logMock.Setup(x =>  x.LogWithRefObj(ref customer)).Returns(true);
			 
			Assert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
			Assert.IsFalse(logMock.Object.LogWithRefObj(ref customerNotUsed));
             
        }

		[Test]
        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
			logMock.SetupAllProperties();
			 
            logMock.Setup(x =>  x.LogSeverity).Returns(10);
            logMock.Setup(x =>  x.LogType).Returns("warning");
			logMock.Object.LogSeverity = 100;
			Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
			Assert.That(logMock.Object.LogType, Is.EqualTo("warning"));



			// callbacks
			string logTemp = "Hello, ";
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true)
				.Callback((string str)=> logTemp += str);
			logMock.Object.LogToDb("ben");
			Assert.That(logTemp, Is.EqualTo("Hello, ben"));// callbacks



			int counter = 5;
			logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
				.Callback(()=> counter++)
				.Returns(true)
				.Callback(()=> counter++);
			logMock.Object.LogToDb("ben");
			logMock.Object.LogToDb("ben");
			Assert.That(counter, Is.EqualTo(9));
             
        }
		[Test]
		public void BankLogDummy_VerifyExample() {
			var logMock = new Mock<ILogBook>();
			BankAccount bankAccount = new BankAccount(logMock.Object);
			bankAccount.Deposit(100);
			Assert.That(bankAccount.GetBalance, Is.EqualTo(100));


			logMock.Verify(x => x.Message(It.IsAny<string>()), Times.Exactly(2));
			logMock.Verify(x => x.Message("Test"), Times.AtLeastOnce);
			logMock.VerifySet(x => x.LogSeverity = 101, Times.Once);
			logMock.VerifyGet(x => x.LogSeverity, Times.Once);
		}
    }
}

