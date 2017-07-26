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
    }
}
