using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Moq;

using HomERP.WebUI.Controllers;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HomERP.WebUI.Tests
{
    [TestClass]
    public class PaymentControllerTests
    {
        [TestMethod]
        public void ShowPaymentsTest()
        {
            //arrange
            Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment{ Amount=100},
                new Payment {Amount=200}
            }
            );
            PaymentController controller = new PaymentController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.Verify(n => n.Payments);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void EditPaymentTest()
        {
            //arrange
            Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Id = 1, Amount = 100 },
                new Payment { Id = 2, Amount = 200 }
            }
            );
            mock.Setup(a => a.Accounts).Returns(new Account[]
            {
                new Account{ Id = 1, Name = "Portfel"}
            });
            mock.Setup(u => u.Users).Returns(new User[]
            {
                new User { Id = 1, Name="Marcin" }
            });
            PaymentController controller = new PaymentController(mock.Object);
            //act
            var result = controller.Edit(1);
            //assert
            mock.Verify(p => p.Payments);
            mock.Verify(a => a.Accounts);
            mock.Verify(u => u.Users);
            result.Should().BeOfType<ViewResult>();
        }
    }
}
