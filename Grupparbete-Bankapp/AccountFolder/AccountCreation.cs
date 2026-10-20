using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class AccountCreation
    {
        public Account CreateAccount(List<Account> accountList)
        {
            Console.WriteLine("Creating new Account");
            string userName = "";
            string passWord = "";
            string fullName = "";
            AccountType accountType;
            userName = SetUserName(accountList);
            passWord = SetPassword();
            fullName = SetFullName();
            accountType = SetAccountType();

            Console.WriteLine($"UserName: {userName}\nPassword: {passWord}\nFullName: {fullName}\nAccountType: {accountType}");

            Console.ReadLine();

            Account account = new Account();

            account.SetUserName(userName);
            account.SetPassWord(passWord);
            account.SetFullName(fullName);
            account.SetAccountType(accountType);

            return account;
        }

        private string SetUserName(List<Account> accountList)
        {
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
                    userName = "";
                }
            } while (userName == "");

            return userName;
        }

        private string SetPassword()
        {
            string passWord;
            do
            {
                Console.WriteLine("PassWord, password most be longer then 4");
                Console.Write("Password: ");
                passWord = Console.ReadLine();

                if (passWord.Length < 5)
                {
                    Console.WriteLine("short password try again");
                }

            } while (passWord.Length < 5);

            return passWord;
        }

        private string SetFullName()
        {
            string fullName = "";
            do
            {
                Console.WriteLine("Write your full name");
                Console.Write("Full name: ");
                fullName = Console.ReadLine() ?? "";
                if (fullName == "")
                {
                    Console.WriteLine("can't be empty");
                }
            } while (fullName == "");
            return fullName;
        }

        private AccountType SetAccountType()
        {
            AccountType accountType;
            Console.WriteLine("AccountType");
            bool enumSet;
            do
            {
                Console.WriteLine("1: User\n2: Admin\n3: Owner");

                enumSet = Enum.TryParse(Console.ReadLine(), out accountType)
                    && Enum.IsDefined(typeof(AccountType), accountType);

                if (!enumSet)
                {
                    Console.WriteLine("choice 1,2 or 3 please");
                }

            } while (!enumSet);

            return accountType;
        }

        //private Account ConfirmCreateAccount(string userName, string passWord,
        //   string fullName, AccountType accountType)
        //{

        //}

    }
}
