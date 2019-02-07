// EN: Adapter Design Pattern
//
// Intent: Convert the interface of a class into the interface clients expect.
// Adapter lets classes work together where they otherwise couldn't, due to
// incompatible interfaces.
//
// RU: Паттерн Адаптер
//
// Назначение: Преобразует интерфейс класса в интерфейс, ожидаемый клиентами.
// Адаптер позволяет классам с несовместимыми интерфейсами работать вместе.

using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Conceptual
{
    // EN: The Target defines the domain-specific interface used by the client code.
    //
    // RU: Целевой класс объявляет интерфейс, с которым может работать клиентский
    // код.
    interface ITarget
    {
        string GetRequest();
    }

    // EN: The Adaptee contains some useful behavior, but its interface is
    // incompatible with the existing client code. The Adaptee needs some adaptation
    // before the client code can use it.
    //
    // RU: Адаптируемый класс содержит некоторое полезное поведение, но его
    // интерфейс несовместим  с существующим клиентским кодом. Адаптируемый класс
    // нуждается в некоторой доработке,  прежде чем клиентский код сможет его
    // использовать.
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    // EN: The Adapter makes the Adaptee's interface compatible with the Target's
    // interface.
    //
    // RU: Адаптер делает интерфейс Адаптируемого класса совместимым с целевым
    // интерфейсом.
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

    class Program
    {
        static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");

            Console.WriteLine(target.GetRequest());
        }
    }
}
