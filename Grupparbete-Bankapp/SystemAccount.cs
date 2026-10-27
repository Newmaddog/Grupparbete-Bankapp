using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class SystemAccount
    {
        private List<Account> accountList = new List<Account>();

        private AccountCreation accountCreation;
        private LogInAccount logInAccount;

        Account? ActiveAccount;

        public SystemAccount(TransactionInput input)
        {
            //here i let waiting transfer run while admin creates a user
            accountCreation = new AccountCreation(input);
            logInAccount = new LogInAccount(input);
            Account account1 = new Account("User1", "12345", "User", AccountType.User);
            Account account2 = new Account("Admin1", "12345", "Admin", AccountType.Admin);
            Account account3 = new Account("Owner1", "12345", "Owner", AccountType.Owner);
            accountList.Add(account1);
            accountList.Add(account2);
            accountList.Add(account3);
        }

        private void CreateAccount()
        {
            Account? account = accountCreation.CreateAccount(accountList);
            if (account != null)
                accountList.Add(account);
        }

        public void CreateAccountAsAdmin()
        {
            if (ActiveAccount == null || ActiveAccount.GetAccountType() != AccountType.Admin)
            {
                Console.WriteLine("You have to log in as admin first");
                return;
            }

            CreateAccount();

        }

        public Account? LogInAccount()
        {
            ActiveAccount = logInAccount.LogIn(accountList);
            return ActiveAccount;

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
