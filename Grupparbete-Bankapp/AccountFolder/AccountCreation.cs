using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class AccountCreation
    {

        private TransactionInput input;

        public AccountCreation(TransactionInput input)
        {
            this.input = input;
        }

        public Account? CreateAccount(List<Account> accountList)
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

            if (input.EndOfInput == true)
            {
                return null;
            }

            Account? account = ConfirmCreateAccount(userName, passWord, fullName, accountType, accountList);
            return account;
        }

        private string SetUserName(List<Account> accountList)
        {
            string userName = "";

            do
            {
                //not sure yet if i will use it
                //Constants.WriteExitText();
                Console.Write("UserName: ");
                userName = input.ReadText("");
                if (Constants.IsNotUsableWordsUtilize(userName))
                {
                    Console.Clear();
                    Console.WriteLine($"You can't have {userName} as a UserName. please try again");
                    userName = "";
                    continue;
                }
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
            } 
            
            while (userName == "" && input.EndOfInput == false);

            return userName;
        }

        private string SetPassword()
        {
            string passWord;
            do
            {
                Console.WriteLine("PassWord, password most be longer then 4");
                Console.Write("Password: ");
                passWord = Console.ReadLine() ?? "";

                if (passWord.Length < 5)
                {
                    Console.WriteLine("short password try again");
                }

            } 
            
            while (passWord.Length < 5 && input.EndOfInput == false);

            return passWord;
        }

        private string SetFullName()
        {
            string fullName = "";
            do
            {
                Console.WriteLine("Write your full name");
                Console.Write("Full name: ");
                fullName = input.ReadText("");
                if (fullName == "")
                {
                    Console.WriteLine("can't be empty");
                }
            }  
            while (fullName == "" && input.EndOfInput == false);
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

            }  while (enumSet == false && input.EndOfInput == false);

            return accountType;
        }

        private Account? ConfirmCreateAccount(string userName, string password,
           string fullName, AccountType accountType, List<Account> accountList)
        {
            int menuChoice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine($"Confirmation process\n");
                Console.WriteLine($"UserName: {userName}\nFullName: {fullName}\nAccountType: {accountType}\n");
                Console.WriteLine($"press 0 to cancel\npress 1 to confirm Account \n2: change UserName\n3: change password\n4: change fullname\n5 change AccountType");

                if (!int.TryParse(Console.ReadLine(), out menuChoice))
                {
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadLine();
                    Console.Clear();
                    menuChoice = -1;
                    continue;
                }

                switch (menuChoice)
                {
                    case 2:
                        userName = SetUserName(accountList);
                        break;
                    case 3:
                        password = SetPassword();
                        break;
                    case 4:
                        fullName = SetFullName();
                        break;
                    case 5:
                        accountType = SetAccountType();
                        break;
                }

            } while (menuChoice is not (0 or 1));

            if (menuChoice == 0)
            {
                return null;
            }

            Account account = new(userName, password, fullName, accountType);
            return account;
        }

    }
}
