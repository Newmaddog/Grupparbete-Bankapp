# Grupparbete-Bankapp

I detta grupparbete ska ni utveckla en bankapp enligt objektorienterade principer, där ni arbetar tillsammans från en backlog med läraren som produktägare.

## System Account - Erico

**SystemAccount.cs**

This part's job was to create new accounts and also to have a login system with security so that if login fails too many times, the system gets blocked.
It is divided into three classes. One is the SystemAccount, which holds a list with saved accounts and blocks the system after too many failed login attempts.

**AccountCreation.cs**

The second is AccountCreation, whose purpose is to create an account for the system to save.
It checks whether the account you are logged in as is an admin before proceeding. It asks for the user's username, password, full name and what type of account it is.
Then it checks whether the username already exists. If it does, it says so and asks for another username. After all the information is set, it shows it to the user along with the information and asks them to confirm to proceed.
If the user is not happy, they can change whichever part they want to change and redo it.
When done, it returns a complete Account back to the SystemAccount.

**LogInAccount.cs**

The third part is the login session. It works simply: the system asks for a username, and if that username exists, it asks for a password.
The user gets three attempts to enter the correct password.
If all three fail, the system terminates and closes the terminal.
If the password is correct, the account is returned to the SystemAccount, after which the program goes back to the menu with the user logged in and ready to continue.


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


## Robin - Loans

Features:

**Loan Application** – Users can apply for a loan by entering the desired amount and the intended duration (in months); the system then calculates the interest for the loan.

**Eligibility Check** – A method verifies whether a user is eligible to apply for a loan based on specific criteria (e.g., having a balance of 0 SEK in the account).

**Interest Calculation** (Savings Accounts and Loans) – A method that calculates interest by taking the loan amount, the annual interest rate (as a percentage), and the loan duration into account.

**Loan Limit** – A feature that ensures a user cannot borrow more than five times their total account balance; if this limit is exceeded, the program displays an error message.

**Existing Loan Overview** – Users can view a list of all their current loans, providing a clear financial overview of their accounts and any outstanding debt.

**Bank Account Integration** – Once all features are complete and the bank classes have been developed, the code is integrated into the bank’s system. This links the loan functionality to the bank account and also the transactions systems, allowing users to access and utilize the loan system whenever a bank account is created.
