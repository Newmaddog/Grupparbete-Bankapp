
using System;

namespace Grupparbete_Bankapp
{
    internal class BankMeny
    {
        private readonly Bank bank;
        private readonly Account user;
        public BankMeny(Bank bank, Account user)
        {
            this.bank = bank;

            // here i keep the user who logged in
            this.user = user;
        }

        public void ShowMenu()
        {
            bool running = true;

            // Show the bank logo when entering the menu
            ShowLogo();

            while (running)
            {
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================");
                Console.WriteLine("           MAIN MENU");
                Console.WriteLine("================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  1. Show bank information");
                Console.WriteLine("  2. Open new account");
                Console.WriteLine("  3. Show all bank accounts");
                Console.WriteLine("  4. Show accounts for a user");
                Console.WriteLine("  5. Transactions");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  0. Exit");
                Console.ResetColor();

                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Choose an option: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        bank.ShowInfo();
                        break;

                    case "2":
                        OpenNewAccount();
                        break;

                    case "3":
                        bank.ShowAllAccounts();
                        break;

                    case "4":
                        Console.Write("Enter username: ");
                        string username = Console.ReadLine() ?? "";
                        bank.ShowAccountsForUser(username);
                        break;

                    case "5":
                        ShowTransactionsMenu();
                        break;

                    case "0":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("Thank you for using CHAS BANK!");
                        Console.WriteLine("Goodbye!");
                        Console.ResetColor();
                        running = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void ShowLogo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                @"   ______ _   _    _    ____   ____    _    _   _ _  __"
            );
            Console.WriteLine(
                @"  / ___/ | | |  / \  / ___| / ___|  / \  | \ | | |/ /"
            );
            Console.WriteLine(
                @" | |   | |_| | / _ \| |    | |     / _ \ |  \| | ' /"
            );
            Console.WriteLine(
                @" | |___|  _  |/ ___ \ |___ | |___ / ___ \| |\  | . \"
            );
            Console.WriteLine(
                @"  \____|_| |_/_/   \_\____| \____/_/   \_\_| \_|_|\_\"
            );

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                    CHAS BANK");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("======================================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          Welcome to your banking experience!");
            Console.ResetColor();

            Console.WriteLine();
        }

        private void OpenNewAccount()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== OPEN NEW ACCOUNT ==========");
            Console.ResetColor();

            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write("Enter account name: ");
            string accountName = Console.ReadLine() ?? "";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select account type:");
            Console.ResetColor();

            Console.WriteLine("1. Regular account");
            Console.WriteLine("2. Savings account");
            Console.Write("Choose an option: ");

            string accountTypeChoice = Console.ReadLine() ?? "";

            BankAccountType? accountType = accountTypeChoice switch
            {
                "1" => BankAccountType.Regular,
                "2" => BankAccountType.Savings,
                _ => null
            };

            if (accountType == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid account type.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select currency:");
            Console.ResetColor();

            Console.WriteLine("1. SEK");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. USD");
            Console.Write("Choose an option: ");

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid currency.");
                Console.ResetColor();
                return;
            }

            BankAccount newAccount = bank.OpenNewAccount(
                username,
                accountName,
                currency.Value,
                accountType.Value
            );

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Account created successfully!");
            Console.ResetColor();

            Console.WriteLine($"Account number: {newAccount.Number}");
            Console.WriteLine($"Account name:   {newAccount.Name}");
            Console.WriteLine($"Account type:   {newAccount.AccountType}");
            Console.WriteLine($"Currency:       {newAccount.Currency}");
            Console.WriteLine($"Balance:        {newAccount.Balance}");

            Console.WriteLine();
        }

        private void ShowTransactionsMenu()
        {
            // here i use the same transaction as the bank 
            TransactionMenu transactionMenu = new TransactionMenu(bank.Transactions);

            // here i open the transaktion menu for the logged in user 
            transactionMenu.Show(user);
        }
    }
}
