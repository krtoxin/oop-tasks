class Program
{
    static void Main()
    {
        DepositAccount depAcc = new DepositAccount("UA123", "Ivan Petrov", 5, 1000);
        depAcc.DisplayBalance();
        depAcc.Deposit(500);
        depAcc.ApplyInterest();
        depAcc.Withdraw(300);
        depAcc.DisplayBalance();

        Console.WriteLine();

        CurrentAccount curAcc = new CurrentAccount("UA456", "Olena Ivanova", 200);
        curAcc.DisplayBalance();
        curAcc.SetCreditLimit(300);
        curAcc.Withdraw(400);
        curAcc.Withdraw(200);
        curAcc.Deposit(500);
        curAcc.DisplayBalance();
    }
}