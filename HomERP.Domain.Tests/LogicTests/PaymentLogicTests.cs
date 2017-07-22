using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic;

namespace HomERP.Domain.Tests.LogicTests
{
    [TestClass]
    public class PaymentLogicTests
    {
        private Payment PrepareExamplePayment()
        {
            return new Payment
            {
                Amount = 102,
                Direction = Helpers.CashFlowDirection.Increase,
                Time = new DateTime(2017, 1, 1, 10, 0, 0)
            };
        }
        [TestMethod]
        public void Test_SavePayment()
        {
            Payment payment = PrepareExamplePayment();
            Mock<IPaymentRepository> mock = new Mock<IPaymentRepository>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Amount=100, Direction= Helpers.CashFlowDirection.Decrease},
                new Payment { Amount=65.02m, Direction= Helpers.CashFlowDirection.Decrease}
            }
                );
            PaymentProvider provider = new PaymentProvider(mock.Object);
            //act
            provider.SavePayment(payment);
            //assert
            mock.Verify(m => m.SavePayment(payment));
        }
    }
}
