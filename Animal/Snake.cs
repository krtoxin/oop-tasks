using System;

public class Snake : Animal
{
    public string Color { get; set; }

    public Snake(string name, int age, string color)
        : base(name, age, "Snake")
    {
        Color = color;
    }

    public override void Sound()
    {
        Console.WriteLine($"{Name} (Snake): Hissss");
    }

    public override void Walk()
    {
        Console.WriteLine($"{Name} (Snake): I slither instead of walking.");
    }

    public override void Info()
    {
        base.Info();
        Console.WriteLine($"Color: {Color}");
    }
}