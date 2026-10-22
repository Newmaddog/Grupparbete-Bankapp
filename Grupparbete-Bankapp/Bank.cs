
using System;
using System.Collections.Generic;

namespace Grupparbete_Bankapp
{
    public class Bank
    {
        public string Name { get; private set; }

        private List<BankAccount> bankAccounts = new List<BankAccount>();
        private int nextAccountNumber = 1001;

        public Bank(string name)
        {
            Name = name;
        }

        public void ShowInfo()
        {
            Console.WriteLine("========================");
            Console.WriteLine($"Välkommen till {Name}!");
            Console.WriteLine("========================");
        }

        public BankAccount OpenNewAccount()
        {
            BankAccount newAccount = new BankAccount(nextAccountNumber);

            bankAccounts.Add(newAccount);
            nextAccountNumber++;

            return newAccount;
        }

        public void ShowAllAccounts()
        {
            // Kontrollera om banken har några konton
            if (bankAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton att visa.");
                return;
            }

            // Skriv ut alla konton
            foreach (BankAccount account in bankAccounts)
            {
                Console.WriteLine($"Kontonummer: {account.AccountNumber}");
                Console.WriteLine($"Saldo: {account.Balance} kr");
                Console.WriteLine("----------------------");
            }
        }
    }
    }
