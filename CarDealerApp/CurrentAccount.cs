public class CurrentAccount
{
    public decimal Balance { get; private set; }

    public void AddFunds(decimal amount) => Balance += amount;
    public bool DeductFunds(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
}