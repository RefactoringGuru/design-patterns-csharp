// EN: Visitor Design Pattern
//
// Intent: Lets you separate algorithms from the objects on which they operate.
//
// RU: Паттерн Посетитель
//
// Назначение: Позволяет создавать новые операции, не меняя классы объектов, над
// которыми эти операции могут выполняться.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Visitor.Conceptual
{
    // EN: The Component interface declares an `accept` method that should take
    // the base visitor interface as an argument.
    //
    // RU: Интерфейс Компонента объявляет метод accept, который в качестве
    // аргумента может получать любой объект, реализующий интерфейс посетителя.
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

    // EN: Each Concrete Component must implement the `Accept` method in such a
    // way that it calls the visitor's method corresponding to the component's
    // class.
    //
    // RU: Каждый Конкретный Компонент должен реализовать метод Accept таким
    // образом, чтобы он вызывал метод посетителя, соответствующий классу
    // компонента.
    public class ConcreteComponentA : IComponent
    {
        // EN: Note that we're calling `VisitConcreteComponentA`, which matches
        // the current class name. This way we let the visitor know the class of
        // the component it works with.
        //
        // RU: Обратите внимание, мы вызываем VisitConcreteComponentA, что
        // соответствует названию текущего класса. Таким образом мы позволяем
        // посетителю узнать, с каким классом компонента он работает.
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentA(this);
        }

        // EN: Concrete Components may have special methods that don't exist in
        // their base class or interface. The Visitor is still able to use these
        // methods since it's aware of the component's concrete class.
        //
        // RU: Конкретные Компоненты могут иметь особые методы, не объявленные в
        // их базовом классе или интерфейсе. Посетитель всё же может
        // использовать эти методы, поскольку он знает о конкретном классе
        // компонента.
        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class ConcreteComponentB : IComponent
    {
        // EN: Same here: VisitConcreteComponentB => ConcreteComponentB
        //
        // RU: То же самое здесь: VisitConcreteComponentB => ConcreteComponentB
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentB(this);
        }

        public string SpecialMethodOfConcreteComponentB()
        {
            return "B";
        }
    }

    // EN: The Visitor Interface declares a set of visiting methods that
    // correspond to component classes. The signature of a visiting method
    // allows the visitor to identify the exact class of the component that it's
    // dealing with.
    //
    // RU: Интерфейс Посетителя объявляет набор методов посещения,
    // соответствующих классам компонентов. Сигнатура метода посещения позволяет
    // посетителю определить конкретный класс компонента, с которым он имеет
    // дело.
    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);

        void VisitConcreteComponentB(ConcreteComponentB element);
    }

    // EN: Concrete Visitors implement several versions of the same algorithm,
    // which can work with all concrete component classes.
    //
    // You can experience the biggest benefit of the Visitor pattern when using
    // it with a complex object structure, such as a Composite tree. In this
    // case, it might be helpful to store some intermediate state of the
    // algorithm while executing visitor's methods over various objects of the
    // structure.
    //
    // RU: Конкретные Посетители реализуют несколько версий одного и того же
    // алгоритма, которые могут работать со всеми классами конкретных
    // компонентов.
    //
    // Максимальную выгоду от паттерна Посетитель вы почувствуете, используя его
    // со сложной структурой объектов, такой как дерево Компоновщика. В этом
    // случае было бы полезно хранить некоторое промежуточное состояние
    // алгоритма при выполнении методов посетителя над различными объектами
    // структуры.
    class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class Client
    {
        // EN: The client code can run visitor operations over any set of
        // elements without figuring out their concrete classes. The accept
        // operation directs a call to the appropriate operation in the visitor
        // object.
        //
        // RU: Клиентский код может выполнять операции посетителя над любым
        // набором элементов, не выясняя их конкретных классов. Операция
        // принятия направляет вызов к соответствующей операции в объекте
        // посетителя.
        public static void ClientCode(List<IComponent> components, IVisitor visitor)
        {
            foreach (var component in components)
            {
                component.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<IComponent> components = new List<IComponent>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            Client.ClientCode(components,visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            Client.ClientCode(components, visitor2);
        }
    }
}
