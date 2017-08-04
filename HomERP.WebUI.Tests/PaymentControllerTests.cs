using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Moq;

using HomERP.WebUI.Controllers;
using HomERP.Domain.Logic.Abstract;
using HomERP.Domain.Entity;
using HomERP.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomERP.WebUI.Tests
{
    [TestClass]
    public class PaymentControllerTests
    {
        [TestMethod]
        public void Should_Call_PaymentProvider_When_Showing_Payments()
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
            mock.Verify(m => m.Payments);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Call_Payments_And_Related_Repositories_When_Display_Edit_Screen()
        {
            //arrange
            Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Id = 1, Amount = 100 },
                new Payment { Id = 2, Amount = 200 }
            }
            );
            mock.Setup(a => a.CashAccounts).Returns(new CashAccount[]
            {
                new CashAccount{ Id = 1, Name = "Portfel"}
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
            mock.Verify(a => a.CashAccounts);
            mock.Verify(u => u.Users);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Call_SavePayment_When_Submitting_Changes()
        {
            //arrange
            Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
            Payment payment = new Payment { Id = 1, Amount = 100, Time = DateTime.Now };
            mock.Setup(m => m.Payments).Returns(new[]
            {
                payment,
                new Payment{Id = 2, Amount = 50, Time = new DateTime(2017, 1, 1) }
            });
            PaymentController controller = new PaymentController(mock.Object);
            PaymentEditVM model = new PaymentEditVM
            {
                Payment = payment
            };
            //act
            controller.Edit(model);
            //assert
            mock.Verify(m => m.SavePayment(payment));
        }


        [TestMethod]
        public void Should_Call_Delete_Given_Payment_And_Redirect_To_Index()
        {
            //arrange
            Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
            mock.Setup(m => m.Payments).Returns(new Payment[]
            {
                new Payment { Id = 1, Amount = 100 },
                new Payment { Id = 2, Amount = 200 }
            });
            PaymentController controller = new PaymentController(mock.Object);
            int itemToDeleteId = 2;
            //act
            var result = controller.Delete(itemToDeleteId);
            //assert
            mock.Verify(p => p.DeletePayment(itemToDeleteId));
            result.Should().BeOfType<RedirectToActionResult>();
        }
    }
}
