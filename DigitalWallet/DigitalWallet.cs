using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class DigitalWallet : IDigitalWallet
{
    private decimal _balance;
    private readonly string _login;
    private readonly string _passwordHash;
    private readonly List<string> _transactions = new();
    private ILoginProvider? _authProvider;

    public DigitalWallet(string login, string password, decimal initialBalance)
    {
        _login = login;
        _passwordHash = Hash(password);
        _balance = initialBalance;
        _transactions.Add($"Wallet created with balance: {initialBalance}");
    }

    public void SetAuthProvider(ILoginProvider authProvider)
    {
        _authProvider = authProvider;
    }

    private void EnsureAuthenticated()
    {
        if (_authProvider == null || !_authProvider.Validate(_login, _passwordHash))
            throw new UnauthorizedAccessException("Invalid credentials");
    }

    public List<string> GetTransactionLog()
    {
        EnsureAuthenticated();
        return new List<string>(_transactions);
    }

    public decimal CheckBalance()
    {
        EnsureAuthenticated();
        return _balance;
    }

    public bool Withdraw(decimal amount)
    {
        EnsureAuthenticated();
        if (amount <= 0 || amount > _balance)
            return false;
        _balance -= amount;
        _transactions.Add($"Withdraw: {amount}, Balance: {_balance}");
        return true;
    }

    public void Deposit(decimal amount)
    {
        EnsureAuthenticated();
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");
        _balance += amount;
        _transactions.Add($"Deposit: {amount}, Balance: {_balance}");
    }

     public static string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}