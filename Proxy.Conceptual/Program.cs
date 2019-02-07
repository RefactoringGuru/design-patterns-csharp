 // EN: Proxy Design Pattern
 //
 // Intent: Provide a surrogate or placeholder for another object to control
 // access to the original object or to add other responsibilities.
 //
 // RU: Паттерн Заместитель
 //
 // Назначение: Предоставляет заменитель или местозаполнитель для другого
 // объекта, чтобы контролировать доступ к оригинальному объекту или добавлять
 // другие обязанности.

using System;

namespace RefactoringGuru.DesignPatterns.Proxy.Conceptual
{
    // EN: The Subject interface declares common operations for both RealSubject and
    // the Proxy. As long as the client works with RealSubject using this interface,
    // you'll be able to pass it a proxy instead of a real subject.
    //
    // RU: Интерфейс Субъекта объявляет общие операции как для Реального Субъекта,
    // так и для Заместителя. Пока клиент работает с Реальным Субъектом, используя
    // этот интерфейс,  вы сможете передать ему заместителя вместо реального
    // субъекта.
    public interface Subject
    {
        void Request();
    }
    
    // EN: The RealSubject contains some core business logic. Usually, RealSubjects
    // are capable of doing some useful work which may also be very slow or
    // sensitive - e.g. correcting input data. A Proxy can solve these issues
    // without any changes to the RealSubject's code.
    //
    // RU: Реальный Субъект содержит некоторую базовую бизнес-логику. Как правило,
    // Реальные Субъекты способны выполнять некоторую полезную работу, которая к
    // тому же может быть очень медленной или точной – например, коррекция входных
    // данных. Заместитель может решить эти задачи без каких-либо изменений в коде
    // Реального Субъекта.
    class RealSubject : Subject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }
    
    // EN: The Proxy has an interface identical to the RealSubject.
    //
    // RU: Интерфейс Заместителя идентичен интерфейсу Реального Субъекта.
    class Proxy : Subject
    {
        private RealSubject _realSubject;
        
        public Proxy(RealSubject realSubject)
        {
            this._realSubject = realSubject;
        }
        
        // EN: The most common applications of the Proxy pattern are lazy loading,
        // caching, controlling the access, logging, etc. A Proxy can perform one of
        // these things and then, depending on the result, pass the execution to the
        // same method in a linked RealSubject object.
        //
        // RU: Наиболее распространёнными областями применения паттерна Заместитель
        // являются ленивая загрузка, кэширование, контроль доступа, ведение журнала
        // и т.д. Заместитель может выполнить одну из этих задач, а затем, в
        // зависимости от результата, передать выполнение одноимённому методу в
        // связанном объекте класса Реального Субъект.
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
        public void ClientCode(Subject subject)
        {
            subject.Request();
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
