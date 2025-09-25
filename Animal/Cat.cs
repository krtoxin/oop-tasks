using System;

public class Cat : Animal
{
    public string Breed { get; set; }

    public Cat(string name, int age, string breed)
        : base(name, age, "Cat")
    {
        Breed = breed;
    }

    public override void Sound()
    {
        Console.WriteLine($"{Name} (Cat): Meow");
    }

    public override void Walk()
    {
        Console.WriteLine($"{Name} (Cat): Gracefully walks on 4 paws.");
    }

    public override void Info()
    {
        base.Info();
        Console.WriteLine($"Breed: {Breed}");
    }
}
