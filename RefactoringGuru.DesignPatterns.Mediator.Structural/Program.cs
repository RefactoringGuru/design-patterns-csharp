using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Mediator.Structural
{
    interface Mediator
    {
        void notify(object sender, object ev);
    }

    class ConcreteMediator : Mediator
    {
        private Component1 component1;

        private Component2 component2;

        public ConcreteMediator(Component1 component1, Component2 component2)
        {
            this.component1 = component1;
            this.component1.setMediator(this);
            this.component2 = component2;
            this.component2.setMediator(this);
        } 

        public void notify(object sender, object ev)
        {
            if(ev == "A")
            {
                Console.Write("Mediator reacts on A and triggers folowing operations:\n");
                this.component2.doC();
            }
            if(ev == "D")
            {
                Console.Write("Mediator reacts on D and triggers following operations:\n");
                this.component1.doB();
                this.component2.doC();
            }
        }
    }

    class BaseComponent
    {
        protected Mediator mediator;

        public BaseComponent(Mediator mediator = null)
        {
            this.mediator = mediator;
        }

        public void setMediator(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }

    class Component1 : BaseComponent
    {
        public void doA()
        {
            Console.Write("Component 1 does A.\n");

            this.mediator.notify(this, "A");
        }

        public void doB()
        {
            Console.Write("Component 1 does B.\n");

            this.mediator.notify(this, "B");
        }
    }

    class Component2 : BaseComponent
    {
        public void doC()
        {
            Console.Write("Component 2 does C.\n");

            this.mediator.notify(this, "C");
        }

        public void doD()
        {
            Console.Write("Component 2 does D.\n");

            this.mediator.notify(this, "D");
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
            Component1 component1 = new Component1();
            Component2 component2 = new Component2();
            Mediator mediator = new ConcreteMediator(component1, component2);

            Console.Write("Client triggets operation A.\n");
            component1.doA();

            Console.WriteLine();

            Console.Write("Client triggers operation D.\n");
            component2.doD();
        }
    }
}
