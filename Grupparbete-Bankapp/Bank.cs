
using System;
using System.Collections.Generic;

namespace Grupparbete_Bankapp
{
    public class Bank
    {
        public string Name { get; private set; }
        private int nextAccountNumber = 1001;

        internal List<BankAccount> BankAccounts { get; private set; }
        internal TransactionService Transactions { get; private set; }

        public Bank(string name)
        {
            Name = name;
            BankAccounts = new List<BankAccount>();
            Transactions = new TransactionService(BankAccounts);
        }

        public void ShowInfo()
        {
            Console.WriteLine("========================");
            Console.WriteLine($"Welcome to {Name}!");
            Console.WriteLine("========================");
        }

        public BankAccount OpenNewAccount(
            string ownerUsername,
            string accountName,
            Currency currency,
            BankAccountType accountType)
        {
            BankAccount newAccount = new BankAccount(
                nextAccountNumber,
                ownerUsername,
                accountName,
                accountType,
                currency,
                0m
            );

            BankAccounts.Add(newAccount);
            nextAccountNumber++;

            return newAccount;
        }

        // Here admin can see all bank accounts and their balances.
        public void ShowAllAccounts()
        {
            if (BankAccounts.Count == 0)
            {
                Console.WriteLine("No bank accounts have been opened yet.");
            }

            foreach (BankAccount account in BankAccounts)
            {
                Console.WriteLine(
                    $"Account: {account.Number} | " +
                    $"Owner: {account.OwnerUsername} | " +
                    $"Name: {account.Name} | " +
                    $"Type: {account.AccountType} | " +
                    $"Currency: {account.Currency} | " +
                    $"Balance: {account.Balance:F2}"
                );
            }
        }

        // Here I show only the accounts that belong to this username.
        public void ShowAccountsForUser(string username)
        {
            bool found = false;

            foreach (BankAccount account in BankAccounts)
            {
                if (account.OwnerUsername == username)
                {
                    found = true;

                    Console.WriteLine(
                        $"Account: {account.Number} | " +
                        $"Name: {account.Name} | " +
                        $"Type: {account.AccountType} | " +
                        $"Currency: {account.Currency} | " +
                        $"Balance: {account.Balance:F2}"
                    );
                }
            }

            if (found == false)
            {
                Console.WriteLine("You do not have any bank accounts yet.");
            }
        }
    }
}
