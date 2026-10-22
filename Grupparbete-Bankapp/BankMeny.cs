
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
            ShowLogo();
            bank.ShowInfo();

            bool running = true;

            while (running)
            {
                ShowMenu();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Välj ett alternativ: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        bank.ShowInfo();
                        Pause();
                        break;

                    case "2":
                        BankAccount newAccount = bank.OpenNewAccount();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nytt konto har öppnats!");
                        Console.WriteLine($"Kontonummer: {newAccount.AccountNumber}");
                        Console.WriteLine($"Saldo: {newAccount.Balance} kr");
                        Console.ResetColor();

                        Pause();
                        break;

                    // NYTT: Menyval 3 – visar alla bankkonton
                    case "3":
                        bank.ShowAllAccounts(); // Anropar metoden i Bank.cs
                        Pause();
                        break;


                    case "0":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Tack för att du besökte CHAS BANK!");
                        Console.ResetColor();

                        running = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ResetColor();
                        Pause();
                        break;
                }
            }
        }

        private void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"
   _____ _   _    _    ____
  / ____| | | |  / \  / ___|
 | |    | |_| | / _ \ \___ \
 | |____|  _  |/ ___ \ ___) |
  \_____|_| |_/_/   \_\____/
");

            Console.WriteLine("================================");
            Console.WriteLine("          CHAS BANK");
            Console.WriteLine("================================");

            Console.ResetColor();
            Console.WriteLine();
        }

        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine();
            Console.WriteLine("========== HUVUDMENY ==========");

            Console.ResetColor();

            Console.WriteLine("1. Visa bankinformation");
            Console.WriteLine("2. Öppna nytt konto");
            Console.WriteLine("3. Visa alla konton");
            Console.WriteLine("0. Avsluta");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("===============================");
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.WriteLine();
            Console.Write("Tryck Enter för att fortsätta...");
            Console.ReadLine();
        }
    }
}
