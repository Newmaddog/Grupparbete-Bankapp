using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class SystemAccount
    {
        private List<Account> accountList = new List<Account>();


        public void CreateAccount()
        {
            Console.WriteLine("Creating new Account");
            string userName = "";
            do
            {
                Console.Write("UserName: ");
                userName = Console.ReadLine() ?? "";
                if (accountList.Any(a => a.GetUserName() == userName))
                {
                    if (userName == "")
                    {
                        Console.WriteLine("UserName Can't be empty");
                        continue;
                    }
                    Console.WriteLine($"an Account with userName: {userName} exist alread!!!");
                }
            } while (userName == "");
        }

    }
}
