public interface IBankAccount
{
    string AccountNumber { get; }
    string Owner { get; }
    decimal Balance { get; }
    void DisplayBalance();
    void Deposit(decimal amount);
    bool Withdraw(decimal amount);
}