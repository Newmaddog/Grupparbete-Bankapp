
using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Skapa systemkontot
            SystemAccount systemAccount = new SystemAccount();
            Bank bank = new Bank("CHAS BANK");
            BankMeny bankMeny = new BankMeny(bank);
            Account? ActiveAccount;

            int choice = -1;
            while (true)
            {


                Console.WriteLine("choice!!");
                Console.WriteLine("1. Show Account List\n2. Log in\n3. Create Account\n4. Bank Meny");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        systemAccount.AccountList();
                        break;

                    case 2:
                        ActiveAccount = systemAccount.LogInAccount();
                        break;
                    case 3:
                        systemAccount.CreateAccountAsAdmin();
                        break;

                    case 4:

                        if (ActiveAccount is null)
                        {
                            Console.WriteLine("Log in first");
                            continue;
                        }
                        // Skapa menyn och skicka in banken
                        bankMeny.ShowMenu(ActiveAccount);
                        break; 
                    default:
                        break;
                }





                if (systemAccount.SystemBlockStatus())
                {
                    Console.WriteLine("Error to many attempt system block!!!");
                    System.Environment.Exit(1);
                }

                // Skapa banken

               


                // Starta menyn

            }





        }
    }
}
