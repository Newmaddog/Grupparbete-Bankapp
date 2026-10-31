
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
            BankAccountType accountType,
            Account User)
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
            User.AddBankAccount(newAccount);
            nextAccountNumber++;

            return newAccount;
        }

        public void ShowAllAccounts()
        {
            foreach (BankAccount account in BankAccounts)
            {
                Console.WriteLine(
                    $"Account: {account.Number} | " +
                    $"Owner: {account.OwnerUsername} | " +
                    $"Name: {account.Name} | " +
                    $"Type: {account.AccountType} | " +
                    $"Currency: {account.Currency} | " +
                    $"Balance: {account.Balance}"
                );
            }
        }

        public void ShowAccountsForUser(Account username)
        {
            foreach (BankAccount account in username.GetBankAccountList())
            {
                Console.WriteLine(
                    $"Account: {account.Number} | " +
                    $"Name: {account.Name} | " +
                    $"Type: {account.AccountType} | " +
                    $"Currency: {account.Currency} | " +
                    $"Balance: {account.Balance}"
                );

            }
        }
    }
}
