public class DepositAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public DepositAccount(string accountNumber, string owner, decimal interestRate, decimal initialBalance = 0)
        : base(accountNumber, owner, initialBalance)
    {
        InterestRate = interestRate;
    }

    public void ApplyInterest()
    {
        decimal interest = Balance * InterestRate / 100;
        Balance += interest;
        Console.WriteLine($"Interest applied: {interest:F2}");
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"DepositAccount of {Owner}: Balance = {Balance:F2}");
    }

    public override void Deposit(decimal amount)
    {
        if (amount > 0) Balance += amount;
    }

    public override bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}