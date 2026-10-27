using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Here I start the program.
            Start();
        }

        private static void Start()
        {
            SystemAccount systemAccount = new SystemAccount();
            systemAccount.AccountList();
            systemAccount.CreateAccount();

            // Here I keep the user returned by the login.
            Account? user = systemAccount.LogInAccount();

            if (systemAccount.SystemBlockStatus())
            {
                Console.WriteLine("Error to many attempt system block!!!");
                System.Environment.Exit(1);
            }

            // Here I stop if the login did not return a user.
            if (user == null)
            {
                return;
            }

            Bank bank = new Bank("CHAS BANK");

            // Here I send the bank and the logged-in user to the menu.
            BankMeny bankMeny = new BankMeny(bank, user);
            bankMeny.ShowMenu();
        }
    }
}