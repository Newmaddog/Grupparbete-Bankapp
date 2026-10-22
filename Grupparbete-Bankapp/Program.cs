
using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapa systemkontot
            SystemAccount systemAccount = new SystemAccount();
            systemAccount.CreateAccount();

            // Skapa banken
            Bank bank = new Bank("CHAS BANK");

            // Skapa menyn och skicka in banken
            BankMeny meny = new BankMeny(bank);

            // Starta menyn
            meny.Start();
        }
    }
}
