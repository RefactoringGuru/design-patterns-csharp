using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new ConcreteBuilder();
            Director director = new Director(builder);

            Client client = new Client();
            client.ClientCode(director, builder);
        }
    }

    public class Client
    {
        public void ClientCode(Director director, Builder builder)
        {
            Console.WriteLine("Standart basic product:");
            director.buildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standart full featured product:");
            director.buildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.WriteLine(builder.GetProduct().ListParts());
        }
    }

    public class Director
    {
        Builder builder;

        public Director(Builder builder)
        {
            this.builder = builder;
        }

        public void buildMinimalViableProduct()
        {
            builder.BuildPartA();
        }
		
        public void buildFullFeaturedProduct()
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();
        }
    }

    public abstract class Builder
    {
        public abstract void BuildPartA();
		
        public abstract void BuildPartB();
		
        public abstract void BuildPartC();
		
        public abstract Product GetProduct();
    }

    public class Product
    {
        List<object> parts = new List<object>();
		
        public void Add(string part)
        {
            parts.Add(part);
        }
		
        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < parts.Count; i++)
            {
                str += parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Product parts: " + str + "\n";
        }
    }

    public class ConcreteBuilder : Builder
    {
        Product product = new Product();
		
        public override void BuildPartA()
        {
            product.Add("PartA1");
        }
		
        public override void BuildPartB()
        {
            product.Add("PartB1");
        }
		
        public override void BuildPartC()
        {
            product.Add("PartC1");
        }
		
        public override Product GetProduct()
        {
            Product result = product;

            this.Reset();

            return result;
        }
		
        public void Reset()
        {
            product = new Product();
        }
    }
}
