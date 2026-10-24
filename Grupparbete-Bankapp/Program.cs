
namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            //Test SystemAccount
            SystemAccount systemAccount = new SystemAccount();
            systemAccount.AccountList();
            systemAccount.CreateAccount();
            systemAccount.LogInAccount();

            // Skapa banken
            Bank bank = new Bank("Gruppens Bank");

            // Skapa menyn och skicka in banken
            BankMeny meny = new BankMeny(bank);

            // Starta banksystemets meny
            meny.Start();
        }
    }
}
