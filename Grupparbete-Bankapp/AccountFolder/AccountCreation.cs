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

            Console.WriteLine("CREATE USER");
            string userName = "";
            string passWord = "";
            string fullName = "";
            AccountType accountType;
            userName = SetUserName(accountList);
            if (input.EndOfInput == true)
            {
                return null;
            }


            passWord = SetPassword();
            if (input.EndOfInput == true)
            {
                return null;
            }

            fullName = SetFullName();

            if (input.EndOfInput == true)
            {
                return null;
            }

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
                Console.Write("Username: ");
                userName = input.ReadText("");
                if (Constants.IsNotUsableWordsUtilize(userName))
                {
                    Console.Clear();
                    Console.WriteLine("This username is reserved. Choose another username.");
                    userName = "";
                    continue;
                }
                if (accountList.Any(a => a.GetUserName() == userName))
                {
                    if (userName == "")
                    {
                        Console.WriteLine("Username cannot be empty.");
                        continue;
                    }
                    Console.WriteLine("This username is already taken. Choose another username.");
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
                Console.WriteLine("The password must contain at least 5 characters.");
                Console.Write("Password: ");
                passWord = input.ReadText("");

                if (passWord.Length < 5)
                {
                    Console.WriteLine("The password is too short. Please try again.");
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
                Console.WriteLine("Enter the user's full name.");
                Console.Write("Full name: ");
                fullName = input.ReadText("");
                if (fullName == "")
                {
                    Console.WriteLine("Full name cannot be empty.");
                }
            }  
            while (fullName == "" && input.EndOfInput == false);
            return fullName;
        }

        private AccountType SetAccountType()
        {
            AccountType accountType;
            Console.WriteLine("Select a user role:");
            bool enumSet;
            do
            {
                Console.WriteLine("1. User\n2. Admin\n3. Owner");

                enumSet = Enum.TryParse(Console.ReadLine(), out accountType)
                    && Enum.IsDefined(typeof(AccountType), accountType);

                if (!enumSet)
                {
                    Console.WriteLine("Please choose 1, 2 or 3.");
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
                Console.WriteLine("CONFIRM USER\n");
                Console.WriteLine($"Username: {userName}\nFull name: {fullName}\nRole: {accountType}\n");

                Console.WriteLine("0. Cancel");
                Console.WriteLine("1. Create user");
                Console.WriteLine("2. Change username");
                Console.WriteLine("3. Change password");
                Console.WriteLine("4. Change full name");
                Console.WriteLine("5. Change user role");
                string choice = input.ReadText("");

                // Here I stop if there is no more input.
                if (input.EndOfInput == true)
                {
                    return null;
                }

                if (!int.TryParse(choice, out menuChoice))
                {
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    input.ReadText("");
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

            } while (menuChoice != 0 && menuChoice != 1 && input.EndOfInput == false);

            if (menuChoice == 0 || input.EndOfInput == true)
            {
                return null;
            }

            Account account = new(userName, password, fullName, accountType);
            return account;
        }

    }
}
