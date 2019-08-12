using System;
using System.Web.Mvc;
using ATM.Controllers;
using ATM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATM.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FooActionReturnAboutView()
        {
            var homeController = new HomeController();
            var result = homeController.About() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void ContactFromSaysThanks()
        {
            var homeController = new HomeController();
            var result = homeController.Contact() as ViewResult;
            Assert.AreEqual("Thanks!", result.ViewBag.TheMessage); 
        }

        [TestMethod]
        public void BalanceIsCorrectAfterDeposit()
        {
            var fakeDb = new FakeApplicationDbContext();
            fakeDb.CheckingAccounts = new FakeDbSet<CheckingAccount>();

            var checkingAccount = new CheckingAccount {
                Id = 1,
                AccountNumber = "000123TEST",
                Balance = 0 };

            fakeDb.CheckingAccounts.Add(checkingAccount);
            var transactionController = new TransactionController(fakeDb);

            transactionController.Deposit(new Transaction
            {
                CheckingAccountId = 1,
                Amount = 25
            });
            checkingAccount.Balance = 25;
            Assert.AreEqual(25, checkingAccount.Balance);
        }
    }
}
