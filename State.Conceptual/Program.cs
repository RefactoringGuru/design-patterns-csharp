// EN: State Design Pattern
//
// Intent: Lets an object alter its behavior when its internal state changes. It
// appears as if the object changed its class.
//
// RU: Паттерн Состояние
//
// Назначение: Позволяет объектам менять поведение в зависимости от своего
// состояния. Извне создаётся впечатление, что изменился класс объекта.

using System;

namespace RefactoringGuru.DesignPatterns.State.Conceptual
{
    // EN: The Context defines the interface of interest to clients. It also
    // maintains a reference to an instance of a State subclass, which
    // represents the current state of the Context.
    //
    // RU: Контекст определяет интерфейс, представляющий интерес для клиентов.
    // Он также хранит ссылку на экземпляр подкласса Состояния, который
    // отображает текущее состояние Контекста.
    class Context
    {
        // EN: A reference to the current state of the Context.
        //
        // RU: Ссылка на текущее состояние Контекста.
        private State _state = null;

        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // EN: The Context allows changing the State object at runtime.
        //
        // RU: Контекст позволяет изменять объект Состояния во время выполнения.
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // EN: The Context delegates part of its behavior to the current State
        // object.
        //
        // RU: Контекст делегирует часть своего поведения текущему объекту
        // Состояния.
        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }
    }
    
    // EN: The base State class declares methods that all Concrete State should
    // implement and also provides a backreference to the Context object,
    // associated with the State. This backreference can be used by States to
    // transition the Context to another State.
    //
    // RU: Базовый класс Состояния объявляет методы, которые должны реализовать
    // все Конкретные Состояния, а также предоставляет обратную ссылку на объект
    // Контекст, связанный с Состоянием. Эта обратная ссылка может
    // использоваться Состояниями для передачи Контекста другому Состоянию.
    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    // EN: Concrete States implement various behaviors, associated with a state
    // of the Context.
    //
    // RU: Конкретные Состояния реализуют различные модели поведения, связанные
    // с состоянием Контекста.
    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");
            this._context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2.");
        }
    }

    class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.Write("ConcreteStateB handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2.");
            Console.WriteLine("ConcreteStateB wants to change the state of the context.");
            this._context.TransitionTo(new ConcreteStateA());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();
        }
    }
}
