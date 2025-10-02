using System;

class Program
{
    static void Init(IHomeDeviceFactory factory)
    {
        var lamp = factory.CreateLamp();
        var sensor = factory.CreateSensor();

        lamp.TurnOn();
        sensor.Detect();
    }

    static void Main(string[] args)
    {
        IHomeDeviceFactory factory = new XiaomiHomeFactory();
        Init(factory);

        factory = new PhilipsHomeFactory();
        Init(factory);
    }
}