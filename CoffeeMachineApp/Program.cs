using System;

namespace CoffeeMachineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICoffeeMachine coffeeMachine = new CoffeeMachine(300, 100);

            while (true)
            {
                Console.WriteLine("\nМеню кавової машини:");
                Console.WriteLine("1. Приготувати еспресо");
                Console.WriteLine("2. Приготувати лате");
                Console.WriteLine("3. Додати воду");
                Console.WriteLine("4. Додати кавові зерна");
                Console.WriteLine("5. Переглянути стан машини");
                Console.WriteLine("6. Вийти");
                Console.Write("Виберіть опцію: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        coffeeMachine.MakeEspresso();
                        break;
                    case "2":
                        coffeeMachine.MakeLatte();
                        break;
                    case "3":
                        Console.Write("Скільки мл додати? ");
                        if (int.TryParse(Console.ReadLine(), out int water))
                            coffeeMachine.AddWater(water);
                        else
                            Console.WriteLine("Некоректне значення.");
                        break;
                    case "4":
                        Console.Write("Скільки грам додати? ");
                        if (int.TryParse(Console.ReadLine(), out int beans))
                            coffeeMachine.AddBeans(beans);
                        else
                            Console.WriteLine("Некоректне значення.");
                        break;
                    case "5":
                        Console.WriteLine($"Вода: {coffeeMachine.WaterAmount} мл");
                        Console.WriteLine($"Кавові зерна: {coffeeMachine.BeansAmount} г");
                        Console.WriteLine($"Вода нагріта: {(coffeeMachine.IsWaterHeated ? "Так" : "Ні")}");
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Некоректний вибір.");
                        break;
                }
            }
        }
    }
}