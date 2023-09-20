//Client

CarBuilder builder; 

Manager manager = new();
// Sedan building
builder = new SedanBuilder("White");
manager.Manage(builder);
builder.Car.ShowCar();

// HatchBack building
builder = new HatchBackBuilder("Black");
manager.Manage(builder);
builder.Car.ShowCar();



// Manager
public class Manager
{
	public void Manage(CarBuilder carbuilder)
	{
		carbuilder.Stamping();
		carbuilder.Welding();
		carbuilder.Mointing();
		carbuilder.Painting();
	}
}


// Abstract builder
public abstract class CarBuilder
{
	protected Car car;
    public Car Car
    {
        get { return car; }
    }
	public abstract void Stamping();
	public abstract void Welding();
	public abstract void Mointing();
	public abstract void Painting();
}

// Concrete builders
public class HatchBackBuilder : CarBuilder
{
	public HatchBackBuilder(string color)
	{
		car = new HatchBack
		{
			EnginePower = 1500,
            MaximumSpeed = 280,
            Color = color
		};
	}
	public override void Stamping() => Console.WriteLine($"HatchBack has been stamped");

	
	public override void Welding() => Console.WriteLine($"HatchBack has been welded");

	
	public override void Painting() => Console.WriteLine($"HatchBack has been painted");
	
	
	public override void Mointing() => Console.WriteLine($"HatchBack has been mointed");

}



public class SedanBuilder : CarBuilder
{
	public SedanBuilder(string color)
	{
		car = new Sedan
		{
			EnginePower = 2000,
            MaximumSpeed = 320,
            Color = color
		};
	}
	public override void Stamping() => Console.WriteLine($"Sedan has been stamped");

	
	public override void Welding() => Console.WriteLine($"Sedan has been welded");

	
	public override void Painting() => Console.WriteLine($"Sedan has been painted");
	
	
	public override void Mointing() => Console.WriteLine($"Sedan has been mointed");

}



// Abstract Entity

public abstract class Car
{
    public int EnginePower { get; set; }
    public int MaximumSpeed { get; set; }
    public string Color { get; set; }
    public abstract string CarType { get; }
	
	public void ShowCar() => Console.WriteLine($"CarType: {CarType} EnginePower :{EnginePower} MaximumSpeed: {MaximumSpeed} Color: {Color}");
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
