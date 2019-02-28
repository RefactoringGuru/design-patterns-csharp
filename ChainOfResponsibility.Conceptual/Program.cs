// EN: Chain of Responsibility Design Pattern
//
// Intent: Avoid coupling a sender of a request to its receiver by giving more
// than one object a chance to handle the request. Chain the receiving objects
// and then pass the request through the chain until some receiver handles it.
//
// RU: Паттерн Цепочка обязанностей
//
// Назначение: Позволяет избежать привязки отправителя запроса к его получателю,
// предоставляя возможность обработать запрос нескольким объектам.  Связывает в
// цепочку объекты-получатели, а затем передаёт запрос по цепочке, пока некий
// получатель не обработает его.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.ChainOfResponsibility.Conceptual
{
    // EN: The Handler interface declares a method for building the chain of
    // handlers. It also declares a method for executing a request.
    //
    // RU: Интерфейс Обработчика объявляет метод построения цепочки
    // обработчиков. Он также объявляет метод для выполнения запроса.
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
		
        object Handle(object request);
    }

    // EN: The default chaining behavior can be implemented inside a base
    // handler class.
    //
    // RU: Поведение цепочки по умолчанию может быть реализовано внутри базового
    // класса обработчика.
    abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            
            // EN: Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            //
            // RU: Возврат обработчика отсюда позволит связать обработчики
            // простым способом, вот так:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }
		
        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }

    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "Banana")
            {
                return $"Monkey: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "Nut")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "MeatBall")
            {
                return $"Dog: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class Client
    {
        // EN: The client code is usually suited to work with a single handler.
        // In most cases, it is not even aware that the handler is part of a
        // chain.
        //
        // RU: Обычно клиентский код приспособлен для работы с единственным
        // обработчиком. В большинстве случаев клиенту даже неизвестно, что этот
        // обработчик является частью цепочки.
        public static void ClientCode(AbstractHandler handler)
        {
            foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
            {
                Console.WriteLine($"Client: Who wants a {food}?");

                var result = handler.Handle(food);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {food} was left untouched.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The other part of the client code constructs the actual
            // chain.
            //
            // RU: Другая часть клиентского кода создает саму цепочку.
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var dog = new DogHandler();

            monkey.SetNext(squirrel).SetNext(dog);

            // EN: The client should be able to send a request to any handler,
            // not just the first one in the chain.
            //
            // RU: Клиент должен иметь возможность отправлять запрос любому
            // обработчику, а не только первому в цепочке.
            Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            Client.ClientCode(monkey);
            Console.WriteLine();

            Console.WriteLine("Subchain: Squirrel > Dog\n");
            Client.ClientCode(squirrel);
        }
    }
}
