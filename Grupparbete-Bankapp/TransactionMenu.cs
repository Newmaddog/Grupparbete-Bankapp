
using System;
using System.Collections.Generic;

namespace Grupparbete_Bankapp
{
    // Displays the transaction menu for the logged-in user.
    internal class TransactionMenu
    {
        private TransactionService service;
        private TransactionInput input;

        // Displays the menu and handles the user's choices until they choose to go back.
        public TransactionMenu(TransactionService service)
        {
            this.service = service;
            input = new TransactionInput(service);
        }

        public void Show(Account user)
        {
            bool running = true;

            while (running == true && input.EndOfInput == false)
            {
                service.ProcessTransactions(DateTime.Now);

                // Clear the old menu before showing the choices again.
                input.ShowScreen($"TRANSACTIONS - {user.GetUserName()}");

                // Displays the next processing time in hours and minutes, for example 14:15.
                Console.WriteLine($"Next processing: {service.GetNextProcessingTime():HH:mm}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. View my accounts");
                Console.WriteLine("2. Transfer money");
                Console.WriteLine("3. View transaction history");

                if (service.IsAdmin(user) == true)
                {
                    Console.WriteLine("4. Update exchange rates");
                }

                Console.WriteLine("0. Back");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                string choice = input.ReadText("Choose an option: ");
                Console.ResetColor();


                switch (choice)
                {
                    case "1":
                        input.ShowScreen("MY ACCOUNTS");
                        ShowAccounts(user);
                        input.WaitForBack();
                        break;

                    case "2":
                        Transfer(user);
                        break;

                    case "3":
                        input.ShowScreen("TRANSACTION HISTORY");
                        ShowHistory(user);
                        input.WaitForBack();
                        break;

                    case "4":
                        UpdateRates(user);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        input.ShowError("Please choose one of the options.");
                        break;
                }
            }
        }

        // Displays the customer's accounts, balances, and available funds.
        private void ShowAccounts(Account user)
        {
            List<BankAccount> accounts = service.GetAccounts(user);

            if (accounts.Count == 0)
            {
                Console.WriteLine("You do not have any bank accounts yet.");
            }

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(
                    $"{account.Number} | {account.Name} | Type: {account.AccountType} | Balance: {account.Balance} {account.Currency} | Available: {service.GetAvailableBalance(account)}");
            }
        }

        // Enter a transfer or choose 0 to go back without saving it.
        private void Transfer(Account user)
        {
            input.ShowScreen("TRANSFER MONEY");
            ShowAccounts(user);

            if (service.GetAccounts(user).Count == 0)
            {
                input.WaitForBack();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("0. Cancel");

            int fromNumber = input.ReadNumber("From account number: ");

            // Stop here if the user chooses 0 or input has ended.
            if (fromNumber == 0 || input.EndOfInput == true)
            {
                return;
            }

            int toNumber = input.ReadNumber("To account number: ");

            if (toNumber == 0 || input.EndOfInput == true)
            {
                return;
            }

            decimal amount = input.ReadDecimal("Amount in the sender's currency: ");

            if (amount == 0 || input.EndOfInput == true)
            {
                return;
            }

            bool added = service.Transfer(user, fromNumber, toNumber, amount);

            if (added == true)
            {
                Console.WriteLine("The transfer is scheduled for the next quarter-hour.");
            }
            else
            {
                Console.WriteLine("The transfer could not be added. Check the accounts, account ownership, amount, available balance, and today's exchange rates.");
            }

            // Keep the result on screen until the user chooses 0.
            input.WaitForBack();
        }

        // Here I show deposits and transfers that are waiting done or failed.
        private void ShowHistory(Account user)
        {
            List<Transaction> history = service.GetHistory(user);

            if (history.Count == 0)
            {
                Console.WriteLine("You have no transactions in your history.");
            }

            // Here I show each deposit or transfer.

            foreach (Transaction transaction in history)
            {
                Console.WriteLine($"Transaction: {transaction.Id}");

                if (transaction.IsDeposit == true)
                {
                    Console.WriteLine($"Deposit to account: {transaction.ToAccount}");
                    Console.WriteLine($"Amount: {transaction.Amount} {transaction.ToCurrency}");
                }
                else
                {
                    Console.WriteLine($"From account: {transaction.FromAccount}");
                    Console.WriteLine($"To account: {transaction.ToAccount}");
                    Console.WriteLine($"Sent: {transaction.Amount} {transaction.FromCurrency}");
                    Console.WriteLine($"Received: {transaction.ReceivedAmount} {transaction.ToCurrency}");
                }

                Console.WriteLine($"Status: {transaction.Status}");
                Console.WriteLine($"Created: {transaction.CreatedAt:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Scheduled: {transaction.ExecuteAt:yyyy-MM-dd HH:mm}");
                Console.WriteLine();
            }
        }


        // Only users with the admin role can update exchange rates or choose 0 to cancel.
        private void UpdateRates(Account user)
        {
            input.ShowScreen("EXCHANGE RATES");

            if (service.IsAdmin(user) == false)
            {
                input.ShowError("Only administrators can update exchange rates.");
                return;
            }

            Console.WriteLine("0. Cancel");

            decimal euro = input.ReadDecimal("How many SEK is 1 EUR worth? ");

            if (euro == 0 || input.EndOfInput == true)
            {
                return;
            }

            decimal dollar = input.ReadDecimal("How many SEK is 1 USD worth? ");

            if (dollar == 0 || input.EndOfInput == true)
            {
                return;
            }

            if (service.UpdateRates(user, euro, dollar) == true)
            {
                Console.WriteLine($"Exchange rates saved for {service.GetRatesDate():yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine("Exchange rates must be between 0.0001 and 1000 SEK.");
            }

            input.WaitForBack();
        }
    }
}


