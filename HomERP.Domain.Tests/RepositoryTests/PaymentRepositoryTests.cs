﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using HomERP.Domain.Repository.Abstract;
using HomERP.Domain.Repository.EntityFramework;
using HomERP.Domain.Entity.Abstract;
using HomERP.Domain.Entity;

namespace HomERP.Domain.Tests.RepositoryTests
{
    [TestClass]
    public class PaymentRepositoryTests
    {
        private EfDbContext context;
        private IPaymentRepository repository;
        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            CashAccount acc = new CashAccount { Id = 1, Name = "Konto" };
            FamilyUser user = new FamilyUser() { Id = 1, Name = "Zenon" };
            context.CashAccounts.Add(acc);
            context.FamilyUsers.Add(user);
            context.SaveChanges();
            repository = new EfPaymentRepository(context);
        }

        [TestMethod]
        public void Should_Add_Payment_To_Context_When_Saving_Repository()
        {
            //arrange
            Payment payment = new Payment
            {
                CashAccount = new CashAccount { Id = 1, Name = "Konto" },
                Amount = 100,
                Direction = Helpers.CashFlowDirection.Increase,
                Time = new DateTime(2017, 1, 1, 12, 0, 0),
                FamilyUser = new FamilyUser() { Id = 1, Name = "Zenon" }
            };
            //act
            repository.SavePayment(payment);
            //assert
            //check if object has been written to the context
            context.Payments.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultPayment = context.Payments.FirstOrDefault();
            resultPayment.Amount.Should().Be(payment.Amount);
            resultPayment.Direction.Should().Be(payment.Direction);
            resultPayment.Time.Should().Be(payment.Time);
        }

        [TestMethod]
        public void Should_Update_Context_When_Updating_Payment()
        {
            //arrange
            Payment payment = new Payment
            {
                CashAccount = new CashAccount { Id = 1, Name = "Konto" },
                Amount = 100,
                Direction = Helpers.CashFlowDirection.Increase,
                Time = new DateTime(2017, 1, 1, 12, 0, 0),
                FamilyUser = new FamilyUser() { Id = 1, Name = "Zenon" }
            };
            repository.SavePayment(payment);
            Payment testPayment = repository.Payments.Where(a => a.Amount == 100).First();
            testPayment.Amount = 120;
            testPayment.Direction = Helpers.CashFlowDirection.Decrease;
            testPayment.Time = new DateTime(2017, 2, 3, 15, 12, 00);
            //act
            repository.SavePayment(testPayment);
            //assert
            context.Payments.Count().Should().Be(1);
            Payment resultPayment = context.Payments.Where(a => a.Amount == testPayment.Amount).First();
            resultPayment.Direction.Should().Be(testPayment.Direction);
            resultPayment.Time.Should().Be(testPayment.Time);
        }

        [TestMethod]
        public void Should_Return_Deleted_Payment_When_Deleting_From_Repository()
        {
            //arrange
            Payment payment = new Payment
            {
                CashAccount = new CashAccount { Id = 1, Name = "Konto" },
                Amount = 100,
                Direction = Helpers.CashFlowDirection.Increase,
                Time = new DateTime(2017, 1, 1, 12, 0, 0),
                FamilyUser = new FamilyUser() { Id = 1, Name = "Zenon" }
            };
            repository.SavePayment(payment);
            //act
            int id = context.Payments.First().Id;
            Payment deletedPayment = repository.DeletePayment(id);
            //assert
            id.Should().NotBe(0, "i already added a Payment to repository, so it should be written to context.");
            deletedPayment.Id.Should().Be(id);
            deletedPayment.Amount.Should().Be(payment.Amount);
            context.Payments.Count().Should().Be(0);
        }

        [TestMethod]
        public void Should_Throw_InvalidOperationException_When_Adding_Payment_With_Nonexistent_Dictionary_Fields()
        {
            //arrange
            Payment payment = new Payment
            {
                CashAccount = new CashAccount { Id = 2, Name = "Konto2" },
                Amount = 100,
                Direction = Helpers.CashFlowDirection.Increase,
                Time = new DateTime(2017, 1, 1, 12, 0, 0),
                FamilyUser = new FamilyUser() { Id = 2, Name = "Marcin" }
            };
            //act
            
            Action test = () =>
            {
                repository.SavePayment(payment);
            };
            //assert
            test.ShouldThrow<InvalidOperationException>();
        }
    }
}
