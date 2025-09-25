using System;

class Program
{
    static void Main()
    {
        int num = 123;

        Console.WriteLine($"Кількість цифр: {num.DigitCount()}");
        Console.WriteLine($"Парне? {num.IsEven()}");
        Console.WriteLine($"Непарне? {num.IsOdd()}");

        Console.WriteLine($"Більше за 50? {num.IsGreaterThan(50)}");
        Console.WriteLine($"Менше за 200? {num.IsLessThan(200)}");
        Console.WriteLine($"Рівне 123? {num.IsEqualTo(123)}");
    }
}
