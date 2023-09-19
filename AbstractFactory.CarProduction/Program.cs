// Client
Car suvCar = SuvFactory.CreateInstance(CarType.Suv, new Suv(2000, 320, "Red"));
Car sedanCar = SedanFactory.CreateInstance(CarType.Sedan, new Suv(1500, 280, "Black"));
Car hatchBackCar = HatchBackFactory.CreateInstance(CarType.Sedan, new Suv(2500, 230, "White"));

List<Car> cars = new() { suvCar, sedanCar, hatchBackCar };

foreach (Car car in cars)
{
    Console.WriteLine($"{car.Color} {car.EnginePower} KW - {car.MaximumSpeed} MPH  created!");
}


// Abstract Factory
public abstract class CarFactory
{
    private static Car Instance { get; set; }
    public static Car GetCar => Instance;

    public static Car CreateInstance(CarType carType, Car car)
    {
        Instance = carType switch
        {
            CarType.Suv => new Suv(car.EnginePower, car.MaximumSpeed, car.Color),
            CarType.Sedan => new Sedan(car.EnginePower, car.MaximumSpeed, car.Color),
            CarType.HatchBack => new HatchBack(car.EnginePower, car.MaximumSpeed, car.Color),
        };

        return Instance;
    }
}


// Concrete Factories
public class SuvFactory : CarFactory { }

public class SedanFactory : CarFactory { }

public class HatchBackFactory : CarFactory { }


// Abstract Product
public abstract class Car
{
    protected Car(int enginePower, int maximumSpeed, string color)
    {
        EnginePower = enginePower;
        MaximumSpeed = maximumSpeed;
        Color = color;
    }
    public int EnginePower { get; set; }
    public int MaximumSpeed { get; set; }
    public string Color { get; set; }
}

// Concrete Products
public class Suv : Car
{
    public Suv(int enginePower, int maximumSpeed, string color) : base(enginePower, maximumSpeed, color) { }
}

public class Sedan : Car
{
    public Sedan(int enginePower, int maximumSpeed, string color) : base(enginePower, maximumSpeed, color) { }
}

public class HatchBack : Car
{
    public HatchBack(int enginePower, int maximumSpeed, string color) : base(enginePower, maximumSpeed, color) { }
}

public enum CarType
{
    Suv, Sedan, HatchBack
}

