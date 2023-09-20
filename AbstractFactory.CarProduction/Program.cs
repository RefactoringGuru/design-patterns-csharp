// Client

// Creating the sedan car
CarFactory sedan = new SedanFactory();
Car firstSedan = sedan.CreateCar("Blue");
Car secondSedan = sedan.CreateCar("Red");

// Creating the hatchBack car
CarFactory hatchBack = new HatchBackFactory();
Car firstHatchBack = hatchBack.CreateCar("White");
Car secondHatchBack = hatchBack.CreateCar("Black");

List<Car> sedans = new List<Car> { firstSedan, secondSedan };
foreach (var item in sedans)
{
    Console.WriteLine($"Type: {item.CarType} / EnginePower: {item.EnginePower} / MaximumSpeed: {item.MaximumSpeed} / Color: {item.Color}");
}

List<Car> hatchBacks = new List<Car> { firstHatchBack, secondHatchBack };
foreach (var item in hatchBacks)
{
    Console.WriteLine($"Type: {item.CarType} / EnginePower: {item.EnginePower} / MaximumSpeed: {item.MaximumSpeed} / Color: {item.Color}");
}


// Abstract Factory
public abstract class CarFactory
{
    public abstract Car CreateCar(string color);
}



// Concrete Factories
public class HatchBackFactory : CarFactory
{
    public override Car CreateCar(string color)
    {
        return new HatchBack
        {
            EnginePower = 1500,
            MaximumSpeed = 280,
            Color = color
        };
    }
}


public class SedanFactory : CarFactory
{
    public override Car CreateCar(string color)
    {
        return new Sedan
        {
            EnginePower = 2000,
            MaximumSpeed = 320,
            Color = color
        };
    }
}


// Abstract Entity
public abstract class Car
{
    public int EnginePower { get; set; }
    public int MaximumSpeed { get; set; }
    public string Color { get; set; }
    public abstract string CarType { get; }
}


// Concrete Entities
public class HatchBack : Car
{
    public override string CarType => nameof(HatchBack);
}

public class Sedan : Car
{
    public override string CarType => nameof(Sedan);
}


//Enumerations
public enum CarType
{
    HatchBack,
    Sedan
}
