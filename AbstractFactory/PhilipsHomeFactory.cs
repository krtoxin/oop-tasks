public class PhilipsHomeFactory : IHomeDeviceFactory
{
    public ILamp CreateLamp()
    {
        return new PhilipsLamp();
    }

    public ISensor CreateSensor()
    {
        return new PhilipsSensor();
    }
}