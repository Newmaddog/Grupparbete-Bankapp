using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    internal class SystemAccount
    {
        private List<Account> accountList = new List<Account>();
        AccountCreation accountCreation = new AccountCreation();

        public void CreateAccount()
        {
            Account account = accountCreation.CreateAccount(accountList);
        }
    }
}
