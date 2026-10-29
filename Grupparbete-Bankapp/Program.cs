
using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            // Skapa banken och transaktionssystemet.
            Bank bank = new Bank("CHAS BANK");
            LoanService loanService = new LoanService(new InterestCalculator());
            TransactionInput input = new TransactionInput(bank.Transactions);
            SystemAccount systemAccount = new SystemAccount();

            Account? activeAccount = null;
            bool running = true;

            // Visa bankens logotyp.
            BankMeny.ShowLogo();

            while (running && !input.EndOfInput)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========== CHAS BANK ==========");
                Console.ResetColor();

                Console.WriteLine("1. Show Account List");
                Console.WriteLine("2. Log in");
                Console.WriteLine("3. Create Account");
                Console.WriteLine("4. Bank Menu");
                Console.WriteLine("0. Exit");

                Console.ForegroundColor = ConsoleColor.Yellow;
                string choice = input.ReadText("Choose an option: ");
                Console.ResetColor();

                switch (choice)
                {
                    case "1":
                        systemAccount.AccountList();
                        break;

                    case "2":
                        activeAccount = systemAccount.LogInAccount();

                        if (systemAccount.SystemBlockStatus())
                        {
                            Console.WriteLine(
                                "Too many failed attempts. System blocked!"
                            );

                            Environment.Exit(1);
                        }
                        break;

                    case "3":
                        systemAccount.CreateAccountAsAdmin();
                        break;

                    case "4":
                        if (activeAccount == null)
                        {
                            Console.WriteLine("Log in first.");
                            break;
                        }

                        BankMeny bankMeny = new BankMeny(
                            bank,
                            activeAccount,
                            systemAccount,
                            loanService
                        );

                        bankMeny.ShowMenu(activeAccount);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose a valid option.");
                        Console.ResetColor();
                        break;
                }
            }

            Console.WriteLine("Thank you for using CHAS BANK!");
        }
    }
}
