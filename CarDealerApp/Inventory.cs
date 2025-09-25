using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Car> cars = new List<Car>();

    public void AddCar(Car car) => cars.Add(car);
    public void RemoveCar(Car car) => cars.Remove(car);
    public List<Car> GetAllCars() => new List<Car>(cars);

    public List<Car> Search(string manufacturer, string model) =>
        cars.Where(c => c.Manufacturer == manufacturer && c.Model == model).ToList();
}