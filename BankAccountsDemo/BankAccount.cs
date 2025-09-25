public abstract class BankAccount
{
    public string AccountNumber { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; protected set; }

    public BankAccount(string accountNumber, string owner, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
    }

    public abstract void DisplayBalance();
    public abstract void Deposit(decimal amount);
    public abstract bool Withdraw(decimal amount);
}