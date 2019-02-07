// EN: Singleton Design Pattern
//
// Intent: Ensure that a class has a single instance, and provide a global point
// of access to it.
//
// RU: Паттерн Одиночка
//
// Назначение: Гарантирует существование единственного экземпляра класса и
// предоставляет глобальную точку доступа к нему.

using System;

namespace Singleton
{
    // EN: The Singleton class defines the `getInstance` method that lets clients
    // access the unique singleton instance.
    //
    // RU: Класс Одиночка предоставляет метод getInstance, который позволяет
    // клиентам получить доступ к уникальному экземпляру одиночки.
    class Singleton
    {
        private static Singleton _instance;

        private static object _lock = new object();

        private Singleton()
        { }

        // EN: The static method that controls the access to the singleton instance.
        //
        // This implementation let you subclass the Singleton class while keeping
        // just one instance of each subclass around.
        //
        // RU: Статический метод, управляющий доступом к экземпляру одиночки.
        //
        // Эта реализация позволяет вам расширять класс Одиночки,
        // сохраняя повсюду только один экземпляр каждого подкласса.
        public static Singleton getInstance()
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new Singleton());
            }
        }
    }
    
    class Client
    {
        public void ClientCode()
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            Singleton s1 = Singleton.getInstance();
            Singleton s2 = Singleton.getInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.ClientCode();
        }
    }
}
