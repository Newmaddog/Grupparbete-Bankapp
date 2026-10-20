
using System;

namespace Grupparbete_Bankapp
{
    public class BankSystem
    {
        private Bank bank;

        public BankSystem(Bank bank)
        {
            this.bank = bank;
        }

        public void Start()
        {
            bool running = true;

            Console.WriteLine("================================");
            Console.WriteLine("       WELCOME TO THE BANK      ");
            Console.WriteLine("================================");

            bank.ShowInfo();

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("========= HUVUDMENY ===========");
                Console.WriteLine("1. Visa bankinformation");
                Console.WriteLine("0. Avsluta");
                Console.Write("Välj ett alternativ: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        bank.ShowInfo();
                        break;

                    case "0":
                        Console.WriteLine("Du har avslutat programmet.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }
    }
}
