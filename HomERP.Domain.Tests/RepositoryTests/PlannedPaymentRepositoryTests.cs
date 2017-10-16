using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class PlannedPaymentRepositoryTests
    {
        private EfDbContext context;
        private IPlannedPaymentRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            repository = new EfPlannedPaymentRepository(context);
        }

        [TestMethod]
        public void Test_AddPlannedPayment()
        {
            //arrange
            PlannedPayment payment = new PlannedPayment() { Amount = 100, Direction = Helpers.CashFlowDirection.Increase, Time = new DateTime(2017, 1, 1, 12, 0, 0), Status = Helpers.PaymentStatus.Proposal };
            //act
            repository.SavePlannedPayment(payment);
            //assert
            //check if object has been written to the context
            context.PlannedPayments.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultPayment = context.PlannedPayments.FirstOrDefault();
            resultPayment.Amount.Should().Be(payment.Amount);
            resultPayment.Direction.Should().Be(payment.Direction);
            resultPayment.Time.Should().Be(payment.Time);
            resultPayment.Status.Should().Be(payment.Status);
        }

        [TestMethod]
        public void Test_EditPlannedPayment()
        {
            //arrange
            PlannedPayment payment = new PlannedPayment() { Amount = 100, Direction = Helpers.CashFlowDirection.Increase, Time = new DateTime(2017, 1, 1, 12, 0, 0), Status = Helpers.PaymentStatus.Proposal };
            repository.SavePlannedPayment(payment);
            PlannedPayment testPayment = repository.PlannedPayments.Where(a => a.Amount == 100).First();
            testPayment.Amount = 120;
            testPayment.Direction = Helpers.CashFlowDirection.Decrease;
            testPayment.Time = new DateTime(2017, 2, 3, 15, 12, 00);
            testPayment.Status = Helpers.PaymentStatus.Accepted;
            //act
            repository.SavePlannedPayment(testPayment);
            //assert
            context.PlannedPayments.Count().Should().Be(1);
            PlannedPayment resultPayment = context.PlannedPayments.Where(a => a.Amount == testPayment.Amount).First();
            resultPayment.Direction.Should().Be(testPayment.Direction);
            resultPayment.Time.Should().Be(testPayment.Time);
            resultPayment.Status = testPayment.Status;
        }

        [TestMethod]
        public void Test_DeletePlannedPayment()
        {
            //arrange
            PlannedPayment payment = new PlannedPayment() { Amount = 100, Direction = Helpers.CashFlowDirection.Increase, Time = new DateTime(2017, 1, 1, 12, 0, 0)};
            repository.SavePlannedPayment(payment);
            //act
            int id = context.Payments.First().Id;
            Payment deletedPayment = repository.DeletePlannedPayment(id);
            //assert
            id.Should().NotBe(0, "i already added a Payment to repository, so it should be written to context.");
            deletedPayment.Id.Should().Be(id);
            deletedPayment.Amount.Should().Be(payment.Amount);
            context.Payments.Count().Should().Be(0);
        }
    }
}
