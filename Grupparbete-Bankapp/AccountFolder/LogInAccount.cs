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

        public Account? LogIn(List<Account> accountList)
        {
            Account? account = FindAccountWithUserName(accountList);
            if (account is null)
                return null;

            if (LogInPasswordSucceded(account))
            {
                WelcomeMessage(account);
                return account;
            }
            return null ;
        }

        public Account? FindAccountWithUserName(List<Account> accountList)
        {
            string? tempUserName = "";
            Console.WriteLine("LOG IN");
            Account? tempAccount = null;
            do
            {
                Constants.WriteExitText();
                Console.Write("Username: ");
                tempUserName = input.ReadText("");
                // Here I stop if the user cancels or input ends.
                if (input.EndOfInput == true || Constants.IsNotUsableWordsUtilize(tempUserName))
                {
                    return null;
                }



                foreach (Account account in accountList)
                {
                    if (account.GetUserName() == tempUserName)
                    {
                        tempAccount = account;
                        Console.WriteLine("Username found.");
                        break;
                    }
                }

                if (tempAccount == null)
                {
                    Console.Clear();
                    Console.WriteLine("Username not found. Please try again.");
                }

            } while (tempAccount is null);

            return tempAccount;

        }
        public bool LogInPasswordSucceded(Account account)
        {
            if (account.IsLocked() == true)
            {
                Console.WriteLine("This user is locked.");
                return false;
            }

            while (account.IsLocked() == false && input.EndOfInput == false)
            {
                Constants.WriteExitText();
                string password = input.ReadText("Password: ");

                if (input.EndOfInput == true || Constants.IsNotUsableWordsUtilize(password))
                {
                    return false;
                }

                if (password == account.GetPassWord())
                {
                    account.ResetFailedLoginAttempts();
                    Console.WriteLine("Login successful.");
                    return true;
                }
                // If the user cancels and tries again, the earlier wrong passwords still count.
                account.AddFailedLoginAttempt();
                int attemptsLeft = 3 - account.GetFailedLoginAttempts();
                Console.WriteLine($"Wrong password. Attempts left: {attemptsLeft}");
            }

            Console.WriteLine("This user is locked after three wrong password attempts.");
            return false;
        }

        public void WelcomeMessage(Account account)
        {
            Console.WriteLine($"Welcome to CHAS BANK, {account.GetFullName()}.");
            Console.WriteLine("Your details:");
            Console.WriteLine(account);
            Console.WriteLine("\nPress Enter to continue");

            input.ReadText("");
        }
    }
}
