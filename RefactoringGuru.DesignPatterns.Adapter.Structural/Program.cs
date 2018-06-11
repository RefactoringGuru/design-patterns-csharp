using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Structural
{
    /// <summary>
    /// EN: Adapter Design Pattern
    ///
    /// Converts the interface of a class into the interface clients expect.
    /// Adapter lets classes work together where they otherwise couldn't, due to incompatible interfaces.
    /// 
    /// RU: Паттерн Адаптер
    /// 
    /// Преобразует интерфейс одного класса к интерфейсу другого класса.
    /// Адаптер позволяет классам работать друг с другом, даже если их интерфейсы несовместимы.
    /// </summary>


    /// <summary>
    /// EN: ITarget defines interface expected by the client.
    /// 
    /// RU: ITarget определяет интерфейс, с которым может работать клиент.
    /// </summary>
    interface ITarget
    {
        string GetRequest();
    }

    /// <summary>
    /// EN: The Adaptee contains some useful behavior, but its interface is incompatible
    /// with the existing client code.The Adaptee needs some adaptation before the client code can use it.
    /// 
    /// RU: Класс Adaptee содержит полезные методы, но его интерфейс несовместим с тем, который
    /// ожидается клиентом. Интерфейс Adaptee требует некоторой адаптации для того,
    /// чтобы клиент мог его использовать.
    /// </summary>
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    /// <summary>
    /// EN: The Adapter makes the Adaptee's interface compatible with the ITarget interface. 
    /// 
    /// RU: Адаптер позволяет привести интерфейс Adaptee к ожидаемому клиентом интерфейсу ITarget.
    /// </summary>
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"This is '{_adaptee.GetSpecificRequest()}'";
        }
    }

    class Client
    {
        public void Main()
        {
            Adaptee adaptee = new Adaptee();

            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");

            Console.WriteLine(target.GetRequest());
        }
    }

    class Program
    {
        static void Main()
        {
            new Client().Main();
        }
    }
}
