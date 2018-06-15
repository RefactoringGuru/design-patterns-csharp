using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.State.Structural
{
    class Context
    {
        private State _state = null;

        public Context(State state)
        {
            this.transitionTo(state);
        }

        public void transitionTo(State state)
        {
            Console.Write("Context: Transition to " + state.ToString() + ".\n");
            this._state = state;
            this._state.setContext(this);
        }

        public void request1()
        {
            this._state.handle1();
        }

        public void request2()
        {
            this._state.handle2();
        }
    }
    
    abstract class State
    {
        protected Context context;

        public void setContext(Context context)
        {
            this.context = context;
        }

        public abstract void handle1();

        public abstract void handle2();
    }

    class ConcreteStateA : State
    {
        public override void handle1()
        {
            Console.Write("ConcreteStateA handles request1.\n");
            Console.Write("ConcreteStateA wants to change the state of the context.\n");
            this.context.transitionTo(new ConcreteStateB());
        }

        public override void handle2()
        {
            Console.Write("ConcreteStateA handles request2.\n");
        }
    }

    class ConcreteStateB : State
    {
        public override void handle1()
        {
            Console.Write("ConcreteStateB handles request1.\n");
        }

        public override void handle2()
        {
            Console.Write("ConcreteStateB handles request2.\n");
            Console.Write("ConcreteStateB wants to change the state of the context.\n");
            this.context.transitionTo(new ConcreteStateA());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client.ClientCode();
        }
    }

    class Client
    {
        public static void ClientCode()
        {
            var context = new Context(new ConcreteStateA());
            context.request1();
            context.request2();
        }
    }
}
