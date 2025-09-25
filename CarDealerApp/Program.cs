using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var dealer1 = new CarDealer("AutoStar");
        var dealer2 = new CarDealer("MegaCars");
        dealer1.Account.AddFunds(100000);
        dealer2.Account.AddFunds(80000);

        dealer1.Inventory.AddCar(new Car("Toyota", "Camry", 2020, 25000));
        dealer1.Inventory.AddCar(new Car("Honda", "Civic", 2019, 18000));
        dealer2.Inventory.AddCar(new Car("BMW", "X5", 2021, 50000));
        dealer2.Inventory.AddCar(new Car("Ford", "Focus", 2018, 15000));

        var dealers = new List<CarDealer> { dealer1, dealer2 };

        while (true)
        {
            Console.WriteLine("\nОберіть роль:");
            Console.WriteLine("1. Адміністратор");
            Console.WriteLine("2. Користувач");
            Console.WriteLine("3. Вийти");
            var role = Console.ReadLine();

            if (role == "3")
                break;

            var currentDealer = SelectDealer(dealers);
            if (currentDealer == null)
                continue;

            if (role == "1")
            {
                while (true)
                {
                    Console.WriteLine($"\n[Адміністратор: {currentDealer.Name}]");
                    Console.WriteLine("1. Додати авто");
                    Console.WriteLine("2. Переглянути авто");
                    Console.WriteLine("3. Встановити націнку");
                    Console.WriteLine("4. Баланс");
                    Console.WriteLine("5. Операції між автосалонами");
                    Console.WriteLine("6. Назад");
                    var choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Console.Write("Виробник: ");
                        var man = Console.ReadLine() ?? "";
                        Console.Write("Модель: ");
                        var mod = Console.ReadLine() ?? "";
                        Console.Write("Рік: ");
                        if (!int.TryParse(Console.ReadLine(), out int year))
                        {
                            Console.WriteLine("Некоректний рік.");
                            continue;
                        }
                        Console.Write("Ціна: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.WriteLine("Некоректна ціна.");
                            continue;
                        }
                        currentDealer.Inventory.AddCar(new Car(man, mod, year, price));
                        Console.WriteLine("Авто додано.");
                    }
                    else if (choice == "2")
                    {
                        foreach (var car in currentDealer.Inventory.GetAllCars())
                            Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Year} - {currentDealer.GetUserPrice(car)}");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введіть нову націнку (наприклад, 0.15 для 15%): ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal markup))
                        {
                            currentDealer.UserMarkup = markup;
                            Console.WriteLine("Націнку змінено.");
                        }
                        else
                        {
                            Console.WriteLine("Некоректне значення.");
                        }
                    }
                    else if (choice == "4")
                    {
                        Console.WriteLine($"Баланс: {currentDealer.Account.Balance}");
                    }
                    else if (choice == "5")
                    {
                        while (true)
                        {
                            Console.WriteLine("\n[Операції між автосалонами]");
                            Console.WriteLine("1. Купити авто у іншого автосалону");
                            Console.WriteLine("2. Обміняти авто з іншим автосалоном");
                            Console.WriteLine("3. Назад");
                            var op = Console.ReadLine();
                            if (op == "1")
                            {
                                var otherDealer = SelectOtherDealer(dealers, currentDealer);
                                if (otherDealer == null) continue;

                                var car = SelectCar(otherDealer);
                                if (car == null) continue;

                                Console.Write($"Вкажіть ціну для купівлі (базова: {car.BasePrice}): ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                                {
                                    Console.WriteLine("Некоректна ціна.");
                                    continue;
                                }

                                if (currentDealer.BuyCarFromDealer(car, otherDealer, price))
                                    Console.WriteLine("Успішно куплено.");
                                else
                                    Console.WriteLine("Операція не виконана (можливо, недостатньо коштів).");
                            }
                            else if (op == "2")
                            {
                                var otherDealer = SelectOtherDealer(dealers, currentDealer);
                                if (otherDealer == null) continue;

                                Console.WriteLine("Ваші авто:");
                                var myCar = SelectCar(currentDealer);
                                if (myCar == null) continue;

                                Console.WriteLine("Авто іншого автосалону:");
                                var theirCar = SelectCar(otherDealer);
                                if (theirCar == null) continue;

                                if (currentDealer.ExchangeCarWithDealer(myCar, otherDealer, theirCar))
                                    Console.WriteLine("Обмін виконано.");
                                else
                                    Console.WriteLine("Обмін не виконано (можливо, недостатньо коштів).");
                            }
                            else if (op == "3")
                                break;
                        }
                    }
                    else if (choice == "6")
                        break;
                }
            }
            else if (role == "2")
            {
                while (true)
                {
                    Console.WriteLine($"\n[Користувач: {currentDealer.Name}]");
                    Console.WriteLine("1. Переглянути авто");
                    Console.WriteLine("2. Пошук авто");
                    Console.WriteLine("3. Назад");
                    var choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        foreach (var car in currentDealer.Inventory.GetAllCars())
                            Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Year} - {currentDealer.GetUserPrice(car)}");
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Виробник: ");
                        var man = Console.ReadLine() ?? "";
                        Console.Write("Модель: ");
                        var mod = Console.ReadLine() ?? "";
                        var found = currentDealer.SearchCars(man, mod);
                        if (found.Count == 0)
                            Console.WriteLine("Авто не знайдено.");
                        else
                            foreach (var car in found)
                                Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Year} - {currentDealer.GetUserPrice(car)}");
                    }
                    else if (choice == "3")
                        break;
                }
            }
        }
    }

    static CarDealer? SelectDealer(List<CarDealer> dealers)
    {
        Console.WriteLine("Оберіть автосалон:");
        for (int i = 0; i < dealers.Count; i++)
            Console.WriteLine($"{i + 1}. {dealers[i].Name}");
        Console.WriteLine($"{dealers.Count + 1}. Назад");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > dealers.Count + 1)
        {
            Console.WriteLine("Некоректний вибір.");
            return null;
        }
        if (idx == dealers.Count + 1)
            return null;
        return dealers[idx - 1];
    }

    static CarDealer? SelectOtherDealer(List<CarDealer> dealers, CarDealer current)
    {
        Console.WriteLine("Оберіть інший автосалон:");
        var others = new List<CarDealer>();
        for (int i = 0, j = 1; i < dealers.Count; i++)
        {
            if (dealers[i] != current)
            {
                Console.WriteLine($"{j}. {dealers[i].Name}");
                others.Add(dealers[i]);
                j++;
            }
        }
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > others.Count)
        {
            Console.WriteLine("Некоректний вибір.");
            return null;
        }
        return others[idx - 1];
    }

    static Car? SelectCar(CarDealer dealer)
    {
        var cars = dealer.Inventory.GetAllCars();
        if (cars.Count == 0)
        {
            Console.WriteLine("Немає авто.");
            return null;
        }
        for (int i = 0; i < cars.Count; i++)
            Console.WriteLine($"{i + 1}. {cars[i].Manufacturer} {cars[i].Model} {cars[i].Year} - {cars[i].BasePrice}");
        if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > cars.Count)
        {
            Console.WriteLine("Некоректний вибір.");
            return null;
        }
        return cars[idx - 1];
    }
}