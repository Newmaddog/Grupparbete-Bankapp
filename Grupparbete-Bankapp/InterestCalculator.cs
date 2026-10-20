using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class InterestCalculator
    {
        public decimal CalculateInterest(decimal amount, decimal yearlyInterestRate, int months)
        
        {
            if(amount <= 0)
            {
                throw new Exception("Loan amount must be larger than zero.");
            }

            if(yearlyInterestRate < 0 || yearlyInterestRate > 100)
            {
                throw new ArgumentOutOfRangeException("Interest has to be between 0 and 100");
            }

            if (months < 1 || months > 600)
            {
                throw new ArgumentOutOfRangeException("Time has to be between 1-600 months");
            }

            decimal years = months / 12m;

            return amount * (yearlyInterestRate / 100m) * years;
        }


    }
}
