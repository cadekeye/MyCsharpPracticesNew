using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharpPractices;

namespace AccountTest
{
    [TestClass]
    public class TestAccount
    {
        [TestMethod]
        public void Deposit_And_Withdraw_should_not_accept_negative() {
            //arrange
            double overdraftLimit = 100;
            Account account = new Account(overdraftLimit);

            //act
            double amount = -20;
            bool actual = account.Deposit(amount);
            bool withdrawActual = account.Withdraw(amount);

            //assert
            Assert.AreEqual(false, actual);
            Assert.AreEqual(false, withdrawActual);
        }
    }
}
