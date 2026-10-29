using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    public class Loan
    {
        //public Guid will be replaced by user guid later when connecting all the parts of the bankapp
        public Guid Id { get; } = Guid.NewGuid();
        public decimal Amount { get; }
        public decimal YearlyInterestRate { get; }
        public int TotalMonths { get; }
        public decimal TotalInterest { get; }
        public decimal TotalRepayment { get; }

        public Loan(decimal amount, decimal yearlyInterestRate, int totalMonths, InterestCalculator calculator)
        {
            Amount = amount;
            YearlyInterestRate = yearlyInterestRate;
            TotalMonths = totalMonths;
            TotalInterest = calculator.CalculateInterest(amount, yearlyInterestRate, totalMonths);
            TotalRepayment = Amount + TotalInterest;
        }
            




    }
}
