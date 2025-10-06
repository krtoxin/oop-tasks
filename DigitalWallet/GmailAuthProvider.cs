public class GmailAuthProvider : ILoginProvider
{
    private readonly string _gmail;
    private readonly string _passwordHash;

    public GmailAuthProvider(string gmail, string password)
    {
        _gmail = gmail;
        _passwordHash = Hasher.Hash(password);
    }

    public bool Validate(string login, string password)
    {
        var inputHash = Hasher.Hash(password);
        return login == _gmail && inputHash == _passwordHash;
    }
}