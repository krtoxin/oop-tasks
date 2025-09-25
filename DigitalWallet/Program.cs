using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Створення гаманця:");
        Console.Write("Логін (email або телефон): ");
        var login = Console.ReadLine() ?? "";
        Console.Write("Пароль: ");
        var password = Console.ReadLine() ?? "";
        Console.Write("Початковий баланс: ");
        decimal.TryParse(Console.ReadLine(), out decimal balance);

        var wallet = new DigitalWallet(login, password, balance);

        Console.WriteLine("Оберіть спосіб авторизації:");
        Console.WriteLine("1. Gmail");
        Console.WriteLine("2. Privat24");
        var authType = Console.ReadLine();

        if (authType == "1")
        {
            wallet.SetAuthProvider(new GmailAuthProvider(login, DigitalWallet.Hash(password)));
        }
        else
        {
            wallet.SetAuthProvider(new Privat24AuthProvider(login, DigitalWallet.Hash(password)));
        }

        while (true)
        {
            Console.WriteLine("\n1. Баланс\n2. Поповнити\n3. Зняти\n4. Транзакції\n5. Вийти");
            var choice = Console.ReadLine();
            try
            {
                if (choice == "1")
                    Console.WriteLine($"Баланс: {wallet.CheckBalance()}");
                else if (choice == "2")
                {
                    Console.Write("Сума: ");
                    decimal.TryParse(Console.ReadLine(), out decimal sum);
                    wallet.Deposit(sum);
                    Console.WriteLine("Поповнено.");
                }
                else if (choice == "3")
                {
                    Console.Write("Сума: ");
                    decimal.TryParse(Console.ReadLine(), out decimal sum);
                    if (wallet.Withdraw(sum))
                        Console.WriteLine("Знято.");
                    else
                        Console.WriteLine("Недостатньо коштів.");
                }
                else if (choice == "4")
                {
                    foreach (var t in wallet.GetTransactionLog())
                        Console.WriteLine(t);
                }
                else if (choice == "5")
                    break;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                break;
            }
        }
    }
}