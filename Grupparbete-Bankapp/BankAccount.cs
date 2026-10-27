
using System;

namespace Grupparbete_Bankapp
{
    public class BankAccount
    {
        // Account number
        public int Number { get; set; }

        // Account owner
        public string OwnerUsername { get; set; }

        // Account name
        public string Name { get; set; }

        // Account currency
        public Currency Currency { get; set; }

        // Account type: Regular or Savings
        public BankAccountType AccountType { get; set; }

        // Balance is stored privately
        private decimal balance;

        // Other classes can read the balance
        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        // Creates a new bank account
        public BankAccount(
            int number,
            string ownerUsername,
            string name,
            BankAccountType accountType,
            Currency currency,
            decimal startingBalance)
        {
            Number = number;
            OwnerUsername = ownerUsername;
            Name = name;
            AccountType = accountType;
            Currency = currency;
            balance = startingBalance;
        }

        // Deposits money if the amount is positive
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            balance = balance + amount;
            return true;
        }

        // Withdraws money if the amount is positive and the balance is sufficient
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > balance)
            {
                return false;
            }

            balance = balance - amount;
            return true;
        }
    }
}
