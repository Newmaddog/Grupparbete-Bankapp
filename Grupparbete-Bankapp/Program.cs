
namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Skapa banken
            Bank bank = new Bank("Gruppens Bank");

            // Skapa menyn och skicka in banken
            BankMeny meny = new BankMeny(bank);

            // Starta banksystemets meny
            meny.Start();
        }
    }
}
