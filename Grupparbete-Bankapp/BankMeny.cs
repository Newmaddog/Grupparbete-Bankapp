
using System;

namespace Grupparbete_Bankapp
{
    public class BankMeny
    {
        private readonly Bank bank;

        public BankMeny(Bank bank)
        {
            this.bank = bank;
        }

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

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        bank.ShowInfo();
                        Pause();
                        break;

                    case "2":
                        {
                            Console.Write("Ange användarnamn: ");
                            string username = Console.ReadLine() ?? "";

                            Console.Write("Ange kontonamn: ");
                            string accountName = Console.ReadLine() ?? "";

                            if (string.IsNullOrWhiteSpace(username) ||
                                string.IsNullOrWhiteSpace(accountName))
                            {
                                Console.WriteLine(
                                    "Användarnamn och kontonamn måste anges.");
                                Pause();
                                break;
                            }

                            Console.WriteLine("\nVälj valuta:");
                            Console.WriteLine("1. SEK");
                            Console.WriteLine("2. EUR");
                            Console.WriteLine("3. USD");
                            Console.Write("Val: ");

                            string currencyChoice = Console.ReadLine() ?? "";

                            Currency? currency = currencyChoice switch
                            {
                                "1" => Currency.SEK,
                                "2" => Currency.EUR,
                                "3" => Currency.USD,
                                _ => null
                            };

                            if (currency == null)
                            {
                                Console.WriteLine("Ogiltigt valutaval.");
                                Pause();
                                break;
                            }

                            BankAccount newAccount = bank.OpenNewAccount(
                                username,
                                accountName,
                                currency.Value
                            );

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nNytt konto har öppnats!");
                            Console.WriteLine($"Kontonummer: {newAccount.Number}");
                            Console.WriteLine($"Ägare: {newAccount.OwnerUsername}");
                            Console.WriteLine($"Kontonamn: {newAccount.Name}");
                            Console.WriteLine(
                                $"Saldo: {newAccount.Balance} {newAccount.Currency}");
                            Console.ResetColor();

                            Pause();
                            break;
                        }

                    case "3":
                        {
                            Console.Write("Ange ditt användarnamn: ");
                            string username = Console.ReadLine() ?? "";

                            if (string.IsNullOrWhiteSpace(username))
                            {
                                Console.WriteLine("Användarnamn måste anges.");
                                Pause();
                                break;
                            }

                            bank.ShowAccountsForUser(username);
                            Pause();
                            break;
                        }

                    case "4":
                        {
                            Console.Write(
                                "Ange ditt användarnamn för transaktioner: ");

                            string username = Console.ReadLine() ?? "";

                            if (string.IsNullOrWhiteSpace(username))
                            {
                                Console.WriteLine("Användarnamn måste anges.");
                                Pause();
                                break;
                            }

                            Account user = new Account(
                                username,
                                "",
                                username,
                                AccountType.User
                            );

                            ShowTransactions(user);
                            break;
                        }

                    case "5":
                        {
                            DepositMoney();
                            break;
                        }

                    case "0":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(
                            "Tack för att du besökte CHAS BANK!");
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

        private void DepositMoney()
        {
            Console.Write("Ange kontonummer: ");

            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("Ogiltigt kontonummer.");
                Pause();
                return;
            }

            Console.Write("Ange belopp att sätta in: ");

            if (!decimal.TryParse(
                    Console.ReadLine(),
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ange ett giltigt belopp.");
                Pause();
                return;
            }

            BankAccount? account = null;

            foreach (BankAccount item in bank.BankAccounts)
            {
                if (item.Number == accountNumber)
                {
                    account = item;
                    break;
                }
            }

            if (account == null)
            {
                Console.WriteLine("Kontot hittades inte.");
            }
            else if (account.Deposit(amount))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nInsättningen lyckades!");
                Console.WriteLine($"Kontonummer: {account.Number}");
                Console.WriteLine(
                    $"Nytt saldo: {account.Balance} {account.Currency}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Insättningen misslyckades.");
            }

            Pause();
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
            Console.WriteLine("5. Sätt in pengar");
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
