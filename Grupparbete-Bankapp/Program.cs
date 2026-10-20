namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            

            Bank bank = new Bank("Gruppens Bank");
            BankSystem system = new BankSystem(bank);

            system.Start();

        }
    }
}
