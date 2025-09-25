public class GmailAuthProvider : ILoginProvider
{
    private readonly string _gmail;
    private readonly string _password;

    public GmailAuthProvider(string gmail, string password)
    {
        _gmail = gmail;
        _password = password;
    }

    public bool Validate(string login, string password)
    {
        return login == _gmail && password == _password;
    }
}