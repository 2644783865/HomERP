using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;

using HomERP.Domain.Entity;
using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Logic;
using System.Threading.Tasks;

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
                Time = new DateTime(2017, 1, 1, 10, 0, 0)
            };
        }

        [TestMethod]
        public void Should_Get_All_Payments()
        {
            //arrange
            Mock<IPaymentRepository> mock = new Mock<IPaymentRepository>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Amount=100},
                new Payment { Amount=65.02m}
            }.AsQueryable());
            PaymentProvider provider = new PaymentProvider(mock.Object);
            //act
            IEnumerable<Payment> Payments = provider.Payments;
            //assert
            Payments.Should().HaveCount(2, "you have 2 entities in the repository");
        }

        [TestMethod]
        public async Task Should_Call_UserProvider_SavePayment()
        {
            Payment payment = PrepareExamplePayment();
            Mock<IPaymentRepository> mock = new Mock<IPaymentRepository>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Amount=100},
                new Payment { Amount=65.02m}
            }.AsQueryable());
            mock.Setup(m => m.SavePaymentAsync(It.IsAny<Payment>())).Returns(Task.FromResult<bool>(true));
            PaymentProvider provider = new PaymentProvider(mock.Object);
            //act
            bool result = await provider.SavePaymentAsync(payment);
            //assert
            //In this place we have to focus on that underlying repository method has been properly called
            //instead of wandering if entity has been properly saved - this is the repository responsibility.
            mock.Verify(m => m.SavePaymentAsync(payment));
            result.Should().BeTrue();

        }

        [TestMethod]
        public async Task Should_Call_UserProvider_DeletePayment()
        {
            //arrange
            Mock<IPaymentRepository> mock = new Mock<IPaymentRepository>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Id = 1, Amount=100},
                new Payment { Id = 2, Amount=65.02m}
            }.AsQueryable());
            Payment paymentToDelete = mock.Object.Payments.Where(p=>p.Id==2).First();
            PaymentProvider provider = new PaymentProvider(mock.Object);
            //act
            await provider.DeletePaymentAsync(paymentToDelete.Id);
            //assert if repository delete method has been called with proper identifier
            mock.Verify(m => m.DeletePaymentAsync(2));
        }
    }
}
