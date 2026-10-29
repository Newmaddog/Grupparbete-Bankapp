using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class LoanMenu
    {
        private readonly LoanService _loanservice;
        public LoanMenu(LoanService loanService)
        {
            _loanservice = loanService;
        }

        public void ShowLoanMenu(BankAccount account, decimal currentLoans)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("====LOAN MENU====");
                Console.WriteLine("1. Apply for loan.");
                Console.WriteLine("2. View my loans.");
                Console.WriteLine("3. Back to main menu.");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": _loanservice.RequestLoan(account, currentLoans);
                        break;

                    case "2": ShowLoans(account);
                        break;

                    case "3": running = false;
                        break;

                    default: Console.WriteLine("Invalid option.");
                        break;

                }
            }
        }
        private void ShowLoans(BankAccount account)
        {
            var loans = _loanservice.GetLoans(account);
            Console.WriteLine("====MY LOANS====");

            if(loans.Count == 0)
            {
                Console.WriteLine("No loans registered.");
                return;
            }
            foreach(var loan in loans)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"Loan amount: {loan.Amount:N2}");
                Console.WriteLine($"Loan duration in months: {loan.TotalMonths}");
                Console.WriteLine($"Interest rate: {loan.YearlyInterestRate}%");
                Console.WriteLine($"Total repayment: {loan.TotalRepayment:N2}");
                Console.WriteLine($"Total interest: {loan.TotalInterest:N2}");
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
