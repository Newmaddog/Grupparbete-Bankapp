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


---

## Transactions - Johan

This part lets customers transfer money between their own accounts or send money to another customer. It also handles deposits, currency exchange and transaction history.

### Files

**Transaction.cs**

Stores one deposit or transfer. It saves the username, account numbers, amounts, currencies, times and status.

`IsDeposit` tells the code whether it is a deposit. `CreatedAt` is when the payment was entered. `ExecuteAt` is when it is planned to run.

**TransactionStatus.cs**

Contains the three possible statuses:

- **Pending:** The payment is waiting.
- **Completed:** The payment has gone through.
- **Failed:** The payment could not go through.

**TransactionService.cs**

Checks accounts and amounts. It handles exchange rates, adds payments to the queue and processes them when they are due. It also finds the customer's transaction history.

**TransactionInput.cs**

Reads text and numbers while checking waiting payments.

`ShowScreen()` clears the old screen and shows a heading. `WaitForBack()` keeps the result on screen until the user chooses 0.

**TransactionMenu.cs**

Shows the customer's accounts, transfers and history. Admin can also update exchange rates here.

### How the parts connect

`Bank` creates one `TransactionService` and gives it the bank account list.

`BankMeny` opens `TransactionMenu` with the logged-in user and this same service. Opening the menu again keeps the same accounts and waiting payments.

Each bank account saves `OwnerUsername`. The service uses this to check who owns the account. Two users can have the same full name without sharing accounts or history.

### Checking a payment

`Transfer()` checks that the sender owns the account and has enough available money.

`Deposit()` checks that the customer owns the account receiving the deposit.

Both methods check that the amount:

- Is more than 0.
- Is no more than 10000.
- Has no more than two decimal places.

### Waiting payments and available money

Accepted payments wait for the next quarter-hour: **:00, :15, :30 or :45**.

`ProcessTransactions()` processes the waiting payments. The balances change when the payment goes through.

`GetAvailableBalance()` subtracts outgoing amounts already waiting in the queue.

For example, if an account has **1000 SEK** and a waiting transfer of **300 SEK**, only **700 SEK** is available. This stops the same money being used for several waiting transfers.

### Transaction history

The queue holds waiting payments. A separate list keeps the history.

Both use the same `Transaction` object. When its status changes, the history shows the new status too.

`GetHistory()` returns deposits and transfers involving the customer's accounts.

### Currency exchange

Accounts can use **SEK, EUR or USD**. The amount is entered in the sender's currency.

Admin enters the SEK value of **1 EUR** and **1 USD** for the current day. Transfers between different currencies need today's saved rates.

The service converts through SEK and rounds the result to two decimal places. The receiving amount is saved when the transfer is added to the queue.

### Using the menu

Choose **0** to go back.

During a transfer or exchange-rate update, **0 cancels without saving**.

Results stay on screen until the user chooses 0. The menu then clears the old screen.

---

## Bank - Mustaf

This part handles the bank and its bank accounts.

### Files

**Bank.cs**

The `Bank` class keeps track of all bank accounts.

It stores the bank's name and has a list of all bank accounts.

It also gives each new account a unique account number, starting from **1001**.

**Main methods:**

- `Bank(string name)`: Creates the bank and sets its name.
- `ShowInfo()`: Shows the bank's name.
- `OpenNewAccount(...)`: Creates a new bank account with the customer's information. The starting balance is 0. The account is added to the list and given a new account number.
- `ShowAllAccounts()`: Shows all bank accounts.
- `ShowAccountsForUser(string username)`: Shows only the accounts belonging to a specific customer.

The class also creates the `TransactionService` and connects it to the bank's account list.

**BankMeny.cs**

The `BankMeny` class handles the bank menu that the customer sees after logging in.

It shows different options and lets the customer choose what to do.

**Main functions:**

- Shows the bank menu.
- Displays bank information.
- Lets the customer open a new bank account.
- Shows the customer's bank accounts.
- Opens the transaction menu.
- Lets the customer go back or exit.

When the customer chooses transactions, `BankMeny` opens `TransactionMenu` and passes the logged-in customer's username and the bank's `TransactionService`.
