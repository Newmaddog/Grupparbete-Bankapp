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

        // Here I keep the bank running while users log in and out.
        private static void Start()
        {
            Bank bank = new Bank("CHAS BANK");
            TransactionInput input = new TransactionInput(bank.Transactions);
            SystemAccount systemAccount = new SystemAccount(input);

            bool running = true;

            // Logging out does not delete the bank accounts or money.
            while (running == true && input.EndOfInput == false)
            {
                BankMeny.ShowLogo();

                Console.WriteLine("1. Log in");
                Console.WriteLine("0. Exit");
                Console.WriteLine();

                string choice = input.ReadText("Choose an option: ");

                switch (choice)
                {
                    case "1":
                        Account? user = systemAccount.LogInAccount();

                        if (user != null)
                        {
                            BankMeny bankMeny = new BankMeny(bank, user, systemAccount);
                            bankMeny.ShowMenu();

                            systemAccount.LogOut();
                        }
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        input.ShowError("Please choose 1 or 0.");
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("Thank you for using CHAS BANK.");



        }
    }
}