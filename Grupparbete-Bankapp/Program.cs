
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

                // 2. Skapa menyn och skicka in banken
                BankMeny meny = new BankMeny(bank);

                // 3. Starta menyn
                meny.Start();
            }
        }
    }

}
    

