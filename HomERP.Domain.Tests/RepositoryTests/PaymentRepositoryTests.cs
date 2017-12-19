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
    public class PaymentRepositoryTests
    {
        private EfDbContext context;
        private IPaymentRepository repository;
        [TestInitialize]
        public void Initialize()
        {
            context = new EfDbContext(HomERP.Domain.Tests.Context.MemoryContext.GenerateContextOptions());
            CashAccount acc = new CashAccount { Id = 1, Name = "Konto" };
            context.CashAccounts.Add(acc);
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
                Time = new DateTime(2017, 1, 1, 12, 0, 0)
            };
            //act
            repository.SavePayment(payment);
            //assert
            //check if object has been written to the context
            context.Payments.Count().Should().Be(1, "when we add one object to empty collection, there should be only this one.");
            var resultPayment = context.Payments.FirstOrDefault();
            resultPayment.Amount.Should().Be(payment.Amount);
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
                Time = new DateTime(2017, 1, 1, 12, 0, 0)
            };
            repository.SavePayment(payment);
            Payment testPayment = repository.Payments.Where(a => a.Amount == 100).First();
            testPayment.Amount = 120;
            testPayment.Time = new DateTime(2017, 2, 3, 15, 12, 00);
            //act
            repository.SavePayment(testPayment);
            //assert
            context.Payments.Count().Should().Be(1);
            Payment resultPayment = context.Payments.Where(a => a.Amount == testPayment.Amount).First();
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
                Time = new DateTime(2017, 1, 1, 12, 0, 0)
            };
            repository.SavePayment(payment);
            //act
            int id = context.Payments.First().Id;
            bool result = repository.DeletePayment(id);
            //assert
            id.Should().NotBe(0, "i already added a Payment to repository, so it should be written to context.");
            result.Should().BeTrue();
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
                Time = new DateTime(2017, 1, 1, 12, 0, 0)
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
