// EN: Template Method Design Pattern
//
// Intent: Defines the skeleton of an algorithm in the superclass but lets
// subclasses override specific steps of the algorithm without changing its
// structure.
//
// RU: Паттерн Шаблонный метод
//
// Назначение: Определяет скелет алгоритма, перекладывая ответственность за
// некоторые его шаги на подклассы. Паттерн позволяет подклассам переопределять
// шаги алгоритма, не меняя его общей структуры.

using System;

namespace RefactoringGuru.DesignPatterns.TemplateMethod.Conceptual
{
    // EN: The Abstract Class defines a template method that contains a skeleton
    // of some algorithm, composed of calls to (usually) abstract primitive
    // operations.
    //
    // Concrete subclasses should implement these operations, but leave the
    // template method itself intact.
    //
    // RU: Абстрактный Класс определяет шаблонный метод, содержащий скелет
    // некоторого алгоритма, состоящего из вызовов (обычно) абстрактных
    // примитивных операций.
    //
    // Конкретные подклассы должны реализовать эти операции, но оставить сам
    // шаблонный метод без изменений.
    abstract class AbstractClass
    {
        // EN: The template method defines the skeleton of an algorithm.
        //
        // RU: Шаблонный метод определяет скелет алгоритма.
        public void TemplateMethod()
        {
            this.BaseOperation1();
            this.RequiredOperations1();
            this.BaseOperation2();
            this.Hook1();
            this.RequiredOperation2();
            this.BaseOperation3();
            this.Hook2();
        }

        // EN: These operations already have implementations.
        //
        // RU: Эти операции уже имеют реализации.
        protected void BaseOperation1()
        {
            Console.WriteLine("AbstractClass says: I am doing the bulk of the work");
        }

        protected void BaseOperation2()
        {
            Console.WriteLine("AbstractClass says: But I let subclasses override some operations");
        }

        protected void BaseOperation3()
        {
            Console.WriteLine("AbstractClass says: But I am doing the bulk of the work anyway");
        }
        
        // EN: These operations have to be implemented in subclasses.
        //
        // RU: А эти операции должны быть реализованы в подклассах.
        protected abstract void RequiredOperations1();

        protected abstract void RequiredOperation2();
        
        // EN: These are "hooks." Subclasses may override them, but it's not
        // mandatory since the hooks already have default (but empty)
        // implementation. Hooks provide additional extension points in some
        // crucial places of the algorithm.
        //
        // RU: Это «хуки». Подклассы могут переопределять их, но это не
        // обязательно, поскольку у хуков уже есть стандартная (но пустая)
        // реализация. Хуки предоставляют дополнительные точки расширения в
        // некоторых критических местах алгоритма.
        protected virtual void Hook1() { }

        protected virtual void Hook2() { }
    }

    // EN: Concrete classes have to implement all abstract operations of the
    // base class. They can also override some operations with a default
    // implementation.
    //
    // RU: Конкретные классы должны реализовать все абстрактные операции
    // базового класса. Они также могут переопределить некоторые операции с
    // реализацией по умолчанию.
    class ConcreteClass1 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation2");
        }
    }

    // EN: Usually, concrete classes override only a fraction of base class'
    // operations.
    //
    // RU: Обычно конкретные классы переопределяют только часть операций
    // базового класса.
    class ConcreteClass2 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation1");
        }

        protected override void RequiredOperation2()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation2");
        }

        protected override void Hook1()
        {
            Console.WriteLine("ConcreteClass2 says: Overridden Hook1");
        }
    }

    class Client
    {
        // EN: The client code calls the template method to execute the
        // algorithm. Client code does not have to know the concrete class of an
        // object it works with, as long as it works with objects through the
        // interface of their base class.
        //
        // RU: Клиентский код вызывает шаблонный метод для выполнения алгоритма.
        // Клиентский код не должен знать конкретный класс объекта, с которым
        // работает, при условии, что он работает с объектами через интерфейс их
        // базового класса.
        public static void ClientCode(AbstractClass abstractClass)
        {
            // ...
            abstractClass.TemplateMethod();
            // ...
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Same client code can work with different subclasses:");

            Client.ClientCode(new ConcreteClass1());

            Console.Write("\n");
            
            Console.WriteLine("Same client code can work with different subclasses:");
            Client.ClientCode(new ConcreteClass2());
        }
    }
}
