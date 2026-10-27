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

         List<Bank> BankAccountList = new List<Bank>();

       
        public Account(string userName,string password,string fullName,AccountType accountType)
        {
            UserName = userName;
            Password = password;
            FullName = fullName;
            AccountType = accountType;
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


        public override string ToString()
        {

            return $"UserName: {UserName}\nPassword: {Password}\nFullName: {FullName}\nAccountType: {AccountType}";
        }

    }
}
