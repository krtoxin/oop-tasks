namespace CoffeeMachineApp
{
    public interface ICoffeeMachine
    {
        int WaterAmount { get; }
        int BeansAmount { get; }
        bool IsWaterHeated { get; }

        void MakeEspresso();
        void MakeLatte();

        void AddWater(int amount);
        void AddBeans(int amount);
    }
}