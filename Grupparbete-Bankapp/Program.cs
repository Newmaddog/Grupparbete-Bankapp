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
            Bank bank = new Bank("CHAS BANK");
            TransactionInput input = new TransactionInput(bank.Transactions);
            SystemAccount systemAccount = new SystemAccount(input);

            Account? user = systemAccount.LogInAccount();

            if (systemAccount.SystemBlockStatus())
            {
                Console.WriteLine("Error to many attempt system block!!!");
                System.Environment.Exit(1);
            }

            if (user == null)
            {
                return;
            }

            // Here I use the same user list in the bank menu.
            BankMeny bankMeny = new BankMeny(bank, user, systemAccount);
            bankMeny.ShowMenu();
        }
    }
}