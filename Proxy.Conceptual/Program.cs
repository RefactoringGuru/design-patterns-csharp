// EN: Proxy Design Pattern
//
// Intent: Lets you provide a substitute or placeholder for another object. A
// proxy controls access to the original object, allowing you to perform
// something either before or after the request gets through to the original
// object.
//
// RU: Паттерн Заместитель
//
// Назначение: Позволяет подставлять вместо реальных объектов специальные
// объекты-заменители. Эти объекты перехватывают вызовы к оригинальному объекту,
// позволяя сделать что-то до или после передачи вызова оригиналу.

using System;

namespace RefactoringGuru.DesignPatterns.Proxy.Conceptual
{
    // EN: The Subject interface declares common operations for both RealSubject
    // and the Proxy. As long as the client works with RealSubject using this
    // interface, you'll be able to pass it a proxy instead of a real subject.
    //
    // RU: Интерфейс Субъекта объявляет общие операции как для Реального
    // Субъекта, так и для Заместителя. Пока клиент работает с Реальным
    // Субъектом, используя этот интерфейс, вы сможете передать ему заместителя
    // вместо реального субъекта.
    public interface ISubject
    {
        void Request();
    }
    
    // EN: The RealSubject contains some core business logic. Usually,
    // RealSubjects are capable of doing some useful work which may also be very
    // slow or sensitive - e.g. correcting input data. A Proxy can solve these
    // issues without any changes to the RealSubject's code.
    //
    // RU: Реальный Субъект содержит некоторую базовую бизнес-логику. Как
    // правило, Реальные Субъекты способны выполнять некоторую полезную работу,
    // которая к тому же может быть очень медленной или точной – например,
    // коррекция входных данных. Заместитель может решить эти задачи без
    // каких-либо изменений в коде Реального Субъекта.
    class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }
    
    // EN: The Proxy has an interface identical to the RealSubject.
    //
    // RU: Интерфейс Заместителя идентичен интерфейсу Реального Субъекта.
    class Proxy : ISubject
    {
        private RealSubject _realSubject;
        
        public Proxy(RealSubject realSubject)
        {
            this._realSubject = realSubject;
        }
        
        // EN: The most common applications of the Proxy pattern are lazy
        // loading, caching, controlling the access, logging, etc. A Proxy can
        // perform one of these things and then, depending on the result, pass
        // the execution to the same method in a linked RealSubject object.
        //
        // RU: Наиболее распространёнными областями применения паттерна
        // Заместитель являются ленивая загрузка, кэширование, контроль доступа,
        // ведение журнала и т.д. Заместитель может выполнить одну из этих
        // задач, а затем, в зависимости от результата, передать выполнение
        // одноимённому методу в связанном объекте класса Реального Субъект.
        public void Request()
        {
            if (this.CheckAccess())
            {
                this._realSubject = new RealSubject();
                this._realSubject.Request();

                this.LogAccess();
            }
        }
		
        public bool CheckAccess()
        {
            // EN: Some real checks should go here.
            //
            // RU: Некоторые реальные проверки должны проходить здесь.
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }
		
        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }
    
    public class Client
    {
        // EN: The client code is supposed to work with all objects (both
        // subjects and proxies) via the Subject interface in order to support
        // both real subjects and proxies. In real life, however, clients mostly
        // work with their real subjects directly. In this case, to implement
        // the pattern more easily, you can extend your proxy from the real
        // subject's class.
        //
        // RU: Клиентский код должен работать со всеми объектами (как с
        // реальными, так и заместителями) через интерфейс Субъекта, чтобы
        // поддерживать как реальные субъекты, так и заместителей. В реальной
        // жизни, однако, клиенты в основном работают с реальными субъектами
        // напрямую. В этом случае, для более простой реализации паттерна, можно
        // расширить заместителя из класса реального субъекта.
        public void ClientCode(ISubject subject)
        {
            // ...
            
            subject.Request();
            
            // ...
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            
            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new RealSubject();
            client.ClientCode(realSubject);

            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);
            client.ClientCode(proxy);
        }
    }
}
