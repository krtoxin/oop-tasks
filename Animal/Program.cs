using System;

class Program
{
    static void Main()
    {
        Animal[] animals = {
            new Snake("Slytherin", 2, "Green"),
            new Cat("Murchyk", 3, "Siamese"),
            new Dog("Barbos", 5, "Ball")
        };

        foreach (var animal in animals)
        {
            animal.Info();
            animal.Sound();
            animal.Walk();
            Console.WriteLine(new string('-', 30));
        }
    }
}