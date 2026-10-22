using System;
using System.Collections.Generic;
using System.Text;


namespace Grupparbete_Bankapp
{
    public class BankAccount
    {
        public int AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public Currency Currency { get; private set; }
        public BankAccount(int accountNumber, Currency currency)
        {
            AccountNumber = accountNumber;
            Balance = 0;
            Currency = currency;
        }
    }
}
