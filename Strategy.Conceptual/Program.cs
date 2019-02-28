// EN: Strategy Design Pattern
//
// Intent: Lets you define a family of algorithms, put each of them into a
// separate class, and make their objects interchangeable.
//
// RU: Паттерн Стратегия
//
// Назначение: Определяет семейство схожих алгоритмов и помещает каждый из них в
// собственный класс, после чего алгоритмы можно взаимозаменять прямо во время
// исполнения программы.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Strategy.Conceptual
{
    // EN: The Context defines the interface of interest to clients.
    //
    // RU: Контекст определяет интерфейс, представляющий интерес для клиентов.
    class Context
    {
        // EN: The Context maintains a reference to one of the Strategy objects.
        // The Context does not know the concrete class of a strategy. It should
        // work with all strategies via the Strategy interface.
        //
        // RU: Контекст хранит ссылку на один из объектов Стратегии. Контекст не
        // знает конкретного класса стратегии. Он должен работать со всеми
        // стратегиями через интерфейс Стратегии.
        private IStrategy _strategy;

        public Context()
        { }

        // EN: Usually, the Context accepts a strategy through the constructor,
        // but also provides a setter to change it at runtime.
        //
        // RU: Обычно Контекст принимает стратегию через конструктор, а также
        // предоставляет сеттер для её изменения во время выполнения.
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // EN: Usually, the Context allows replacing a Strategy object at
        // runtime.
        //
        // RU: Обычно Контекст позволяет заменить объект Стратегии во время
        // выполнения.
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // EN: The Context delegates some work to the Strategy object instead of
        // implementing multiple versions of the algorithm on its own.
        //
        // RU: Вместо того, чтобы самостоятельно реализовывать множественные
        // версии алгоритма, Контекст делегирует некоторую работу объекту
        // Стратегии.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    // EN: The Strategy interface declares operations common to all supported
    // versions of some algorithm.
    //
    // The Context uses this interface to call the algorithm defined by Concrete
    // Strategies.
    //
    // RU: Интерфейс Стратегии объявляет операции, общие для всех поддерживаемых
    // версий некоторого алгоритма.
    //
    // Контекст использует этот интерфейс для вызова алгоритма, определённого
    // Конкретными Стратегиями.
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // EN: Concrete Strategies implement the algorithm while following the base
    // Strategy interface. The interface makes them interchangeable in the
    // Context.
    //
    // RU: Конкретные Стратегии реализуют алгоритм, следуя базовому интерфейсу
    // Стратегии. Этот интерфейс делает их взаимозаменяемыми в Контексте.
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code picks a concrete strategy and passes it to
            // the context. The client should be aware of the differences
            // between strategies in order to make the right choice.
            //
            // RU: Клиентский код выбирает конкретную стратегию и передаёт её в
            // контекст. Клиент должен знать о различиях между стратегиями,
            // чтобы сделать правильный выбор.
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");
            context.SetStrategy(new ConcreteStrategyA());
            context.DoSomeBusinessLogic();
            
            Console.WriteLine();
            
            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();
        }
    }
}
