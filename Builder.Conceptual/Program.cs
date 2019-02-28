// EN: Builder Design Pattern
//
// Intent: Lets you construct complex objects step by step. The pattern allows
// you to produce different types and representations of an object using the
// same construction code.
//
// RU: Паттерн Строитель
//
// Назначение: Позволяет создавать сложные объекты пошагово. Строитель даёт
// возможность использовать один и тот же код строительства для получения разных
// представлений объектов.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Builder.Conceptual
{
    // EN: The Builder interface specifies methods for creating the different
    // parts of the Product objects.
    //
    // RU: Интерфейс Строителя объявляет создающие методы для различных частей
    // объектов Продуктов.
    public interface IBuilder
    {
        void BuildPartA();
		
        void BuildPartB();
		
        void BuildPartC();
    }
    
    // EN: The Concrete Builder classes follow the Builder interface and provide
    // specific implementations of the building steps. Your program may have
    // several variations of Builders, implemented differently.
    //
    // RU: Классы Конкретного Строителя следуют интерфейсу Строителя и
    // предоставляют конкретные реализации шагов построения. Ваша программа
    // может иметь несколько вариантов Строителей, реализованных по-разному.
    public class ConcreteBuilder : IBuilder
    {
        private Product _product = new Product();
        
        // EN: A fresh builder instance should contain a blank product object,
        // which is used in further assembly.
        //
        // RU: Новый экземпляр строителя должен содержать пустой объект
        // продукта, который используется в дальнейшей сборке.
        public ConcreteBuilder()
        {
            this.Reset();
        }
        
        public void Reset()
        {
            this._product = new Product();
        }
		
        // EN: All production steps work with the same product instance.
        //
        // RU: Все этапы производства работают с одним и тем же экземпляром
        // продукта.
        public void BuildPartA()
        {
            this._product.Add("PartA1");
        }
		
        public void BuildPartB()
        {
            this._product.Add("PartB1");
        }
		
        public void BuildPartC()
        {
            this._product.Add("PartC1");
        }
		
        // EN: Concrete Builders are supposed to provide their own methods for
        // retrieving results. That's because various types of builders may
        // create entirely different products that don't follow the same
        // interface. Therefore, such methods cannot be declared in the base
        // Builder interface (at least in a statically typed programming
        // language).
        //
        // Usually, after returning the end result to the client, a builder
        // instance is expected to be ready to start producing another product.
        // That's why it's a usual practice to call the reset method at the end
        // of the `GetProduct` method body. However, this behavior is not
        // mandatory, and you can make your builders wait for an explicit reset
        // call from the client code before disposing of the previous result.
        //
        // RU: Конкретные Строители должны предоставить свои собственные методы
        // получения результатов. Это связано с тем, что различные типы
        // строителей могут создавать совершенно разные продукты с разными
        // интерфейсами. Поэтому такие методы не могут быть объявлены в базовом
        // интерфейсе Строителя (по крайней мере, в статически типизированном
        // языке программирования).
        //
        // Как правило, после возвращения конечного результата клиенту,
        // экземпляр строителя должен быть готов к началу производства
        // следующего продукта. Поэтому обычной практикой является вызов метода
        // сброса в конце тела метода GetProduct. Однако такое поведение не
        // является обязательным, вы можете заставить своих строителей ждать
        // явного запроса на сброс из кода клиента, прежде чем избавиться от
        // предыдущего результата.
        public Product GetProduct()
        {
            Product result = this._product;

            this.Reset();

            return result;
        }
    }
    
    // EN: It makes sense to use the Builder pattern only when your products are
    // quite complex and require extensive configuration.
    //
    // Unlike in other creational patterns, different concrete builders can
    // produce unrelated products. In other words, results of various builders
    // may not always follow the same interface.
    //
    // RU: Имеет смысл использовать паттерн Строитель только тогда, когда ваши
    // продукты достаточно сложны и требуют обширной конфигурации.
    //
    // В отличие от других порождающих паттернов, различные конкретные строители
    // могут производить несвязанные продукты. Другими словами, результаты
    // различных строителей  могут не всегда следовать одному и тому же
    // интерфейсу.
    public class Product
    {
        private List<object> _parts = new List<object>();
		
        public void Add(string part)
        {
            this._parts.Add(part);
        }
		
        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Product parts: " + str + "\n";
        }
    }
    
    // EN: The Director is only responsible for executing the building steps in
    // a particular sequence. It is helpful when producing products according to
    // a specific order or configuration. Strictly speaking, the Director class
    // is optional, since the client can control builders directly.
    //
    // RU: Директор отвечает только за выполнение шагов построения в
    // определённой последовательности. Это полезно при производстве продуктов в
    // определённом порядке или особой конфигурации. Строго говоря, класс
    // Директор необязателен, так как клиент может напрямую управлять
    // строителями.
    public class Director
    {
        private IBuilder _builder;
        
        public IBuilder Builder
        {
            set { _builder = value; } 
        }
        
        // EN: The Director can construct several product variations using the
        // same building steps.
        //
        // RU: Директор может строить несколько вариаций продукта, используя
        // одинаковые шаги построения.
        public void buildMinimalViableProduct()
        {
            this._builder.BuildPartA();
        }
		
        public void buildFullFeaturedProduct()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code creates a builder object, passes it to the
            // director and then initiates the construction process. The end
            // result is retrieved from the builder object.
            //
            // RU: Клиентский код создаёт объект-строитель, передаёт его
            // директору, а затем инициирует  процесс построения. Конечный
            // результат извлекается из объекта-строителя.
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;
            
            Console.WriteLine("Standard basic product:");
            director.buildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard full featured product:");
            director.buildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            // EN: Remember, the Builder pattern can be used without a Director
            // class.
            //
            // RU: Помните, что паттерн Строитель можно использовать без класса
            // Директор.
            Console.WriteLine("Custom product:");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.Write(builder.GetProduct().ListParts());
        }
    }
}
