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
            wallet.SetAuthProvider(new GmailAuthProvider(login, password));
        }
        else
        {
            wallet.SetAuthProvider(new Privat24AuthProvider(login, password));
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
                    var input = Console.ReadLine();
                    if (decimal.TryParse(input, out decimal sum) && sum > 0)
                    {
                        wallet.Deposit(sum);
                        Console.WriteLine($"Поповнено на {sum}.");
                    }
                    else
                    {
                        Console.WriteLine("Некоректна сума. Поповнення не виконано.");
                    }
                }
                else if (choice == "3")
                {
                    Console.Write("Сума: ");
                    var input = Console.ReadLine();
                    if (decimal.TryParse(input, out decimal sum) && sum > 0)
                    {
                        if (wallet.Withdraw(sum))
                            Console.WriteLine("Знято.");
                        else
                            Console.WriteLine("Недостатньо коштів.");
                    }
                    else
                    {
                        Console.WriteLine("Некоректна сума. Зняття не виконано.");
                    }
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