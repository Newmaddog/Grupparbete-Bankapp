using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Here I start the program.
            Start();
        }

        private static void Start()
        {
            Bank bank = new Bank("CHAS BANK");
            TransactionInput input = new TransactionInput(bank.Transactions);
            SystemAccount systemAccount = new SystemAccount(input);
            bool running = true;

            // Here I show the logo before anyone logs in.
            BankMeny.ShowLogo();

            // Logging out does not delete the users, bank accounts or money.

            while (running == true && input.EndOfInput == false)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("========== CHAS BANK ==========");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Log in");
                Console.WriteLine("0. Exit");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                string choice = input.ReadText("Choose an option: ");
                Console.ResetColor();

                switch (choice)
                {
                    case "1":
                        Account? user = systemAccount.LogInAccount();

                        if (user != null)
                        {
                            BankMeny bankMeny = new BankMeny(bank, user, systemAccount);
                            bankMeny.ShowMenu();
                        }
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose 1 or 0.");
                        Console.ResetColor();
                        break;
                }
            }


            
        }
    }
}