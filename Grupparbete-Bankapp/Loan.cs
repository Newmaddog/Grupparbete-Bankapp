using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class Loan
    {
        //public Guid will be replaced by user guid later when connecting all the parts of the bankapp
        public Guid Id { get; } = Guid.NewGuid();

        public int AccountNumber { get; }
        public decimal Amount { get; }
        public decimal YearlyInterestRate { get; }
        public int TotalMonths { get; }
        public decimal TotalInterest { get; }
        public decimal TotalRepayment { get; }

        public Loan(int accountNumber, decimal amount, decimal yearlyInterestRate, int totalMonths, InterestCalculator calculator)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            YearlyInterestRate = yearlyInterestRate;
            TotalMonths = totalMonths;
            TotalInterest = calculator.CalculateInterest(amount, yearlyInterestRate, totalMonths);
            TotalRepayment = Amount + TotalInterest;
        }
            




    }
}
