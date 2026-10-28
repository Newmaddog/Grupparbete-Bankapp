
using System;
using System.Collections.Generic;

namespace Grupparbete_Bankapp
{
    // Displays the transaction menu for the logged-in user.
    internal class TransactionMenu
    {
        private TransactionService service;
        private TransactionInput input;

        // Here I connect the menu to the bank's accounts and transfers.
        public TransactionMenu(TransactionService service)
        {
            this.service = service;
            input = new TransactionInput(service);
        }

        // Here I clear the old menu and show the transaction choices.
        public void Show(Account user)
        {
            bool running = true;

            while (running == true && input.EndOfInput == false)
            {
                service.ProcessTransactions(DateTime.Now);

                input.ShowScreen("TRANSACTIONS");
                Console.WriteLine($"User: {user.GetUserName()}");

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
                        input.ShowError("Please choose one of the listed numbers.");
                        break;
                }
            }
        }

        // Here I show this user's accounts and the money still available.
        private void ShowAccounts(Account user)
        {
            List<BankAccount> accounts = service.GetAccounts(user);

            if (accounts.Count == 0)
            {
                Console.WriteLine("You do not have any bank accounts yet.");
            }

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine($"Account: {account.Number} | {account.Name} | {account.AccountType}");
                Console.WriteLine($"Balance: {account.Balance:F2} {account.Currency}");
                Console.WriteLine($"Available: {service.GetAvailableBalance(account):F2} {account.Currency}");
                Console.WriteLine();
            }
        }

        // Here I queue a transfer to another account and let 0 cancel.
        private void Transfer(Account user)
        {
            input.ShowScreen("TRANSFER MONEY");

            if (service.GetAccounts(user).Count == 0)
            {
                input.ShowError("Open a bank account first.");
                return;
            }

            ShowAccounts(user);

            int fromNumber = input.ReadNumber("From account number (0 to cancel): ");

            if (fromNumber == 0 || input.EndOfInput == true)
            {
                return;
            }

            int toNumber = input.ReadNumber("To account number (0 to cancel): ");

            if (toNumber == 0 || input.EndOfInput == true)
            {
                return;
            }

            decimal amount = input.ReadDecimal("Amount in the sender's currency (0 to cancel): ");

            if (amount == 0 || input.EndOfInput == true)
            {
                return;
            }

            bool added = service.Transfer(user, fromNumber, toNumber, amount);

            if (added == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Transfer scheduled for {service.GetNextProcessingTime():HH:mm}.");
                Console.ResetColor();

                input.WaitForBack();
            }
            else
            {
                input.ShowError("Check the account numbers, your available balance, the amount and today's exchange rates.");
            }
        }

        // Here I show this user's waiting, completed and failed transactions.
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
                    Console.WriteLine($"Amount: {transaction.Amount:F2} {transaction.ToCurrency}");
                }
                else
                {
                    Console.WriteLine($"From account: {transaction.FromAccount}");
                    Console.WriteLine($"To account: {transaction.ToAccount}");
                    Console.WriteLine($"Sent: {transaction.Amount:F2} {transaction.FromCurrency}");
                    Console.WriteLine($"Received: {transaction.ReceivedAmount:F2} {transaction.ToCurrency}");
                }

                Console.WriteLine($"Status: {transaction.Status}");
                Console.WriteLine($"Created: {transaction.CreatedAt:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Scheduled: {transaction.ExecuteAt:yyyy-MM-dd HH:mm}");
                Console.WriteLine();
            }
        }


        // Here admin enters today's EUR and USD rates in SEK.
        private void UpdateRates(Account user)
        {
            input.ShowScreen("EXCHANGE RATES");

            if (service.IsAdmin(user) == false)
            {
                input.ShowError("Only administrators can update exchange rates.");
                return;
            }

            decimal euro = input.ReadDecimal("SEK for 1 EUR (0 to cancel): ");

            if (euro == 0 || input.EndOfInput == true)
            {
                return;
            }

            decimal dollar = input.ReadDecimal("SEK for 1 USD (0 to cancel): ");

            if (dollar == 0 || input.EndOfInput == true)
            {
                return;
            }

            bool updated = service.UpdateRates(user, euro, dollar);

            if (updated == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Exchange rates saved for {service.GetRatesDate():yyyy-MM-dd}.");
                Console.ResetColor();

                input.WaitForBack();
            }
            else
            {
                input.ShowError("Exchange rates must be between 0.0001 and 1000 SEK.");
            }
        }
    }
}


