using System;
using System.Linq;

namespace LinqSkipMin3;

class Program
{
    static void Main()
    {
        var str = "a, 1, 2, f, -1, 0, 4, 10, 4, f, 4f, 8, 9, 3";
        Console.WriteLine("Вхідний рядок:");
        Console.WriteLine(str);
        Console.WriteLine(new string('-', 50));

        var tokens = str.Split(',')
                        .Select(s => s.Trim())
                        .ToList();
        Console.WriteLine("Токени (після Split+Trim):");
        Console.WriteLine(string.Join(" | ", tokens));
        Console.WriteLine(new string('-', 50));

        var parsedWithFlags = tokens
            .Select(s =>
            {
                bool ok = int.TryParse(s, out var v);
                return new { Token = s, Ok = ok, Value = v };
            })
            .ToList();

        Console.WriteLine("Спроба парсингу (Token -> або число, або X):");
        Console.WriteLine(string.Join(", ",
            parsedWithFlags.Select(p => p.Ok ? $"{p.Token}->{p.Value}" : $"{p.Token}->X")));

        var numbers = parsedWithFlags
            .Where(p => p.Ok)
            .Select(p => p.Value)
            .ToList();

        Console.WriteLine("Валідні числа (у початковому порядку):");
        Console.WriteLine(string.Join(", ", numbers));
        Console.WriteLine(new string('-', 50));

        var sorted = numbers.OrderBy(n => n).ToList();
        Console.WriteLine("Відсортовані (з урахуванням повторів):");
        Console.WriteLine(string.Join(", ", sorted));

        var afterSkip = sorted.Skip(3).ToList();
        Console.WriteLine("Після Skip(3):");
        Console.WriteLine(string.Join(", ", afterSkip));

        var sum = afterSkip.Sum();
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"Сума без 3 найменших елементів: {sum}");

        var compactSum =
            str.Split(',')
               .Select(s => s.Trim())
               .Select(s => int.TryParse(s, out var v) ? (int?)v : null)
               .Where(v => v.HasValue)
               .Select(v => v.Value)
               .OrderBy(v => v)
               .Skip(3)
               .Sum();

        Console.WriteLine($"(Перевірка компактним виразом) Сума: {compactSum}");
    }
}