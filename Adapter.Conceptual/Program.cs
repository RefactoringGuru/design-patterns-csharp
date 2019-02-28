// EN: Adapter Design Pattern
//
// Intent: Provides a unified interface that allows objects with incompatible
// interfaces to collaborate.
//
// RU: Паттерн Адаптер
//
// Назначение: Позволяет объектам с несовместимыми интерфейсами работать вместе.

using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Conceptual
{
    // EN: The Target defines the domain-specific interface used by the client
    // code.
    //
    // RU: Целевой класс объявляет интерфейс, с которым может работать
    // клиентский код.
    public interface ITarget
    {
        string GetRequest();
    }

    // EN: The Adaptee contains some useful behavior, but its interface is
    // incompatible with the existing client code. The Adaptee needs some
    // adaptation before the client code can use it.
    //
    // RU: Адаптируемый класс содержит некоторое полезное поведение, но его
    // интерфейс несовместим  с существующим клиентским кодом. Адаптируемый
    // класс нуждается в некоторой доработке,  прежде чем клиентский код сможет
    // его использовать.
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    // EN: The Adapter makes the Adaptee's interface compatible with the
    // Target's interface.
    //
    // RU: Адаптер делает интерфейс Адаптируемого класса совместимым с целевым
    // интерфейсом.
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"This is '{this._adaptee.GetSpecificRequest()}'";
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
