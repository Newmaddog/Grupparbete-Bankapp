using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class LogInAccount
    {

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
            Console.WriteLine($"Log in");
            Account? tempAccount = null;
            do
            {
                Constants.WriteExitText();
                Console.Write($"Write your UserName please: ");
                tempUserName = Console.ReadLine();

                if (Constants.NotUsableWords.Contains(tempUserName))
                {
                    return null;
                }

                foreach (Account account in accountList)
                {
                    if (account.GetUserName() == tempUserName)
                    {
                        tempAccount = account;
                        Console.WriteLine($"Account with UserName: {tempUserName} found");
                        break;
                    }
                }

                if (tempAccount == null)
                {
                    Console.Clear();
                    Console.WriteLine($"Could not find {tempUserName} in our database!!");
                }

            } while (tempAccount is null);

            return tempAccount;

        }
        public bool LogInPasswordSucceded(Account account)
        {
            string password = account.GetPassWord();
            string attemptPasword = "";
            int attempt = 3;
            do
            {
                Constants.WriteExitText();
                Console.Write($"\nLog in with Password: ");
                attemptPasword = Console.ReadLine() ?? "";

                if (Constants.NotUsableWords.Contains(attemptPasword))
                {
                    Console.Clear();
                    Console.WriteLine("Cancel Log in");
                    return false;
                }

                if (attemptPasword == password)
                {
                    Console.WriteLine("Succes! loggin in");
                    return true;
                }
                Console.Clear();
                Console.WriteLine("wrong password try again");
                attempt--;

            } while (attempt > 0);

            Console.WriteLine("To many attempts, account getting block.");

            return false;
        }

        public void WelcomeMessage(Account account)
        {
            Console.WriteLine($"Welcome {account.GetFullName()} to the bank");
            Console.WriteLine("All your information: ");
            Console.WriteLine(account);
            Console.WriteLine("\nPress Enter to continue");
            Console.ReadLine();
        }
    }
}
