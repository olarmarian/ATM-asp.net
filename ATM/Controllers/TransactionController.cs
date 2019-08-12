using ATM.Models;
using ATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IApplicationDbContext db;

        public TransactionController()
        {
            db = new ApplicationDbContext();
        }

        public TransactionController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }


        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction )
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public ActionResult Withdraw(int checkingAccountId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Amount = transaction.Amount * (-1);
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult QuickCash(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Amount = -100;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult TransferFunds(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransferFunds(string transferToId, Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var service = new CheckingAccountService(db);
                try
                {
                    transaction.Amount = transaction.Amount * (-1);
                    db.Transactions.Add(transaction);
                    service.UpdateBalance(transaction.CheckingAccountId);
                    transaction.Amount = transaction.Amount * (-1);
                    transaction.CheckingAccountId = int.Parse(transferToId);
                    db.Transactions.Add(transaction);
                    service.UpdateBalance(transaction.CheckingAccountId);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                db.SaveChanges();
                
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}