
using System;

namespace Grupparbete_Bankapp
{
    public class BankMeny
    {
        private Bank bank;
        private TransactionInput transactionInput;

        public BankMeny(Bank bank)
        {
            this.bank = bank;
            transactionInput = new TransactionInput(bank.Transactions);
        }

        // Here I open the transaction menu for the user who is logged in.

        internal void ShowTransactions(Account user)
        {
            TransactionMenu transactionMenu = new TransactionMenu(bank.Transactions);

            transactionMenu.Show(user);
        }


        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("==========================");
            Console.WriteLine("      CHAS BANK");
            Console.WriteLine("==========================");

            Console.ResetColor();

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("1. Visa bankinformation");
                Console.WriteLine("0. Avsluta");

                Console.Write("Välj: ");
                // Here I read the menu choice while checking if waiting transfers are ready to run.
                string choice = transactionInput.ReadText("");

                if (choice == "1")
                {
                    bank.ShowInfo();
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Hej då!");
                    running = false;
                }
                else
                {
                    Console.WriteLine("Fel val. Försök igen.");
                }
            }
        }
    }
}
