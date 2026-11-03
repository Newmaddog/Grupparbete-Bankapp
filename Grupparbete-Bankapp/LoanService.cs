using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;

namespace Grupparbete_Bankapp
{
    internal class LoanService
    {
        private readonly InterestCalculator _interestCalculator;
        private readonly List<Loan> _loans = new();
        public LoanService(InterestCalculator interestCalculator)
        {
            _interestCalculator = interestCalculator;
        }

        //method for checking if you can load an amount 5x of your current balance
        public bool CanLoan(decimal moneyInBank, decimal currentLoans, decimal newLoan)
        {

            if (moneyInBank < 0 || currentLoans < 0)
            {
                Console.WriteLine("Invalid account or loan balance");
                return false;
            }

            if (newLoan <= 0)
            {
                Console.WriteLine("The loan amount must be greater than zero.");
                return false;
            }
            if(currentLoans + newLoan > moneyInBank * 5)
            {
                Console.WriteLine("Loan denied, you have exceeded your maximum loan limit.");
                return false;
            }

            return true;

        }

        //counts up existing loans the account has.
        public decimal GetCurrentLoans(int accountNumber)
        {
            decimal total = 0;

            foreach(Loan loan in _loans)
            {
                if(loan.AccountNumber == accountNumber)
                {
                    total += loan.Amount;
                }
            }
            return total;
        }

        //stores the loaning details and creates the loan.
        public Loan? ApplyForLoan(BankAccount account, decimal amount, decimal yearlyInterestRate, int months)
        {
            decimal currentLoans = GetCurrentLoans(account.Number);
            if (!CanLoan(account.Balance, currentLoans, amount))
            {
                return null;
            }

            var loan = new Loan(account.Number, amount, yearlyInterestRate, months, _interestCalculator);
            if (!account.Deposit(amount))
            {
                return null;
            }
            _loans.Add(loan);

            return loan;
                
        }

       

        //method that retrieves the total loans
        public List<Loan> GetLoans(int accountNumber)
        {
            List<Loan> accountLoans = new List<Loan>();

            foreach(Loan loan in _loans)
            {
                if(loan.AccountNumber == accountNumber)
                {
                    accountLoans.Add(loan);
                }
            }
            return accountLoans;
        }

        //method for a user requesting a loan.
        public void RequestLoan(BankAccount account)
        {
            Console.Write("How much would you like to borrow? ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal requestedLoan))
            {
                Console.WriteLine("Invalid loan amount");
                return;
            }

            Console.WriteLine("Enter yearly interest rate(in %): ");
            if(!decimal.TryParse(Console.ReadLine(), out decimal interestRate) || interestRate <= 0 || interestRate > 100)
            {
                Console.WriteLine("Invalid interest, it can only be from 1-100");
                return;
            }

            Console.WriteLine("Enter the loan duration(in months): ");
            if(!int.TryParse(Console.ReadLine(), out int months) || months < 1 || months > 60)
            {
                Console.WriteLine("invalid loan duration, loan duration can only be from 1-60 months.");
                return;
            }

            Loan? loan = ApplyForLoan(account, requestedLoan, interestRate, months);
            if (loan == null)
            {
                return;
            }

            
            Console.WriteLine("Loan approved!");
            Console.WriteLine($"ID: {loan.Id}");
            Console.WriteLine($"Account: {loan.AccountNumber}");
            Console.WriteLine($"Loan amount: {loan.Amount:N2}");
            Console.WriteLine($"Loan duration in months: {loan.TotalMonths}");
            Console.WriteLine($"Interest rate: {loan.YearlyInterestRate}%");
            Console.WriteLine($"Total repayment: {loan.TotalRepayment:N2}");
            Console.WriteLine($"Total interest: {loan.TotalInterest:N2}");

        }
    }
}
