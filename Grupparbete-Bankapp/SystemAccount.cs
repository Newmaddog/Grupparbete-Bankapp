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
        private TransactionInput input;

        Account? ActiveAccount;

        public SystemAccount(TransactionInput input)
        {
            //here i let waiting transfer run while admin creates a user
            this.input = input;
            accountCreation = new AccountCreation(input);
            logInAccount = new LogInAccount(input);
            Account account1 = new Account("User1", "12345", "User", AccountType.User);
            Account account2 = new Account("Admin1", "12345", "Admin", AccountType.Admin);
            Account account3 = new Account("Owner1", "12345", "Owner", AccountType.Owner);
            accountList.Add(account1);
            accountList.Add(account2);
            accountList.Add(account3);
        }


        // Here I save the user after admin confirms the details.
        private void CreateAccount()
        {
            Account? account = accountCreation.CreateAccount(accountList);

            if (account != null)
            {
                accountList.Add(account);

                input.ShowScreen("USER CREATED");
                Console.WriteLine($"Username: {account.GetUserName()}");
                Console.WriteLine($"Role: {account.GetAccountType()}");
                input.WaitForBack();
            }
        }

        // Here I check that the logged-in user is admin before creating a user.
        public void CreateAccountAsAdmin()
        {
            if (ActiveAccount == null || ActiveAccount.GetAccountType() != AccountType.Admin)
            {
                input.ShowError("Only administrators can create users.");
                return;
            }

            CreateAccount();

        }

        // Here I keep the user who just logged in.
        public Account? LogInAccount()
        {
            ActiveAccount = logInAccount.LogIn(accountList);
            return ActiveAccount;

        }
        // Here I end the login without deleting the users or their bank accounts.
        public void LogOut()
        {
            ActiveAccount = null;
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
