// EN: Mediator Design Pattern
//
// Intent: Lets you reduce chaotic dependencies between objects. The pattern
// restricts direct communications between the objects and forces them to
// collaborate only via a mediator object.
//
// RU: Паттерн Посредник
//
// Назначение: Позволяет уменьшить связанность множества классов между собой,
// благодаря перемещению этих связей в один класс-посредник.

using System;

namespace RefactoringGuru.DesignPatterns.Mediator.Conceptual
{
    // EN: The Mediator interface declares a method used by components to notify
    // the mediator about various events. The Mediator may react to these events
    // and pass the execution to other components.
    //
    // RU: Интерфейс Посредника предоставляет метод, используемый компонентами
    // для уведомления посредника о различных событиях. Посредник может
    // реагировать на эти события  и передавать исполнение другим компонентам.
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    // EN: Concrete Mediators implement cooperative behavior by coordinating
    // several components.
    //
    // RU: Конкретные Посредники реализуют совместное поведение, координируя
    // отдельные компоненты.
    class ConcreteMediator : IMediator
    {
        private Component1 _component1;

        private Component2 _component2;

        public ConcreteMediator(Component1 component1, Component2 component2)
        {
            this._component1 = component1;
            this._component1.SetMediator(this);
            this._component2 = component2;
            this._component2.SetMediator(this);
        } 

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                this._component2.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                this._component1.DoB();
                this._component2.DoC();
            }
        }
    }

    // EN: The Base Component provides the basic functionality of storing a
    // mediator's instance inside component objects.
    //
    // RU: Базовый Компонент обеспечивает базовую функциональность хранения
    // экземпляра посредника внутри объектов компонентов.
    class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    // EN: Concrete Components implement various functionality. They don't
    // depend on other components. They also don't depend on any concrete
    // mediator classes.
    //
    // RU: Конкретные Компоненты реализуют различную функциональность. Они не
    // зависят от других компонентов. Они также не зависят от каких-либо
    // конкретных классов посредников.
    class Component1 : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 1 does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 1 does B.");

            this._mediator.Notify(this, "B");
        }
    }

    class Component2 : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component 2 does C.");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component 2 does D.");

            this._mediator.Notify(this, "D");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            new ConcreteMediator(component1, component2);

            Console.WriteLine("Client triggets operation A.");
            component1.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            component2.DoD();
        }
    }
}
