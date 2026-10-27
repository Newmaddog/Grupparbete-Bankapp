using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class Account
    {
        private string UserName;
        private string Password;
        private string FullName;
        private AccountType AccountType;
        private int failedLoginAttempts;

         List<Bank> BankAccountList = new List<Bank>();

       
        public Account(string userName,string password,string fullName,AccountType accountType)
        {
            UserName = userName;
            Password = password;
            FullName = fullName;
            AccountType = accountType;
            failedLoginAttempts = 0;
        }

        public void AddBankAccount(Bank bank)
        {
            BankAccountList.Add(bank);
        }

        public void GetBankAccountList()
        {
            foreach (Bank bank in BankAccountList)
            {
                Console.WriteLine(bank);
            }
        }

        

        public string GetUserName() => UserName;
        public void SetUserName(string  userName)
        {
            this.UserName = userName;
        }
        public string GetPassWord() => Password;
        public void SetPassWord(string passWord)
        {
            this.Password = passWord;
        }

        public string GetFullName() => FullName;
        public void SetFullName(string fullName)
        {
            this.FullName = fullName;
        }
        public AccountType GetAccountType() => AccountType;
        public void SetAccountType(AccountType accountType)
        {
            this.AccountType = accountType;
        }



        //here i count a wrong password
        public void AddFailedLoginAttempt()
        {
            failedLoginAttempts = failedLoginAttempts + 1;
        }

        public int GetFailedLoginAttempts()
        {
            return failedLoginAttempts;

        }


        //three wrong passwords lock this user 
        public bool IsLocked()
        {
            if (failedLoginAttempts >= 3)
            {
                return true;
            }
            return false;
        }

        // A correct password clears the wrong attempts.
        public void ResetFailedLoginAttempts()
        {
            failedLoginAttempts = 0;

        }

        public override string ToString()
        {

            return $"UserName: {UserName}\nFullName: {FullName}\nAccountType: {AccountType}";
        }

    }
}
