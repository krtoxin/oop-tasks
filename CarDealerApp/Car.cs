public class Car
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal BasePrice { get; set; }

    public Car(string manufacturer, string model, int year, decimal basePrice)
    {
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        BasePrice = basePrice;
    }
}