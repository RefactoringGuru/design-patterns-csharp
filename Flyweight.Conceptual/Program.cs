// EN: Flyweight Design Pattern
//
// Intent: Lets you fit more objects into the available amount of RAM by sharing
// common parts of state between multiple objects, instead of keeping all of the
// data in each object.
//
// RU: Паттерн Легковес
//
// Назначение: Позволяет вместить бóльшее количество объектов в отведённую
// оперативную память. Легковес экономит память, разделяя общее состояние
// объектов между собой, вместо хранения одинаковых данных в каждом объекте.

using System;
using System.Collections.Generic;
using System.Linq;
// EN: Use Json.NET library, you can download it from NuGet Package Manager
//
// RU: Используем библиотеку Json.NET, загрузить можно через NuGet Package
// Manager
using Newtonsoft.Json;

namespace RefactoringGuru.DesignPatterns.Flyweight.Conceptual
{
    // EN: The Flyweight stores a common portion of the state (also called
    // intrinsic state) that belongs to multiple real business entities. The
    // Flyweight accepts the rest of the state (extrinsic state, unique for each
    // entity) via its method parameters.
    //
    // RU: Легковес хранит общую часть состояния (также называемую внутренним
    // состоянием), которая принадлежит нескольким реальным бизнес-объектам.
    // Легковес принимает оставшуюся часть состояния (внешнее состояние,
    // уникальное для каждого объекта) через его параметры метода.
    public class Flyweight
    {
        private Car _sharedState;

        public Flyweight(Car car)
        {
            this._sharedState = car;
        }

        public void Operation(Car uniqueState)
        {
            string s = JsonConvert.SerializeObject(this._sharedState);
            string u = JsonConvert.SerializeObject(uniqueState);
            Console.WriteLine($"Flyweight: Displaying shared {s} and unique {u} state.");
        }
    }

    // EN: The Flyweight Factory creates and manages the Flyweight objects. It
    // ensures that flyweights are shared correctly. When the client requests a
    // flyweight, the factory either returns an existing instance or creates a
    // new one, if it doesn't exist yet.
    //
    // RU: Фабрика Легковесов создает объекты-Легковесы и управляет ими. Она
    // обеспечивает правильное разделение легковесов. Когда клиент запрашивает
    // легковес, фабрика либо возвращает существующий экземпляр, либо создает
    // новый, если он ещё не существует.
    public class FlyweightFactory
    {
        private List<Tuple<Flyweight, string>> flyweights = new List<Tuple<Flyweight, string>>();

        public FlyweightFactory(params Car[] args)
        {
            foreach (var elem in args)
            {
                flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(elem), this.getKey(elem)));
            }
        }

        // EN: Returns a Flyweight's string hash for a given state.
        //
        // RU: Возвращает хеш строки Легковеса для данного состояния.
        public string getKey(Car key)
        {
            List<string> elements = new List<string>();

            elements.Add(key.Model);
            elements.Add(key.Color);
            elements.Add(key.Company);

            if (key.Owner != null && key.Number != null)
            {
                elements.Add(key.Number);
                elements.Add(key.Owner);
            }

            elements.Sort();

            return string.Join("_", elements);
        }

        // EN: Returns an existing Flyweight with a given state or creates a new
        // one.
        //
        // RU: Возвращает существующий Легковес с заданным состоянием или
        // создает новый.
        public Flyweight GetFlyweight(Car sharedState)
        {
            string key = this.getKey(sharedState);

            if (flyweights.Where(t => t.Item2 == key).Count() == 0)
            {
                Console.WriteLine("FlyweightFactory: Can't find a flyweight, creating new one.");
                this.flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), key));
            }
            else
            {
                Console.WriteLine("FlyweightFactory: Reusing existing flyweight.");
            }
            return this.flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
        }

        public void listFlyweights()
        {
            var count = flyweights.Count;
            Console.WriteLine($"\nFlyweightFactory: I have {count} flyweights:");
            foreach (var flyweight in flyweights)
            {
                Console.WriteLine(flyweight.Item2);
            }
        }
    }

    public class Car
    {
        public string Owner { get; set; }

        public string Number { get; set; }

        public string Company { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code usually creates a bunch of pre-populated
            // flyweights in the initialization stage of the application.
            //
            // RU: Клиентский код обычно создает кучу предварительно заполненных
            // легковесов на этапе инициализации приложения.
            var factory = new FlyweightFactory(
                new Car { Company = "Chevrolet", Model = "Camaro2018", Color = "pink" },
                new Car { Company = "Mercedes Benz", Model = "C300", Color = "black" },
                new Car { Company = "Mercedes Benz", Model = "C500", Color = "red" },
                new Car { Company = "BMW", Model = "M5", Color = "red" },
                new Car { Company = "BMW", Model = "X6", Color = "white" }
            );
            factory.listFlyweights();

            addCarToPoliceDatabase(factory, new Car {
                Number = "CL234IR",
                Owner = "James Doe",
                Company = "BMW",
                Model = "M5",
                Color = "red"
            });

            addCarToPoliceDatabase(factory, new Car {
                Number = "CL234IR",
                Owner = "James Doe",
                Company = "BMW",
                Model = "X1",
                Color = "red"
            });

            factory.listFlyweights();
        }

        public static void addCarToPoliceDatabase(FlyweightFactory factory, Car car)
        {
            Console.WriteLine("\nClient: Adding a car to database.");

            var flyweight = factory.GetFlyweight(new Car {
                Color = car.Color,
                Model = car.Model,
                Company = car.Company
            });

            // EN: The client code either stores or calculates extrinsic state
            // and passes it to the flyweight's methods.
            //
            // RU: Клиентский код либо сохраняет, либо вычисляет внешнее
            // состояние и передает его методам легковеса.
            flyweight.Operation(car);
        }
    }
}
