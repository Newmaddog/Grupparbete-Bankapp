
using System;
using System.Collections.Generic;
using System.Text;

/*
    GAMLA VERSIONEN – SPARAD SOM KOMMENTAR

    Vi behåller den tillfälligt så att du kan jämföra
    med gruppens befintliga BankAccount-klass.

namespace Grupparbete_Bankapp
{
    public class BankAccount
    {
        private int nextAccountNumber;

        public int AccountNumber { get; private set; }

        public decimal Balance { get; private set; }

        public Currency Currency { get; private set; }

        public Bankaccount(int accountNumber, Currency currency)
        {
            AccountNumber = accountNumber;
            Balance = 0;
            Currency = currency;
        }

        public BankAccount(int nextAccountNumber)
        {
            this.nextAccountNumber = nextAccountNumber;
        }
    }
}
*/

namespace Grupparbete_Bankapp
{
    // GRUPPENS AKTIVA BANKKONTO
    public class BankAccount
    {
        // Kontots nummer
        public int Number { get; set; }

        // Användaren som äger kontot
        public string OwnerUsername { get; set; }

        // Kontots namn, exempelvis "Sparkonto"
        public string Name { get; set; }

        // Kontots valuta, exempelvis SEK eller USD
        public Currency Currency { get; set; }

        // Saldo lagras privat så att det inte kan ändras direkt utifrån
        private decimal balance;

        // Andra klasser kan läsa saldot
        public decimal Balance
        {
            get
            {
                return balance;
            }
        }

        // Skapar ett konto med kontonummer, ägare, namn, valuta och startsaldo
        public BankAccount(
            int number,
            string ownerUsername,
            string name,
            Currency currency,
            decimal startingBalance)
        {
            Number = number;
            OwnerUsername = ownerUsername;
            Name = name;
            Currency = currency;
            balance = startingBalance;
        }

        // Sätter bara in pengar om beloppet är positivt
        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            balance = balance + amount;
            return true;
        }

        // Tar bara ut pengar om beloppet är positivt och saldot räcker
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
