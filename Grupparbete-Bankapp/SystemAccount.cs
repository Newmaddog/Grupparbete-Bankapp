using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class SystemAccount
    {
        private List<Account> accountList = new List<Account>();
        AccountCreation accountCreation = new AccountCreation();
        LogInAccount logInAccount = new LogInAccount();

        public SystemAccount()
        {
            Account account1 = new Account("Test1", "12345", "Test1", AccountType.User);
            Account account2 = new Account("Test2", "12345", "Test2", AccountType.Admin);
            Account account3 = new Account("Test3", "12345", "Test3", AccountType.Owner);
            accountList.Add(account1);
            accountList.Add(account2);
            accountList.Add(account3);
        }

        public void CreateAccount()
        {
            Account? account = accountCreation.CreateAccount(accountList);
            if (account != null)
                accountList.Add(account);
        }

        public Account? LogInAccount()
        {
            return logInAccount.LogIn(accountList);

        }

        public void AccountList()
        {
            foreach (Account account in accountList)
            {
                Console.WriteLine(account);
                Console.WriteLine();
            }
        }
    }
}
