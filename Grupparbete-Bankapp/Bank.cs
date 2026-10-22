
using System;

namespace Grupparbete_Bankapp
{
    public class Bank
    {
        public string Name { get; private set; }

        // Here I keep the bank accounts in one list that the transaction service also uses.
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
            Console.WriteLine($"Välkommen till {Name}!");
            Console.WriteLine("========================");
        }
    }
}
