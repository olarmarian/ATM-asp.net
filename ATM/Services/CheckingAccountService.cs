using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Services
{
    public class CheckingAccountService
    {
        private IApplicationDbContext db;
        public CheckingAccountService(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        {
            var accountnumber = (123456 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
            var checkingAccount = new CheckingAccount
            {
                FirstName = firstName,
                LastName = lastName,
                AccountNumber = accountnumber,
                Balance = initialBalance,
                ApplicationUserId = userId
            };

            db.CheckingAccounts.Add(checkingAccount);
            db.SaveChanges();
        }

        public void UpdateBalance(int checkingAccountId)
        {
            var checkingAccount = db.CheckingAccounts.Where(c=>c.Id==checkingAccountId).First();
            checkingAccount.Balance = db.Transactions.Where(c => c.CheckingAccountId == checkingAccountId).Sum(c => c.Amount);
            db.SaveChanges();
        }
    }
}