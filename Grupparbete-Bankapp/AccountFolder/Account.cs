using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class Account
    {
        private string UserName;
        private string Password;
        private string FullName;
        private AccountType AccountType; 

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
        public Enum GetAccountType() => AccountType;
        public void SetAccountType(AccountType accountType)
        {
            this.AccountType = accountType;
        }

    }
}
