public class Privat24AuthProvider : ILoginProvider
{
    private readonly string _phone;
    private readonly string _passwordHash;

    public Privat24AuthProvider(string phone, string password)
    {
        _phone = phone;
        _passwordHash = Hasher.Hash(password);
    }

    public bool Validate(string login, string password)
    {
        var inputHash = Hasher.Hash(password);
        return login == _phone && inputHash == _passwordHash;
    }
}