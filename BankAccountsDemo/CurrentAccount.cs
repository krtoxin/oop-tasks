using System;

public class CurrentAccount : IBankAccount
{
    public string AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    public decimal CreditLimit { get; private set; }

    public CurrentAccount(string accountNumber, string owner, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
        CreditLimit = 0;
    }

    public void SetCreditLimit(decimal limit)
    {
        if (limit >= 0) CreditLimit = limit;
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"CurrentAccount of {Owner}: Balance = {Balance:F2}, Credit Limit = {CreditLimit:F2}");
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0) Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance + CreditLimit)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}