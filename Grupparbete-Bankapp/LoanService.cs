using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class LoanService
    {

        public static bool CanLoan(decimal moneyInBank, decimal currentLoans)
        {
            Console.WriteLine("How much money do you want to loan(maximum x5 of your current money)?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal newLoans))
            {
                Console.WriteLine("Wrong input, write a number");
                return false;
            }

            if (moneyInBank < 0 || currentLoans < 0)
            {
                Console.WriteLine("Invalid account or loan balance");
            }

            if (newLoans <= 0)
            {
                Console.WriteLine("You can not loan a negative amount.");
                return false;
            }

            if (currentLoans + newLoans > moneyInBank * 5)
            {
                Console.WriteLine("You have exceeded the maximum loan amount");
                return false;
            }

            Console.WriteLine("Loan approved.");


            decimal maxLoan = moneyInBank * 5;
            decimal totalLoan = currentLoans + newLoans;
            return totalLoan <= maxLoan;
         

        }



    }
}
