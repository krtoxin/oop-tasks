using System.Collections.Generic;

public interface ICarDealer
{
    List<Car> SearchCars(string manufacturer, string model);
    bool BuyCarFromDealer(Car car, ICarDealer seller, decimal price);
    bool ExchangeCarWithDealer(Car myCar, ICarDealer otherDealer, Car theirCar);
}