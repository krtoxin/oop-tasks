using System;

public class Dog : Animal
{
    public string FavoriteToy { get; set; }

    public Dog(string name, int age, string favoriteToy)
        : base(name, age, "Dog")
    {
        FavoriteToy = favoriteToy;
    }

    public override void Sound()
    {
        Console.WriteLine($"{Name} (Dog): Woof");
    }

    public override void Walk()
    {
        Console.WriteLine($"{Name} (Dog): Happily runs on 4 legs.");
    }

    public override void Info()
    {
        base.Info();
        Console.WriteLine($"Favorite toy: {FavoriteToy}");
    }
}