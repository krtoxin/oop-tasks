using System;

class Program
{
    static void Main()
    {
        MenuHandler menu = new MenuHandler();
        while (true)
        {
            menu.ShowMainMenu();
            Console.WriteLine("Бажаєте продовжити? (q для виходу, будь-що — ще раз):");
            string ans = Console.ReadLine();
            if (ans == "q") break;
            Console.Clear();
        }
        Console.WriteLine("Дякуємо за користування!");
    }
}