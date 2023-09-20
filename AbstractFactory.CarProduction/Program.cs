// Client

// Creating the sedan car
CarFactory sedanFactory = new SedanFactory();
Car firstSedan = sedanFactory.CreateCar("Blue");
Car secondSedan = sedanFactory.CreateCar("Red");

// Creating the hatchBack car
CarFactory hatchBackFactory = new HatchBackFactory();
Car firstHatchBack = hatchBackFactory.CreateCar("White");
Car secondHatchBack = hatchBackFactory.CreateCar("Black");



foreach (var item in hatchBackFactory.Cars)
{
    Console.WriteLine($"Type: {item.CarType} / EnginePower: {item.EnginePower} / MaximumSpeed: {item.MaximumSpeed} / Color: {item.Color}");
}


foreach (var item in sedanFactory.Cars)
{
    Console.WriteLine($"Type: {item.CarType} / EnginePower: {item.EnginePower} / MaximumSpeed: {item.MaximumSpeed} / Color: {item.Color}");
}




// Abstract Factory
public abstract class CarFactory
{
    public abstract Car CreateCar(string color);
    public List<Car>Cars {get;set;} = new();

}



// Concrete Factories
public class HatchBackFactory : CarFactory
{
    public override Car CreateCar(string color)
    {
        Car hatchBach = new HatchBack
        {
            EnginePower = 1500,
            MaximumSpeed = 280,
            Color = color
        };

        base.Cars.Add(hatchBach);
        return hatchBach;
    }
}


public class SedanFactory : CarFactory
{
    public override Car CreateCar(string color)
    {
        Car sedan= new Sedan
        {
            EnginePower = 2000,
            MaximumSpeed = 320,
            Color = color
        };
        base.Cars.Add(sedan);
        return sedan;
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

