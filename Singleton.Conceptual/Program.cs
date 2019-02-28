// EN: Singleton Design Pattern
//
// Intent: Lets you ensure that a class has only one instance, while providing a
// global access point to this instance.
//
// RU: Паттерн Одиночка
//
// Назначение: Гарантирует, что у класса есть только один экземпляр, и
// предоставляет к нему глобальную точку доступа.

using System;

namespace Singleton
{
    // EN: The Singleton class defines the `getInstance` method that lets
    // clients access the unique singleton instance.
    //
    // RU: Класс Одиночка предоставляет метод getInstance, который позволяет
    // клиентам получить доступ к уникальному экземпляру одиночки.
    class Singleton
    {
        private static Singleton _instance;

        private static object _lock = new object();

        private Singleton()
        { }

        // EN: The static method that controls the access to the singleton
        // instance.
        //
        // This implementation let you subclass the Singleton class while
        // keeping just one instance of each subclass around.
        //
        // RU: Статический метод, управляющий доступом к экземпляру одиночки.
        //
        // Эта реализация позволяет вам расширять класс Одиночки, сохраняя
        // повсюду только один экземпляр каждого подкласса.
        public static Singleton getInstance()
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new Singleton());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
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
}
