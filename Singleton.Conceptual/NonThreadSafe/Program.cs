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

namespace RefactoringGuru.DesignPatterns.Singleton.Conceptual.NonThreadSafe
{
    // EN: The Singleton class defines the `GetInstance` method that serves as
    // an alternative to constructor and lets clients access the same instance
    // of this class over and over.
    //
    // RU: Класс Одиночка предоставляет метод `GetInstance`, который ведёт себя
    // как альтернативный конструктор и позволяет клиентам получать один и тот
    // же экземпляр класса при каждом вызове.

    // EN : The Singleton should always be a 'sealed' class to prevent class
    // inheritance through external classes and also through nested classes.
    public sealed class Singleton
    {
        // EN: The Singleton's constructor should always be private to prevent
        // direct construction calls with the `new` operator.
        //
        // RU: Конструктор Одиночки всегда должен быть скрытым, чтобы
        // предотвратить создание объекта через оператор new.
        private Singleton() { }

        // EN: The Singleton's instance is stored in a static field. There there
        // are multiple ways to initialize this field, all of them have various
        // pros and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        //
        // RU: Объект одиночки храниться в статичном поле класса. Существует
        // несколько способов инициализировать это поле, и все они имеют разные
        // достоинства и недостатки. В этом примере мы рассмотрим простейший из
        // них, недостатком которого является полная неспособность правильно
        // работать в многопоточной среде.
        private static Singleton _instance;

        // EN: This is the static method that controls the access to the
        // singleton instance. On the first run, it creates a singleton object
        // and places it into the static field. On subsequent runs, it returns
        // the client existing object stored in the static field.
        //
        // RU: Это статический метод, управляющий доступом к экземпляру
        // одиночки. При первом запуске, он создаёт экземпляр одиночки и
        // помещает его в статическое поле. При последующих запусках, он
        // возвращает клиенту объект, хранящийся в статическом поле.
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // EN: Finally, any singleton should define some business logic, which
        // can be executed on its instance.
        //
        // RU: Наконец, любой одиночка должен содержать некоторую бизнес-логику,
        // которая может быть выполнена на его экземпляре.
        public static void someBusinessLogic()
        {
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

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
