public class Privat24AuthProvider : ILoginProvider
{
    private readonly string _phone;
    private readonly string _password;

    public Privat24AuthProvider(string phone, string password)
    {
        _phone = phone;
        _password = password;
    }

    public bool Validate(string login, string password)
    {
        return login == _phone && password == _password;
    }
}