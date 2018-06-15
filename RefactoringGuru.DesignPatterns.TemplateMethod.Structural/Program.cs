using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.TemplateMethod.Structural
{
    abstract class AbstractClass
    {
        public void templateMethod()
        {
            this.baseOperation1();
            this.requiredOperations1();
            this.baseOperation2();
            this.hook1();
            this.requiredOperation2();
            this.baseOperation3();
            this.hook2();
        }

        protected void baseOperation1()
        {
            Console.Write("AbstractClass says: I am doing the bulk of the work\n");
        }

        protected void baseOperation2()
        {
            Console.Write("AbstractClass says: But I let subclasses override some operations\n");
        }

        protected void baseOperation3()
        {
            Console.Write("AbstractClass says: But I am doing the bulk of the work anyway\n");
        }

        protected abstract void requiredOperations1();

        protected abstract void requiredOperation2();

        protected virtual void hook1() { }

        protected virtual void hook2() { }
    }

    class ConcreteClass1 : AbstractClass
    {
        protected override void requiredOperations1()
        {
            Console.Write("ConcreteClass1 says: Implemented Operation1\n");
        }

        protected override void requiredOperation2()
        {
            Console.Write("ConcreteClass1 says: Implemented Operation2\n");
        }
    }

    class ConcreteClass2 : AbstractClass
    {
        protected override void requiredOperations1()
        {
            Console.Write("ConcreteClass2 says: Implemented Operation1\n");
        }

        protected override void requiredOperation2()
        {
            Console.Write("ConcreteClass2 says: Implemented Operation2\n");
        }

        protected override void hook1()
        {
            Console.Write("ConcreteClass2 says: Overridden Hook1\n");
        }
    }

    class Client
    {
        public static void ClientCode(AbstractClass ac)
        {
            ac.templateMethod();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Same client code can work with different subclasses:\n");

            Client.ClientCode(new ConcreteClass1());

            Console.Write("\n");
            Console.Write("Same client code can work with different subclasses:\n");
            Client.ClientCode(new ConcreteClass2());
        }
    }
}
