using System.Collections.Generic;

public interface IDigitalWallet
{
    IReadOnlyList<string> GetTransactionLog();
    decimal CheckBalance();
    bool Withdraw(decimal amount);
    void Deposit(decimal amount);
    void SetAuthProvider(ILoginProvider authProvider);
}