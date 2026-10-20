
using System;

namespace Grupparbete_Bankapp
{
    public class BankMeny
    {
        private Bank bank;

        public BankMeny(Bank bank)
        {
            this.bank = bank;
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
                string choice = Console.ReadLine() ?? "";

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
