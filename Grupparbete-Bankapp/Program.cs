
using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapa systemkontot
            SystemAccount systemAccount = new SystemAccount();
            systemAccount.AccountList();
            systemAccount.CreateAccount();
            systemAccount.LogInAccount();

            // Skapa banken
            Bank bank = new Bank("CHAS BANK");

            // Skapa menyn och skicka in banken
            BankMeny bankMeny = new BankMeny(bank);
            bankMeny.ShowMenu();


            // Starta menyn
         
        }
    }
}
