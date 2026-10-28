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

        // Here I collect the details and stop without saving if admin chooses 0.
        public Account? CreateAccount(List<Account> accountList)
        {

            string userName = SetUserName(accountList);

            if (userName == "0" || input.EndOfInput == true)
            {
                return null;
            }

            string passWord = SetPassword();

            if (passWord == "0" || input.EndOfInput == true)
            {
                return null;
            }

            string fullName = SetFullName();

            if (fullName == "0" || input.EndOfInput == true)
            {
                return null;
            }

            AccountType? selectedType = SetAccountType();

            // Here I stop if admin chose 0.
            if (selectedType == null)
            {
                return null;
            }

            AccountType accountType = selectedType.Value;

            return ConfirmCreateAccount(userName, passWord, fullName, accountType, accountList);
        }

        // Here I ask for a username that is filled in and not already used.
        private string SetUserName(List<Account> accountList)
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("CREATE USER - USERNAME");
                Console.WriteLine("0. Cancel");

                string userName = input.ReadText("Username: ").Trim();

                if (userName == "0" || input.EndOfInput == true)
                {
                    return "0";
                }

                bool alreadyUsed = false;

                // Here I check that no other user has this username.
                foreach (Account account in accountList)
                {
                    if (account.GetUserName() == userName)
                    {
                        alreadyUsed = true;
                    }
                }

                if (userName == "")
                {
                    input.ShowError("Username cannot be empty.");
                }
                else if (alreadyUsed == true)
                {
                    input.ShowError("This username is already in use.");
                }
                else
                {
                    return userName;
                }
            }

            return "0";
        }

        // Here I ask for a password with at least five characters.
        private string SetPassword()
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("CREATE USER - PASSWORD");
                Console.WriteLine("Use at least 5 characters.");
                Console.WriteLine("0. Cancel");

                string password = input.ReadText("Password: ");

                if (password == "0" || input.EndOfInput == true)
                {
                    return "0";
                }

                if (password.Length >= 5)
                {
                    return password;
                }

                input.ShowError("The password is too short. Please try again.");
            }

            return "0";
        }

        // Here I ask for the full name and let 0 cancel.
        private string SetFullName()
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("CREATE USER - FULL NAME");
                Console.WriteLine("0. Cancel");

                string fullName = input.ReadText("Full name: ").Trim();

                if (fullName == "0" || input.EndOfInput == true)
                {
                    return "0";
                }

                if (fullName != "")
                {
                    return fullName;
                }

                input.ShowError("Full name cannot be empty.");
            }

            return "0";
        }

        // Here I turn the menu number into the user's role.
        private AccountType? SetAccountType()
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("CREATE USER - ROLE");
                Console.WriteLine("1. User");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Owner");
                Console.WriteLine("0. Cancel");

                string choice = input.ReadText("Choose an option: ");

                if (input.EndOfInput == true)
                {
                    return null;
                }

                switch (choice)
                {
                    case "1":
                        return AccountType.User;

                    case "2":
                        return AccountType.Admin;

                    case "3":
                        return AccountType.Owner;

                    case "0":
                        return null;

                    default:
                        input.ShowError("Please choose 1, 2, 3 or 0.");
                        break;
                }
            }

            return null;
        }

        // Here admin can check or change the details before creating the user.
        private Account? ConfirmCreateAccount(string userName,string password,string fullName,AccountType accountType,List<Account> accountList)
        {
            while (input.EndOfInput == false)
            {
                input.ShowScreen("CREATE USER - CONFIRM");

                Console.WriteLine($"Username: {userName}");
                Console.WriteLine($"Full name: {fullName}");
                Console.WriteLine($"Role: {accountType}");
                Console.WriteLine();

                Console.WriteLine("1. Create user");
                Console.WriteLine("2. Change username");
                Console.WriteLine("3. Change password");
                Console.WriteLine("4. Change full name");
                Console.WriteLine("5. Change user role");
                Console.WriteLine("0. Cancel");

                string choice = input.ReadText("Choose an option: ");

                if (input.EndOfInput == true)
                {
                    return null;
                }

                switch (choice)
                {
                    case "1":
                        return new Account(userName, password, fullName, accountType);

                    case "2":
                        userName = SetUserName(accountList);

                        if (userName == "0")
                        {
                            return null;
                        }
                        break;

                    case "3":
                        password = SetPassword();

                        if (password == "0")
                        {
                            return null;
                        }
                        break;

                    case "4":
                        fullName = SetFullName();

                        if (fullName == "0")
                        {
                            return null;
                        }
                        break;

                    case "5":
                        AccountType? selectedType = SetAccountType();

                        if (selectedType == null)
                        {
                            return null;
                        }

                        accountType = selectedType.Value;
                        break;

                    case "0":
                        return null;

                    default:
                        input.ShowError("Please choose one of the listed numbers.");
                        break;
                }
            }

            return null;
        }

    }
}
