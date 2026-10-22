using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    // Here I show the transaction menu for the user who is logged in.
    internal class TransactionMenu
    {
        private TransactionService service;
        private TransactionInput input;


        // Here I show the menu and handle choices for the user who is logged in until they choose to go back.
        public TransactionMenu(TransactionService service)
        {
            this.service = service;
            input = new TransactionInput(service);

        }

        public void Show(Account user)
        {
            bool running = true;

            while (running == true && input.EndOfInput == false)
            {
                service.ProcessTransactions(DateTime.Now);

                Console.WriteLine();
                Console.WriteLine($"Transaktioner för {user.GetUserName()}");
                // Here I use HH:mm to show the next processing time as hours and minutes, like 14:15.
                Console.WriteLine($"Nästa behandling: {service.GetNextProcessingTime():HH:mm}");

                Console.WriteLine("1. Visa mina konton");
                Console.WriteLine("2. Överför pengar");
                Console.WriteLine("3. Visa min historik");

                if (service.IsAdmin(user) == true)
                {
                    Console.WriteLine("4. Uppdatera valutakurser");
                }

                Console.WriteLine("0. Tillbaka");

                string choice = input.ReadText("Välj: ");
                switch (choice)
                {
                    case "1":
                        ShowAccounts(user);
                        break;

                    case "2":
                        Transfer(user);
                        break;

                    case "3":
                        ShowHistory(user);
                        break;

                    case "4":
                        UpdateRates(user);
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Fel val. Försök igen.");
                        break;
                }
            }
        }

        // Here I show the customer's own accounts, their balances and the money they can still use.

        private void ShowAccounts(Account user)
        {
            List<BankAccount> accounts = service.GetAccounts(user);

            if (accounts.Count == 0)
            {
                Console.WriteLine("Du har inga bankkonton ännu.");
            }

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine($"{account.Number} | {account.Name} | Saldo: {account.Balance} {account.Currency} | Tillgängligt: {service.GetAvailableBalance(account)}");
            }
        }


        private void Transfer(Account user)
        {
            ShowAccounts(user);

            int fromNumber = input.ReadNumber("Från kontonummer: ");
            int toNumber = input.ReadNumber("Till kontonummer: ");

            decimal amount = input.ReadDecimal("Belopp i avsändarkontots valuta: ");

            if (input.EndOfInput == true)
            {
                return;
            }

            bool added = service.Transfer(user, fromNumber, toNumber, amount);

            if (added == true)
            {
                Console.WriteLine("Överföringen väntar till nästa kvart.");
            }
            else
            {
                Console.WriteLine("Överföringen kunde inte läggas till. Kontrollera konton, kontoägare, belopp, tillgängligt saldo och dagens valutakurser.");
            }

        }

        // Here I show the customer's transfers and whether they are waiting, completed or failed.
        private void ShowHistory(Account user)
        {
            List<Transaction> history = service.GetHistory(user);
            
            if (history.Count == 0)
            {
                Console.WriteLine("Du har inga överföringar i historiken.");
            }



            foreach (Transaction transaction in history)
            {
                Console.WriteLine($"#{transaction.Id} | {transaction.FromAccount} -> {transaction.ToAccount}");

                Console.WriteLine($"{transaction.Amount} {transaction.FromCurrency} -> {transaction.ReceivedAmount} {transaction.ToCurrency} | {transaction.Status}");

                Console.WriteLine($"Skapad: {transaction.CreatedAt:yyyy-MM-dd HH:mm} | Planerad: {transaction.ExecuteAt:yyyy-MM-dd HH:mm}");
            }




        }


        // Only the group's admin role can save new rates.


        private void UpdateRates(Account user)
        {
            if (service.IsAdmin(user) == false)
            {
                Console.WriteLine("Bara admin får uppdatera valutakurser.");
                return;
            }

            decimal euro = input.ReadDecimal("Hur många SEK är 1 EUR värd? ");
            decimal dollar = input.ReadDecimal("Hur många SEK är 1 USD värd? ");

            if (input.EndOfInput == true)
            {
                return;
            }

            if (service.UpdateRates(user, euro, dollar) == true)
            {
                Console.WriteLine($"Valutakurserna är sparade för {service.GetRatesDate():yyyy-MM-dd}.");
            }
            else
            {
                Console.WriteLine("Kurserna måste vara mellan 0,0001 och 1000 SEK.");
            }



        }



    }



}





