using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Grupparbete_Bankapp
{
    // This is an account with money. Account is the group login account. 
    internal class BankAccount
    {

        public int Number { get; set; }
        public string OwnerUsername { get; set; }

        public string Name { get; set; }

        public Currency Currency { get; set; }

        private decimal balance;

        public decimal Balance
        {
            get
            {
                return balance;

            }

        }


        // the group uses username to connect customers to their bank account
        public BankAccount(int number, string ownerUsername, string name, Currency currency, decimal startingBalance)
        {

            Number = number;
            OwnerUsername = ownerUsername;
            Name = name;
            Currency = currency;
            balance = startingBalance;

        }

        // only positive amount can be added
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            balance = balance + amount;
            return true;
            
        }

        //check the amount and balance before taking money out

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
