using System;

namespace CoffeeMachineApp
{
    public class CoffeeMachine : ICoffeeMachine
    {
        private int waterAmount;
        private int beansAmount;
        private bool isWaterHeated;

        private const int MaxWater = 2000; 
        private const int MaxBeans = 500; 

        public int WaterAmount => waterAmount;
        public int BeansAmount => beansAmount;
        public bool IsWaterHeated => isWaterHeated;

        public CoffeeMachine(int initialWater, int initialBeans)
        {
            waterAmount = Math.Min(initialWater, MaxWater);
            beansAmount = Math.Min(initialBeans, MaxBeans);
            isWaterHeated = false;
        }

        public void AddWater(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Кількість води має бути додатною.");
                return;
            }
            waterAmount = Math.Min(waterAmount + amount, MaxWater);
            Console.WriteLine($"Додано {amount} мл води.");
        }

        public void AddBeans(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Кількість зерен має бути додатною.");
                return;
            }
            beansAmount = Math.Min(beansAmount + amount, MaxBeans);
            Console.WriteLine($"Додано {amount} г кавових зерен.");
        }

        private void HeatWater()
        {
            isWaterHeated = true;
            Console.WriteLine("Вода нагріта.");
        }

        private bool GrindBeans(int requiredBeans)
        {
            if (beansAmount < requiredBeans)
                return false;
            beansAmount -= requiredBeans;
            return true;
        }

        private bool CheckWater(int requiredWater)
        {
            return waterAmount >= requiredWater;
        }

        public void MakeEspresso()
        {
            const int espressoBeans = 20;
            const int espressoWater = 50;

            if (!CheckWater(espressoWater))
            {
                Console.WriteLine("Недостатньо води для еспресо.");
                return;
            }
            if (beansAmount < espressoBeans)
            {
                Console.WriteLine("Недостатньо кавових зерен для еспресо.");
                return;
            }
            if (!isWaterHeated)
            {
                Console.WriteLine("Вода не нагріта. Підігріваємо...");
                HeatWater();
            }

            if (!GrindBeans(espressoBeans))
            {
                Console.WriteLine("Сталася помилка з меленям кави.");
                return;
            }
            waterAmount -= espressoWater;
            isWaterHeated = false; 
            Console.WriteLine("Еспресо готове! Смачного!");
        }

        public void MakeLatte()
        {
            const int latteBeans = 25;
            const int latteWater = 100;

            if (!CheckWater(latteWater))
            {
                Console.WriteLine("Недостатньо води для лате.");
                return;
            }
            if (beansAmount < latteBeans)
            {
                Console.WriteLine("Недостатньо кавових зерен для лате.");
                return;
            }
            if (!isWaterHeated)
            {
                Console.WriteLine("Вода не нагріта. Підігріваємо...");
                HeatWater();
            }

            if (!GrindBeans(latteBeans))
            {
                Console.WriteLine("Сталася помилка з меленям кави.");
                return;
            }
            waterAmount -= latteWater;
            isWaterHeated = false; 
            Console.WriteLine("Лате готове! Смачного!");
        }
    }
}