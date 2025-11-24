using LibraryLab.Interfaces;

namespace LibraryLab.Services;

public class ConsoleEmailSender : IEmailSender
{
    public void Send(string to, string subject, string body)
    {
        Console.WriteLine($"[EMAIL to={to}] {subject}\n{body}");
    }
}
