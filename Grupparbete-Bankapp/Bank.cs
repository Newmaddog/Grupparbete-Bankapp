namespace Grupparbete_Bankapp
{
    public class Bank
    {
        public string Name { get; private set; }

        private int nextAccountNumber = 1001;

        // Bankens gemensamma lista som även TransactionService använder.
        internal List<BankAccount> BankAccounts { get; private set; }

        internal TransactionService Transactions { get; private set; }

        public Bank(string name)
        {
            Name = name;

            BankAccounts = new List<BankAccount>();
            Transactions = new TransactionService(BankAccounts);
        }

        public void ShowInfo()
        {
            Console.WriteLine("========================");
            Console.WriteLine($"Välkommen till {Name}!");
            Console.WriteLine("========================");
        }

        // Öppnar ett nytt konto och sparar det i bankens gemensamma lista.
        public BankAccount OpenNewAccount(
            string ownerUsername,
            string accountName,
            Currency currency)
        {
            BankAccount newAccount = new BankAccount(
                nextAccountNumber,
                ownerUsername,
                accountName,
                currency,
                0m
            );

            BankAccounts.Add(newAccount);
            nextAccountNumber++;

            return newAccount;
        }

        // Visar alla konton i banken.
        public void ShowAllAccounts()
        {
            if (BankAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton att visa.");
                return;
            }

            foreach (BankAccount account in BankAccounts)
            {
                Console.WriteLine($"Kontonummer: {account.Number}");
                Console.WriteLine($"Ägare: {account.OwnerUsername}");
                Console.WriteLine($"Kontonamn: {account.Name}");
                Console.WriteLine($"Saldo: {account.Balance} {account.Currency}");
                Console.WriteLine("----------------------");
            }
        }

        // Visar endast konton som tillhör en viss användare.
        public void ShowAccountsForUser(string username)
        {
            bool foundAccount = false;

            foreach (BankAccount account in BankAccounts)
            {
                if (account.OwnerUsername == username)
                {
                    Console.WriteLine($"Kontonummer: {account.Number}");
                    Console.WriteLine($"Kontonamn: {account.Name}");
                    Console.WriteLine($"Saldo: {account.Balance} {account.Currency}");
                    Console.WriteLine("----------------------");

                    foundAccount = true;
                }
            }

            if (!foundAccount)
            {
                Console.WriteLine("Du har inga bankkonton.");
            }
        }

        /*
        GAMMAL KOD – SPARAD FÖR JÄMFÖRELSE

        Den gamla versionen använde en separat lista som hette
        bankAccounts. Nu använder vi BankAccounts, samma lista
        som transaktionssystemet använder.

        public BankAccount OpenNewAccount()
        {
            BankAccount newAccount = new BankAccount(nextAccountNumber);

            bankAccounts.Add(newAccount);
            nextAccountNumber++;

            return newAccount;
        }

        public void ShowAllAccounts()
        {
            if (bankAccounts.Count == 0)
            {
                Console.WriteLine("Det finns inga konton att visa.");
                return;
            }

            foreach (BankAccount account in bankAccounts)
            {
                Console.WriteLine($"Kontonummer: {account.AccountNumber}");
                Console.WriteLine($"Saldo: {account.Balance} kr");
            }
        }
        */
    }
}
