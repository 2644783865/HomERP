using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HomERP.Domain.Helpers;

namespace HomERP.Domain.Tests
{
    [TestClass]
    public class BankAccountNumberTests
    {
        [TestMethod]
        public void Test_ParseAccountNumber_When_IncorrectNumber_Returns_False()
        {
            //arrange
            BankAccountNumber account = new BankAccountNumber();
            //act
            bool result = BankAccountNumber.TryParse("15114020170000460202562537", out account);
            //assert
            Assert.IsFalse(result);
            Assert.AreEqual(String.Empty, account.ToString());
        }

        [TestMethod]
        public void Test_ParseAccountNumber_When_CorrectNumber_Returns_True()
        {
            //arrange
            BankAccountNumber account = new BankAccountNumber();
            //act
            bool result = BankAccountNumber.TryParse("15114020170000460202562536", out account);
            //assert
            Assert.IsTrue(result);
            Assert.AreEqual("15 1140 2017 0000 4602 0256 2536", account.ToString());
        }
    }
}
