
namespace Grupparbete_Bankapp
{
    internal class Program
    { 
    
    }
     
        


namespace Grupparbete_Bankapp
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                // 1. Skapa banken
                Bank bank = new Bank("CHAS BANK");
            Console.WriteLine("Hello, World!");


            //Test SystemAccount
            SystemAccount systemAccount = new SystemAccount();
            systemAccount.CreateAccount();

            // Skapa banken
            Bank bank = new Bank("Gruppens Bank");

                // 2. Skapa menyn och skicka in banken
                BankMeny meny = new BankMeny(bank);

                // 3. Starta menyn
                meny.Start();
            }
        }
    }

}
    

