
using System;

namespace Grupparbete_Bankapp
{
    internal class BankMeny
    {
        private readonly Bank bank;
        

        public BankMeny(Bank bank)
        {
            this.bank = bank;
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

                if (bank.Transactions.IsAdmin(user) == true)
                {
                    Console.WriteLine("  3. Show all bank accounts");
                }

                Console.WriteLine("  4. Show my accounts");
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

                        // here i show the accounts of the user who logged in 

                        bank.ShowAccountsForUser(user);
                        break;

                    case "5":
                        ShowTransactionsMenu();
                        break;

                    case "6":
                        // we have this code elsewhere 
                        //// This method also checks that the user is admin.
                        //systemAccount.CreateAccountAsAdmin();
                        break;

                    case "7":
                        Deposit();
                        break;

                    case "8":
                        ShowLoanOption();
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

            // the new account belongs to the user who logged in
            string username = user.GetFullName();

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
                accountType.Value,
                user
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

            bank.ShowAccountsForUser(user);

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
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== TRANSACTIONS ==========");
            Console.ResetColor();

            Console.WriteLine(
                "Your existing TransactionMenu should be connected here."
            );
        }

        private void ShowLoanOption()
        {
            var accounts = bank.Transactions.GetAccounts(user);

            if (accounts.Count == 0)
            {
                Console.WriteLine("No bank account registered, open one first.");
                return;
            }
            Console.WriteLine("Select the account you want to loan for.");
            foreach (BankAccount account in accounts)
            {
                Console.WriteLine($"Account: {account.Number}");
                Console.WriteLine($"Name: {account.Name}");
                Console.WriteLine($"Balance: {account.Balance} {account.Currency}");
            }

            int accountNumber = transactionInput.ReadNumber("Enter account number: ");

            if (transactionInput.EndOfInput)
            {
                return;
            }

            BankAccount? selectedAccount = null;
            foreach (BankAccount account in accounts)
            {
                if (account.Number == accountNumber)
                {
                    selectedAccount = account;
                    break;
                }
            }

            if (selectedAccount == null)
            {
                Console.WriteLine("Invalid account.");
                return;
            }

            loanMenu.ShowLoanMenu(selectedAccount);

        }

    }
}
