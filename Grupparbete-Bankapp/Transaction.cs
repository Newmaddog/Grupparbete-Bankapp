using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    // keep the transfer details and the time when the money should move
    internal class Transaction
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal Amount { get; set; }
        public decimal RecievedAmount { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExecuteAt { get; set; }
        public TransactionStatus Status { get; set; }

        public Transaction(int id, string username, int fromAccount, int toAccount, decimal amount, decimal receivedAmount, Currency fromCurrency, Currency toCurrency, DateTime createdAt, DateTime excuteAt)
        {
            Id = id;
            Username = username;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
            RecievedAmount = receivedAmount;
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;

            CreatedAt = createdAt;

            ExecuteAt = excuteAt;

            Status = TransactionStatus.Completed;
                

        }

    }
}
