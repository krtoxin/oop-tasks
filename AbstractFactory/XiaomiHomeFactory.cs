public class XiaomiHomeFactory : IHomeDeviceFactory
{
    public ILamp CreateLamp()
    {
        return new XiaomiLamp();
    }

    public ISensor CreateSensor()
    {
        return new XiaomiSensor();
    }
}