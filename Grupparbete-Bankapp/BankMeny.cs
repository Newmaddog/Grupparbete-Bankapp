
using System;
using System.Security.Principal;

namespace Grupparbete_Bankapp
{
    internal class BankMeny
    {
        private readonly Bank bank;
        private readonly Account user;
        private TransactionInput transactionInput;
        private SystemAccount systemAccount;
        public BankMeny(Bank bank, Account user, SystemAccount systemAccount)
        {
            this.bank = bank;

            // here i keep the user who logged in
            this.user = user;
            this.systemAccount = systemAccount;

            // Here I let transfers run while the user types or waits.
            transactionInput = new TransactionInput(bank.Transactions);
        }

        public void ShowMenu()
        {
            bool running = true;

            // Show the bank logo when entering the menu
            ShowLogo();

            while (running == true && transactionInput.EndOfInput == false)
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
                
                if (bank.Transactions.IsAdmin(user) == true)
                { 
                    Console.WriteLine("  3. Show all bank accounts"); 
                }

                Console.WriteLine("  4. Show my accounts");
                Console.WriteLine("  5. Transactions");
                if (bank.Transactions.IsAdmin(user) == true)
                {
                    Console.WriteLine("  6. Create user");
                }

                Console.WriteLine("  7. Deposit money");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  0. Log out");
                Console.ResetColor();

                Console.WriteLine("--------------------------------");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Choose an option: ");
                Console.ResetColor();


                // Here I read the choice while checking waiting transfers.
                string choice = transactionInput.ReadText("");

                switch (choice)
                {
                    case "1":
                        bank.ShowInfo();
                        break;

                    case "2":
                        OpenNewAccount();
                        break;

                    case "3":
                        // only admin kan see everyone account
                        if (bank.Transactions.IsAdmin(user) == true)

                        {
                            bank.ShowAllAccounts();
                        }
                        else
                        {
                            Console.WriteLine("Only admin can view all accounts.");
                        }
                        break;

                    case "4":
                       
                        // here i show the accounts of the user who logged in 

                        bank.ShowAccountsForUser(user.GetUserName());
                        break;

                    case "5":
                        ShowTransactionsMenu();
                        break;

                    case "6":
                        // This method also checks that the user is admin.
                        systemAccount.CreateAccountAsAdmin();
                        break;

                    case "7":
                        Deposit();
                        break;


                    case "0":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("You are now logged out.");
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

        public static void ShowLogo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(@"  ____   _   _      _      ____      ____       _      _   _   _  __");
            Console.WriteLine(@" / ___| | | | |    / \    / ___|    | __ )     / \    | \ | | | |/ /");
            Console.WriteLine(@"| |     | |_| |   / _ \   \___ \    |  _ \    / _ \   |  \| | | ' /");
            Console.WriteLine(@"| |___  |  _  |  / ___ \   ___) |   | |_) |  / ___ \  | |\  | | . \");
            Console.WriteLine(@" \____| |_| |_| /_/   \_\ |____/    |____/  /_/   \_\ |_| \_| |_|\_\");

            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Welcome to CHAS BANK.");
            Console.WriteLine();
        }

        private void OpenNewAccount()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== OPEN NEW ACCOUNT ==========");
            Console.ResetColor();

            // the new account belongs to the user who logged in
            string username = user.GetUserName();

            Console.Write("Enter account name: ");
            string accountName = transactionInput.ReadText("");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select account type:");
            Console.ResetColor();

            Console.WriteLine("1. Regular account");
            Console.WriteLine("2. Savings account");
            Console.Write("Choose an option: ");

            string accountTypeChoice = transactionInput.ReadText("");

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

            string currencyChoice = transactionInput.ReadText("");

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

        private void Deposit()
        {
            if (bank.Transactions.GetAccounts(user).Count == 0)
            {
                Console.WriteLine("Open a bank account first.");
                return;
            }

            bank.ShowAccountsForUser(user.GetUserName());

            int accountNumber = transactionInput.ReadNumber("Account number: ");

            decimal amount = transactionInput.ReadDecimal("Amount in the account's currency: ");

            if (transactionInput.EndOfInput == true)
            {
                return;
            }

            bool added = bank.Transactions.Deposit(user, accountNumber, amount);

            if (added == false)
            {
                Console.WriteLine("Check your account number and amount. Use an amount over 0 and up to 10000 with up to two decimals.");
                return;
            }

            Console.WriteLine("The deposit is scheduled for the next quarter-hour.");

            foreach (BankAccount account in bank.Transactions.GetAccounts(user))
            {
                if (account.Number == accountNumber && account.AccountType == BankAccountType.Savings)
                {
                    // Here I show the interest before the deposit goes through.
                    decimal interest = account.GetSavingsInterest(amount);
                    Console.WriteLine($"Estimated interest for one year at 2%: {interest:F2} {account.Currency}");
                }
            }


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
