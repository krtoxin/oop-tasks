using System;

public abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Species { get; protected set; }

    public Animal(string name, int age, string species)
    {
        Name = name;
        Age = age;
        Species = species;
    }

    public abstract void Sound();
    public abstract void Walk();

    public virtual void Info()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Species: {Species}");
    }
}