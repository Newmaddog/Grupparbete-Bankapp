using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class LogInAccount
    {

        private TransactionInput input;

        public LogInAccount(TransactionInput input)
        {
            this.input = input;
        }


        // Here I return the user only when the password is correct.
        public Account? LogIn(List<Account> accountList)
        {
            Account? account = FindAccountWithUserName(accountList);

            if (account == null)
            {
                return null;
            }

            if (LogInPasswordSucceded(account) == true)
            {
                return account;
            }

            return null;
        }

        // Here I find the user and let 0 cancel the login.

        public Account? FindAccountWithUserName(List<Account> accountList)
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("LOG IN - USERNAME");
                Constants.WriteExitText();

                string username = input.ReadText("Username: ").Trim();

                if (username == "0" || input.EndOfInput == true)
                {
                    return null;
                }

                foreach (Account account in accountList)
                {
                    if (account.GetUserName() == username)
                    {
                        return account;
                    }
                }

                input.ShowError("Username not found. Please try again.");
            }

            return null;

        }

        // Here I check the password and lock the user after three wrong attempts.
        public bool LogInPasswordSucceded(Account account)
        {
            if (account.IsLocked() == true)
            {
                input.ShowError("This user is locked after three wrong password attempts.");
                return false;
            }

            while (account.IsLocked() == false && input.EndOfInput == false)
            {
                input.ShowScreen("LOG IN - PASSWORD");
                Console.WriteLine($"Username: {account.GetUserName()}");
                Constants.WriteExitText();

                string password = input.ReadText("Password: ");

                if (password == "0" || input.EndOfInput == true)
                {
                    return false;
                }

                if (password == account.GetPassWord())
                {
                    account.ResetFailedLoginAttempts();
                    return true;
                }

                // Cancelling later does not remove these wrong attempts.
                account.AddFailedLoginAttempt();

                int attemptsLeft = 3 - account.GetFailedLoginAttempts();

                if (account.IsLocked() == true)
                {
                    input.ShowError("This user is locked after three wrong password attempts.");
                }
                else
                {
                    input.ShowError($"Incorrect password. Attempts left: {attemptsLeft}");
                }
            }

            return false;
        }

       
    }
}
