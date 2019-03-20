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
using System.Threading;

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

        private static readonly object _lock = new object();
        
        private string _value;

        public Singleton(string value)
        {
            this._value = value; 
        }
        
        public string GetValue()
        {
            return this._value;
        }

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
        public static Singleton GetInstance(string value)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Singleton(value);
                }
            }
            return _instance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            
            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );
            
            Thread process1 = new Thread(() => {
                TestSingleton("FOO");
            });
            Thread process2 = new Thread(() => {
                TestSingleton("BAR");
            });
            process1.Start();
            process2.Start();
        }
        
        static public void TestSingleton(string value)
        {
            Singleton singleton = Singleton.GetInstance(value);
            Console.WriteLine(singleton.GetValue());
        } 
    }
}
