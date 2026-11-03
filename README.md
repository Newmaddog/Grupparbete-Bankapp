# Grupparbete-Bankapp

I detta grupparbete ska ni utveckla en bankapp enligt objektorienterade principer, där ni arbetar tillsammans från en backlog med läraren som produktägare.

//Erico
public class Account
{
Variabler: String FullName, string UserName, string Password, (Enum)AccountType AcountType, List<BankAccount> BankAccountList;

Functions:

My functions mainly consist of getters and setters for the variables within the class, along with an AddBankAccount function that adds a BankAccount to the list.

I have also overridden the ToString() function to display the specific information I want when using Console.WriteLine(Account).

}

internal class AccountCreation
{
Purpose: The purpose of this class is to create an account. When the user chooses to create an account, the system calls this class and starts collecting the user's username, password, and full name. Once all the required information has been provided, the class moves on to the next function, called ConfirmCreateAccount, which checks whether all the information is correct. If everything is correct, the function returns an Account object.
Variabler: none;

Function: public Account? CreateAccount(List<Account> accountList), private string SetUserName(List<Account> accountList),
private string SetPassword(), private string SetFullName(), private AccountType SetAccountType(),
private Account? ConfirmCreateAccount(string userName, string password,string fullName, AccountType accountType,List<Account>accountList)
}
