
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

            // Behåller gruppens befintliga transaktionssystem.
            transactionInput = new TransactionInput(bank.Transactions);
        }

        // Gruppens befintliga transaktionsmeny.
        // Den här metoden lämnar vi orörd.
        internal void ShowTransactions(Account user)
        {
            TransactionMenu transactionMenu =
                new TransactionMenu(bank.Transactions);

            transactionMenu.Show(user);
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

                // Läser användarens menyval en gång.
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        // Visar bankens information.
                        bank.ShowInfo();
                        Pause();
                        break;

                    case "2":
                        // Frågar vem kontot ska tillhöra.
                        Console.Write("Ange användarnamn: ");
                        string username = Console.ReadLine() ?? "";

                        // Användaren får namnge sitt konto.
                        Console.Write("Ange kontonamn: ");
                        string accountName = Console.ReadLine() ?? "";

                        Console.WriteLine("Transaktionsmenyn kräver ett Account-objekt.");
                        Pause();
                        break;

                        // Användaren väljer vilken valuta kontot ska ha.
                        Console.WriteLine("\nVälj valuta:");
                        Console.WriteLine("1. SEK");
                        Console.WriteLine("2. EUR");
                        Console.WriteLine("3. USD");
                        Console.Write("Val: ");

                        string currencyChoice =
                            Console.ReadLine() ?? "";

                        // Omvandlar menyvalet till en Currency.
                        // Vi skapar inget konto om valet är ogiltigt.
                        Currency? currency = currencyChoice switch
                        {
                            "1" => Currency.SEK,
                            "2" => Currency.EUR,
                            "3" => Currency.USD,
                            _ => null
                        };

                        if (currency == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ogiltigt valutaval.");
                            Console.ResetColor();
                            Pause();
                            break;
                        }

                        // Banken skapar kontot, sparar det i sin lista
                        // och returnerar det nya BankAccount-objektet.
                        BankAccount newAccount = bank.OpenNewAccount(
                            username,
                            accountName,
                            currency.Value
                        );

                        // Visar information om det skapade kontot.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nNytt konto har öppnats!");
                        Console.WriteLine(
                            $"Kontonummer: {newAccount.Number}");
                        Console.WriteLine(
                            $"Kontonamn: {newAccount.Name}");
                        Console.WriteLine(
                            $"Saldo: {newAccount.Balance} {newAccount.Currency}");
                        Console.ResetColor();

                        Pause();
                        break;

                    case "3":
                        // Visar konton kopplade till det angivna
                        // användarnamnet.
                        Console.Write("Ange ditt användarnamn: ");
                        string searchUsername =
                            Console.ReadLine() ?? "";

                        bank.ShowAccountsForUser(searchUsername);

                        Pause();
                        break;

                    case "0":
                        // Avslutar huvudmenyn.
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(
                            "Tack för att du besökte CHAS BANK!");
                        Console.ResetColor();

                        running = false;
                        break;

                    default:
                        // Hanterar menyval som inte finns.
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

/ | | | |  / \  / |
| |    | || | / _ \ _ 
| ||  _  |/ ___ \ ) |
___|| |//   _____/
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
            Console.WriteLine("3. Visa mina bankkonton");
            Console.WriteLine("4. Transaktioner"); 
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