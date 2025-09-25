using System.Collections.Generic;

public class CarDealer : ICarDealer
{
    public string Name { get; }
    public Inventory Inventory { get; }
    public CurrentAccount Account { get; }
    public decimal UserMarkup { get; set; } = 0.1m;

    public CarDealer(string name)
    {
        Name = name;
        Inventory = new Inventory();
        Account = new CurrentAccount();
    }

    public void BuyCar(Car car, decimal price)
    {
        if (Account.DeductFunds(price))
        {
            Inventory.AddCar(car);
        }
    }

    public void SellCar(Car car, decimal price)
    {
        Inventory.RemoveCar(car);
        Account.AddFunds(price);
    }

    public List<Car> SearchCars(string manufacturer, string model)
    {
        return Inventory.Search(manufacturer, model);
    }

    public decimal GetUserPrice(Car car)
    {
        return car.BasePrice * (1 + UserMarkup);
    }

    public bool BuyCarFromDealer(Car car, ICarDealer seller, decimal price)
    {
        if (Account.DeductFunds(price))
        {
            if (seller is CarDealer dealerSeller && dealerSeller.Inventory.GetAllCars().Contains(car))
            {
                dealerSeller.SellCar(car, price);
                Inventory.AddCar(car);
                return true;
            }
        }
        return false;
    }

    public bool ExchangeCarWithDealer(Car myCar, ICarDealer otherDealer, Car theirCar)
    {
        if (Inventory.GetAllCars().Contains(myCar)
            && otherDealer is CarDealer dealerOther
            && dealerOther.Inventory.GetAllCars().Contains(theirCar))
        {
            decimal myPrice = myCar.BasePrice;
            decimal theirPrice = theirCar.BasePrice;
            decimal diff = theirPrice - myPrice;

            if (diff > 0)
            {
                if (!Account.DeductFunds(diff))
                    return false;
                dealerOther.Account.AddFunds(diff);
            }
            else if (diff < 0)
            {
                if (!dealerOther.Account.DeductFunds(-diff))
                    return false;
                Account.AddFunds(-diff);
            }

            Inventory.RemoveCar(myCar);
            dealerOther.Inventory.RemoveCar(theirCar);

            Inventory.AddCar(theirCar);
            dealerOther.Inventory.AddCar(myCar);

            return true;
        }
        return false;
    }
}