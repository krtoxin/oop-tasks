using System;

public class DepositAccount : IBankAccount
{
    public string AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    public decimal InterestRate { get; set; }

    public DepositAccount(string accountNumber, string owner, decimal interestRate, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        InterestRate = interestRate;
        Balance = initialBalance;
    }

    public void ApplyInterest()
    {
        decimal interest = Balance * InterestRate / 100;
        Balance += interest;
        Console.WriteLine($"Interest applied: {interest:F2}");
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"DepositAccount of {Owner}: Balance = {Balance:F2}");
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0) Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}