using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    // Here I handle transfers, currency exchange and transaction history.
    internal class TransactionService
    {
        private List<BankAccount> accounts;

        private List<Transaction> transactions = new List<Transaction>();

        private Queue<Transaction> waiting = new Queue<Transaction>();

        private Dictionary<Currency, decimal> excangeRates = new Dictionary<Currency, decimal>();

        private DateTime ratesUpdatedOn = DateTime.MinValue;

        private DateTime nextProcessingTime;

        //use the bank account list so manoy is stored in one place

        public TransactionService(List<BankAccount> accouts)
        {
            this.accounts = accounts;

            nextProcessingTime = NextQuarter(DateTime.Now);

            excangeRates.Add(Currency.SEK, 1);
            excangeRates.Add(Currency.EUR, 11);
            excangeRates.Add(Currency.USD, 10);



        }

        // Here I get the bank accounts that belong to this customer.
        public List<BankAccount> GetAccounts(Account user)
        {
            List<BankAccount> result = new List<BankAccount>();

            foreach (BankAccount account in accounts)
            {
                if (account.OwnerUsername == user.GetUserName())
                {
                    result.Add(account);

                }

            }
            return result;

        }



        // return the lsit position or -1 if the account number does not exist
        private int FindAccountIndex(int accountNumber)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Number == accountNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        //Here I get the user's role from Account and check if it is Admin.
        public bool IsAdmin(Account user)
        {
            AccountType role = (AccountType)user.GetAccountType();

            return role == AccountType.Admin;
        }


        //admin enters how many sek one eur and one usd are worth today 

        public bool UpdateRates(Account admin, decimal euro, decimal dollar)
        {
            if (IsAdmin(admin) == false)
            {
                return false;
            }

            if (euro < 0.0001m || euro > 1000 || dollar < 0.0001m || dollar > 1000)
            {
                return false;
            }


            excangeRates[Currency.EUR] = euro;
            excangeRates[Currency.USD] = dollar;

            return true;
        }

        // show when the admin saved the rates
        public DateTime GetRatesDate()
        {
            return ratesUpdatedOn;
        }


        //the menu uses thsi to show the next processing time 
        public DateTime GetNextProcessingTime()
        {
            return nextProcessingTime;
        }


        // Here I find the next quarter-hour when transfers should run.
        private DateTime NextQuarter(DateTime time)
        {
            DateTime minute = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Second, 0);
                int minutesToAdd = 15 - time.Minute % 15;

            return minute.AddMinutes(minutesToAdd);
        }

        // Here I check that the amount is over 0, no more than 100000 and has up to two decimals.
        private bool AmountIsValid(decimal amount)
        {
            if (amount <= 0 || amount > 10000)
            {
                return false;
            }
            if ((amount * 100) % 1 != 0)
            {
                return false;
            }
            return false;


        }

        // Here I round the amount to two decimals
        private decimal RoundMoeny(decimal amount)
        {
            decimal cents = amount * 100;
            decimal fraction = cents % 1;

            cents = cents - fraction;

            if (fraction >= 0.5m)
            {
                cents = cents + 1;
            }

            return cents / 100;
        }

        // Here I change the amount to sek and then to the other currency using the rates admin saved .
        private decimal ConvertMoney(decimal amount, Currency from, Currency to)
        {
            if (from == to)
            {
                return amount;
            }

            if (ratesUpdatedOn != DateTime.Today)
            {
                return -1;
            }

            if (excangeRates.ContainsKey(from) == false || excangeRates.ContainsKey(to) == false)
            {
                return -1;
            }

            decimal amountInSek = amount * excangeRates[from];
            decimal received = amountInSek / excangeRates[to];

            return RoundMoeny(received);

               


        }





    }
}
