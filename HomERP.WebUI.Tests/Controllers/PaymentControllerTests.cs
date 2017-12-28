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
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using HomERP.WebUI.Handlers.Abstract;
using HomERP.WebUI.Models.PaymentViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomERP.WebUI.Tests.Controllers
{
    [TestClass]
    public class PaymentControllerTests
    {
        [TestMethod]
        public void Should_Call_PaymentProvider_When_Showing_Payments()
        {
            //arrange
            Mock<IPaymentHandler> mock = new Mock<IPaymentHandler>();
            mock.Setup(m => m.Payments).Returns(new PaymentVM[]
            {
                new PaymentVM { Amount=100 },
                new PaymentVM { Amount=200 }
            }.AsQueryable());
            PaymentController controller = new PaymentController(mock.Object);
            //act
            var result = controller.Index();
            //assert
            mock.Verify(m => m.Payments);
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Should_Display_Payment_Edit_Screen_With_Select_Source()
        {
            //arrange
            Mock<IPaymentHandler> mock = new Mock<IPaymentHandler>();
            mock.Setup(m => m.Edit(It.IsAny<int>())).Returns(new PaymentEditVM
            {
                Amount = 100, CashAccountId = 2, Id = 2, Time = new DateTime(2017, 12, 1),
                CashAccountList = new SelectListItem[] { }
            });
            PaymentController controller = new PaymentController(mock.Object);
            //act
            var result = controller.Edit(2);
            //assert
            mock.Verify(p => p.Edit(2));
            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeOfType<PaymentEditVM>();
        }

        //[TestMethod]
        //public void Should_Call_SavePayment_When_Submitting_Changes()
        //{
        //    //arrange
        //    Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
        //    Payment payment = new Payment { Id = 1, Amount = 100, Time = DateTime.Now };
        //    mock.Setup(m => m.Payments).Returns(new[]
        //    {
        //        payment,
        //        new Payment{Id = 2, Amount = 50, Time = new DateTime(2017, 1, 1) }
        //    }.AsQueryable());
        //    PaymentController controller = new PaymentController(mock.Object);
        //    PaymentEditVM model = new PaymentEditVM
        //    {
        //        Payment = payment
        //    };
        //    //act
        //    controller.Edit(model);
        //    //assert
        //    mock.Verify(m => m.SavePayment(payment));
        //}


        //[TestMethod]
        //public void Should_Call_Delete_Given_Payment_And_Redirect_To_Index()
        //{
        //    //arrange
        //    Mock<IPaymentProvider> mock = new Mock<IPaymentProvider>();
        //    mock.Setup(m => m.Payments).Returns(new Payment[]
        //    {
        //        new Payment { Id = 1, Amount = 100 },
        //        new Payment { Id = 2, Amount = 200 }
        //    }.AsQueryable());
        //    PaymentController controller = new PaymentController(mock.Object);
        //    int itemToDeleteId = 2;
        //    //act
        //    var result = controller.Delete(itemToDeleteId);
        //    //assert
        //    mock.Verify(p => p.DeletePayment(itemToDeleteId));
        //    result.Should().BeOfType<RedirectToActionResult>();
        //}

        [TestMethod]
        public void Verify_PaymentController_Is_Decorated_With_Authorize_Attribute()
        {
            var type = typeof(PaymentController);
            var methodInfo = type.GetMethod("Index", new Type[] {});

            var attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            var classAttributes = type.GetTypeInfo().GetCustomAttributes(typeof(AuthorizeAttribute));

            attributes.Should().BeEmpty();
            classAttributes.Should().NotBeEmpty();
        }
    }
}
