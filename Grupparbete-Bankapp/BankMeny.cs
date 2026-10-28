
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

        // Here I show the bank menu for the user who logged in.
        public void ShowMenu()
        {
            bool running = true;

            while (running == true && transactionInput.EndOfInput == false)
            {
                ShowLogo();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Welcome, {user.GetFullName()}.");
                Console.WriteLine("MAIN MENU");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("1. Show bank information");
                Console.WriteLine("2. Open a bank account");
                Console.WriteLine("3. View my accounts");
                Console.WriteLine("4. Transactions");
                Console.WriteLine("5. Deposit money");

                if (bank.Transactions.IsAdmin(user) == true)
                {
                    Console.WriteLine("6. View all bank accounts");
                    Console.WriteLine("7. Create user");
                }

                Console.WriteLine("0. Log out");

                string choice = transactionInput.ReadText("Choose an option: ");

                switch (choice)
                {
                    case "1":
                        transactionInput.ShowScreen("BANK INFORMATION");
                        bank.ShowInfo();
                        transactionInput.WaitForBack();
                        break;

                    case "2":
                        OpenNewAccount();
                        break;

                    case "3":
                        transactionInput.ShowScreen("MY ACCOUNTS");
                        bank.ShowAccountsForUser(user.GetUserName());
                        transactionInput.WaitForBack();
                        break;

                    case "4":
                        ShowTransactionsMenu();
                        break;

                    case "5":
                        Deposit();
                        break;

                    case "6":
                        transactionInput.ShowScreen("ALL BANK ACCOUNTS");

                        if (bank.Transactions.IsAdmin(user) == true)
                        {
                            bank.ShowAllAccounts();
                            transactionInput.WaitForBack();
                        }
                        else
                        {
                            transactionInput.ShowError("Only administrators can view all accounts.");
                        }
                        break;

                    case "7":
                        systemAccount.CreateAccountAsAdmin();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        transactionInput.ShowError("Please choose one of the listed numbers.");
                        break;
                }
            }
        }

        // Here I clear the old screen and show the bank logo.
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

        // Here I open a bank account for the user who logged in.
        private void OpenNewAccount()
        {
            transactionInput.ShowScreen("OPEN A BANK ACCOUNT");
            Console.WriteLine("0. Cancel");

            string accountName = transactionInput.ReadText("Account name: ").Trim();

            if (accountName == "0" || transactionInput.EndOfInput == true)
            {
                return;
            }

            if (accountName == "")
            {
                transactionInput.ShowError("Account name cannot be empty.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("1. Regular account");
            Console.WriteLine("2. Savings account");
            Console.WriteLine("0. Cancel");

            string typeChoice = transactionInput.ReadText("Account type: ");

            if (typeChoice == "0" || transactionInput.EndOfInput == true)
            {
                return;
            }

            BankAccountType accountType = BankAccountType.Regular;

            if (typeChoice == "2")
            {
                accountType = BankAccountType.Savings;
            }
            else if (typeChoice != "1")
            {
                transactionInput.ShowError("Please choose 1, 2 or 0.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("1. SEK");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. USD");
            Console.WriteLine("0. Cancel");

            string currencyChoice = transactionInput.ReadText("Currency: ");

            if (currencyChoice == "0" || transactionInput.EndOfInput == true)
            {
                return;
            }

            Currency currency = Currency.SEK;

            if (currencyChoice == "2")
            {
                currency = Currency.EUR;
            }
            else if (currencyChoice == "3")
            {
                currency = Currency.USD;
            }
            else if (currencyChoice != "1")
            {
                transactionInput.ShowError("Please choose 1, 2, 3 or 0.");
                return;
            }

            // Here I use the logged-in user as the account owner.
            BankAccount account = bank.OpenNewAccount(user.GetUserName(), accountName, currency, accountType);

            transactionInput.ShowScreen("BANK ACCOUNT CREATED");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your bank account has been created.");
            Console.ResetColor();

            Console.WriteLine($"Account number: {account.Number}");
            Console.WriteLine($"Name: {account.Name}");
            Console.WriteLine($"Type: {account.AccountType}");
            Console.WriteLine($"Balance: {account.Balance:F2} {account.Currency}");

            transactionInput.WaitForBack();
        }

        // Here I put a deposit in the queue for the next quarter-hour.
        private void Deposit()
        {
            transactionInput.ShowScreen("DEPOSIT MONEY");

            if (bank.Transactions.GetAccounts(user).Count == 0)
            {
                transactionInput.ShowError("Open a bank account first.");
                return;
            }

            bank.ShowAccountsForUser(user.GetUserName());
            Console.WriteLine();

            int accountNumber = transactionInput.ReadNumber("Account number (0 to cancel): ");

            if (accountNumber == 0 || transactionInput.EndOfInput == true)
            {
                return;
            }

            decimal amount = transactionInput.ReadDecimal("Amount in the account's currency (0 to cancel): ");

            if (amount == 0 || transactionInput.EndOfInput == true)
            {
                return;
            }

            bool added = bank.Transactions.Deposit(user, accountNumber, amount);

            if (added == false)
            {
                transactionInput.ShowError("Use your own account and an amount above 0 and up to 10000 with at most two decimal places.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Deposit scheduled for {bank.Transactions.GetNextProcessingTime():HH:mm}.");
            Console.ResetColor();

            foreach (BankAccount account in bank.Transactions.GetAccounts(user))
            {
                if (account.Number == accountNumber && account.AccountType == BankAccountType.Savings)
                {
                    // Here I show how much interest the deposit could earn in one year.
                    decimal interest = account.GetSavingsInterest(amount);

                    Console.WriteLine($"Estimated interest for one year at 2%: {interest:F2} {account.Currency}");
                }
            }

            transactionInput.WaitForBack();


        }

        // Here I open transactions using the bank's service and the logged-in user.
        private void ShowTransactionsMenu()
        {
            TransactionMenu transactionMenu = new TransactionMenu(bank.Transactions);
            transactionMenu.Show(user);
        }
    }
}
