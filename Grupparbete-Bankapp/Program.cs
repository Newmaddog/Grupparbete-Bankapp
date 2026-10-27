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

            // Logging out does not delete the users, bank accounts or money.

            while (running == true && input.EndOfInput == false)
            {
                Console.WriteLine();
                Console.WriteLine("1. Log in");
                Console.WriteLine("0. Exit");
                string choice = input.ReadText("Choose: ");

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
                        Console.WriteLine("Choose 1 or 0.");
                        break;
                }
            }


            
        }
    }
}