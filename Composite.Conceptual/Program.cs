// EN: Composite Design Pattern
//
// Intent: Lets you compose objects into tree structures and then work with
// these structures as if they were individual objects.
//
// RU: Паттерн Компоновщик
//
// Назначение: Позволяет сгруппировать объекты в древовидную структуру, а затем
// работать с ними так, как будто это единичный объект.

using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Composite.Conceptual
{
    // EN: The base Component class declares common operations for both simple
    // and complex objects of a composition.
    //
    // RU: Базовый класс Компонент объявляет общие операции как для простых, так
    // и для сложных объектов структуры.
    abstract class Component
    {
        public Component() { }

        // EN: The base Component may implement some default behavior or leave
        // it to concrete classes (by declaring the method containing the
        // behavior as "abstract").
        //
        // RU: Базовый Компонент может сам реализовать некоторое поведение по
        // умолчанию или поручить это конкретным классам, объявив метод,
        // содержащий поведение абстрактным.
        public abstract string Operation();
    }

    // EN: The Leaf class represents the end objects of a composition. A leaf
    // can't have any children.
    //
    // Usually, it's the Leaf objects that do the actual work, whereas Composite
    // objects only delegate to their sub-components.
    //
    // RU: Класс Лист представляет собой конечные объекты структуры. Лист не
    // может иметь вложенных компонентов.
    //
    // Обычно объекты Листьев выполняют фактическую работу, тогда как объекты
    // Контейнера лишь делегируют работу своим подкомпонентам.
    class Leaf : Component
    {
        public override string Operation()
        {
            return "Leaf";
        }
    }

    // EN: The Composite class represents the complex components that may have
    // children. Usually, the Composite objects delegate the actual work to
    // their children and then "sum-up" the result.
    //
    // RU: Класс Контейнер содержит сложные компоненты, которые могут иметь
    // вложенные компоненты. Обычно объекты Контейнеры делегируют фактическую
    // работу своим детям, а затем «суммируют» результат.
    class Composite : Component
    {
        protected List<Component> _children = new List<Component>();
        
        public void Add(Component component)
        {
            this._children.Add(component);
        }

        public void Remove(Component component)
        {
            this._children.Remove(component);
        }

        // EN: The Composite executes its primary logic in a particular way. It
        // traverses recursively through all its children, collecting and
        // summing their results. Since the composite's children pass these
        // calls to their children and so forth, the whole object tree is
        // traversed as a result.
        //
        // RU: Контейнер выполняет свою основную логику особым образом. Он
        // проходит рекурсивно через всех своих детей, собирая и суммируя их
        // результаты. Поскольку потомки контейнера передают эти вызовы своим
        // потомкам и так далее,  в результате обходится всё дерево объектов.
        public override string Operation()
        {
            int i = 0;
            string result = "Branch(";

            foreach (Component component in this._children)
            {
                result += component.Operation();
                if (i != this._children.Count - 1)
                {
                    result += "+";
                }
                i++;
            }
            
            return result + ")";
        }
    }

    class Client
    {
        // EN: The client code works with all of the components via the base
        // interface.
        //
        // RU: Клиентский код работает со всеми компонентами через базовый
        // интерфейс.
        public void ClientCode(Component component)
        {
            Console.WriteLine($"RESULT: {component.Operation()}\n");
        }
        
        public void ClientCode2(Composite composite, Component component)
        {
            composite.Add(component);
            Console.WriteLine($"RESULT: {composite.Operation()}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            // EN: This way the client code can support the simple leaf
            // components...
            //
            // RU: Таким образом, клиентский код может поддерживать простые
            // компоненты-листья...
            Leaf leaf = new Leaf();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(leaf);

            // EN: ...as well as the complex composites.
            //
            // RU: ...а также сложные контейнеры.
            Composite tree = new Composite();
            Composite branch1 = new Composite();
            branch1.Add(new Leaf());
            branch1.Add(new Leaf());
            Composite branch2 = new Composite();
            branch2.Add(new Leaf());
            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I've got a composite tree:");
            client.ClientCode(tree);

            Console.Write("Client: I don't need to check the components classes even when managing the tree:\n");
            client.ClientCode2(tree, leaf);
        }
    }
}
