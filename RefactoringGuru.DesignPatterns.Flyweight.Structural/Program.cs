using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Flyweight.Structural
{
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
            Console.Write($"Flyweight: Displaying shared {s} and unique {u} state.\n");
        }
    }

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

        public string getKey(Car key)
        {
            List<string> elements = new List<string>();

            elements.Add(key.Model);
            elements.Add(key.Color);
            elements.Add(key.Company);

            if(key.Owner != null && key.Number != null)
            {
                elements.Add(key.Number);
                elements.Add(key.Owner);
            }

            elements.Sort();

            return string.Join("_", elements);
        }

        public Flyweight GetFlyweight(Car sharedState)
        {
            string key = this.getKey(sharedState);

            if (flyweights.Where(t => t.Item2 == key).Count() == 0)
            {
                Console.Write("FlyweightFactory: Can't find a flyweight, creating new one.\n");
                this.flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), key));
            }
            else
            {
                Console.Write("FlyweightFactory: Reusing existing flyweight.\n");
            }
            return this.flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
        }

        public void listFlyweights()
        {
            var count = flyweights.Count;
            Console.Write($"\nFlyweightFactory: I have {count} flyweights:\n");
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
            Client client = new Client();
            client.ClientCode();
        }
    }

    public class Client
    {
        public void addCarToPoliceDatabase(FlyweightFactory factory, Car car)
        {
            Console.Write("\nClient: Adding a car to database.\n");

            var flyweight = factory.GetFlyweight(new Car { Color = car.Color, Model = car.Model, Company = car.Company });
            flyweight.Operation(car);
        }

        public void ClientCode()
        {
            var factory = new FlyweightFactory
                (
                    new Car { Company = "Chevrolet", Model = "Camaro2018", Color = "pink" },
                    new Car { Company = "Mercedes Benz", Model = "C300", Color = "black" },
                    new Car { Company = "Mercedes Benz", Model = "C500", Color = "red" },
                    new Car { Company = "BMW", Model = "M5", Color = "red" },
                    new Car { Company = "BMW", Model = "X6", Color = "white" }
                );
            factory.listFlyweights();

            addCarToPoliceDatabase(factory, 
                new Car { Number = "CL234IR",
                          Owner = "James Doe",
                          Company = "BMW",
                          Model = "M5",
                          Color = "red"
                        });


            addCarToPoliceDatabase(factory,
                new Car
                {
                    Number = "CL234IR",
                    Owner = "James Doe",
                    Company = "BMW",
                    Model = "X1",
                    Color = "red"
                });

            factory.listFlyweights();
        }
    }
}
