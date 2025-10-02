public interface IHomeDeviceFactory
{
    ILamp CreateLamp();
    ISensor CreateSensor();
}