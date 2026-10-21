using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
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

        // Here I subtract waiting transfers to find how much money the customer can still use.
        public decimal GetAvailableBalance(BankAccount account)
        {
            decimal available = account.Balance;
            foreach (Transaction transaction in waiting)
            {
                if (transaction.FromAccount == account.Number)
                {
                    available = available - transaction.Amount;
                }
            }
            return available;
        }


        public bool Transfer(Account user, int fromNumber, int toNumber, decimal amount)

        {
            DateTime createdAt = DateTime.Now;

            ProcessTransactions(createdAt);

            if (AmountIsValid(amount) == false || fromNumber == toNumber)
            {
                return false;
            }
            int fromIndex = FindAccountIndex(fromNumber);
            int toIndex = FindAccountIndex(toNumber);

            if (fromIndex == -1 || toIndex == -1)
            {
                return false;
            }

            BankAccount from = accounts[fromIndex];
            BankAccount to = accounts[toIndex];

            if (from.OwnerUsername != user.GetUserName() || amount > GetAvailableBalance(from))
            {
                return false;
            }

            decimal received = ConvertMoney(amount, from.Currency, to.Currency);

            if (received <= 0)
            {
                return false;
            }

            DateTime executeAt = NextQuarter(createdAt);

            Transaction transaction = new Transaction(transactions.Count + 1, user.GetUserName(), fromNumber, toNumber, amount, received, from.Currency, to.Currency, createdAt, executeAt);

            // Here I save the same transfer in the queue and history, so the history updates when its status changes. 
            waiting.Enqueue(transaction);
            transactions.Add(transaction);

            return true;
        }


        // Here I move the money for waiting transfers when their planned time has arrived.
        public int ProcessTransactions(DateTime currentTime)
        {
            int completed = 0;

            if (currentTime < nextProcessingTime)
            {
                return completed;
            }

            while (waiting.Count > 0)
            {
                Transaction transaction = waiting.Peek();

                if (transaction.ExecuteAt > currentTime)
                {
                    break;
                }
                transaction = waiting.Dequeue();

                int fromIndex = FindAccountIndex(transaction.FromAccount);
                int toIndex = FindAccountIndex(transaction.ToAccount);

                bool valid = true;
                // Here I check that both accounts exist before taking money from the sender's account.

                if (fromIndex == -1 || toIndex == -1)
                {
                    valid = false;
                }
                else
                {
                    BankAccount from = accounts[fromIndex];
                    BankAccount to = accounts[toIndex];

                    if (from.OwnerUsername != transaction.Username || from.Currency != transaction.FromCurrency || to.Currency != transaction.ToCurrency)
                    {
                        valid = false;
                    }
                    else if (from.Withdraw(transaction.Amount) == false)
                    {
                        valid = false;
                    }
                    else
                    {
                        to.Deposit(transaction.RecievedAmount);

                    }

                }
                if (valid == true)
                {
                    transaction.Status = TransactionStatus.Completed;
                    completed = completed + 1;
                }
                else
                {
                    transaction.Status = TransactionStatus.Failed;
                }


            }

            nextProcessingTime = NextQuarter(currentTime);
            return completed;

        }

        // Here I get transfers to and from the customer's accounts, with each transfer listed only once.
        public List<Transaction> GetHistory(Account user)
        {
            List<Transaction> result = new List<Transaction>();

            foreach (Transaction transaction in transactions)
            {
                bool ownsAccount = false;
                int fromIndex = FindAccountIndex(transaction.FromAccount);
                int toIndex = FindAccountIndex(transaction.ToAccount);

                if (fromIndex != -1)
                {
                    if (accounts[fromIndex].OwnerUsername == user.GetUserName())
                    {
                        ownsAccount = true;
                    }
                }

                if (toIndex != -1)
                {
                    if (accounts[toIndex].OwnerUsername == user.GetUserName())
                        ownsAccount = true;
                }
                if (ownsAccount == true)
                {
                    result.Add(transaction);
                }

            }

            return result;
        }


    }
}
