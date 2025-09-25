public class CurrentAccount : BankAccount
{
    public decimal CreditLimit { get; private set; }

    public CurrentAccount(string accountNumber, string owner, decimal initialBalance = 0)
        : base(accountNumber, owner, initialBalance)
    {
        CreditLimit = 0;
    }

    public void SetCreditLimit(decimal limit)
    {
        if (limit >= 0) CreditLimit = limit;
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"CurrentAccount of {Owner}: Balance = {Balance:F2}, Credit Limit = {CreditLimit:F2}");
    }

    public override void Deposit(decimal amount)
    {
        if (amount > 0) Balance += amount;
    }

    public override bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance + CreditLimit)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}