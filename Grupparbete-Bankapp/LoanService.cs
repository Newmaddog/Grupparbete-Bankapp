using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Grupparbete_Bankapp
{
    internal class LoanService
    {
        //This will change slightly when bank account class is created so will improve upon later.
        public bool CanLoan(decimal moneyInBank, decimal currentLoans, decimal newLoans)
        {
            
            if (moneyInBank < 0 || currentLoans < 0)
            {
                Console.WriteLine("Invalid account or loan balance");
                return false;
            }

            if (newLoans <= 0)
            {
                Console.WriteLine("The loan amount must be greater than zero.");
                return false;
            }

            if (currentLoans + newLoans > moneyInBank * 5)
            {
                Console.WriteLine("You have exceeded the maximum loan amount");
                return false;
            }

            Console.WriteLine("Loan approved.");
            return true;

           

        }

        
       

    }
}
