using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Visitor.Structural
{
    interface Component
    {
        void accept(Visitor visitor);
    }

    public class ConcreteComponentA : Component
    {
        public void accept(Visitor visitor)
        {
            visitor.visitConcreteComponentA(this);
        }

        public string exclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class ConcreteComponentB : Component
    {
        public void accept(Visitor visitor)
        {
            visitor.visitConcreteComponentB(this);
        }

        public string specialMethodOfConcreteComponentB()
        {
            return "B";
        }
    }

    public interface Visitor
    {
        void visitConcreteComponentA(ConcreteComponentA el);

        void visitConcreteComponentB(ConcreteComponentB el);
    }

    class ConcreteVisitor1 : Visitor
    {
        public void visitConcreteComponentA(ConcreteComponentA el)
        {
            Console.Write(el.exclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1\n");
        }

        public void visitConcreteComponentB(ConcreteComponentB el)
        {
            Console.Write(el.specialMethodOfConcreteComponentB() + " + ConcreteVisitor1\n");
        }
    }

    class ConcreteVisitor2 : Visitor
    {
        public void visitConcreteComponentA(ConcreteComponentA el)
        {
            Console.Write(el.exclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2\n");
        }

        public void visitConcreteComponentB(ConcreteComponentB el)
        {
            Console.Write(el.specialMethodOfConcreteComponentB() + " + ConcreteVisitor2\n");
        }
    }

    internal class Client
    {
        internal static void ClientCode(List<Component> components, Visitor visitor)
        {
            foreach(var component in components)
            {
                component.accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Component> components = new List<Component>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.Write("The client code works with all visitors via the base Visitor interface:\n");
            var visitor1 = new ConcreteVisitor1();
            Client.ClientCode(components,visitor1);

            Console.WriteLine();

            Console.Write("It allows the same client code to work with different types of visitors:\n");
            var visitor2 = new ConcreteVisitor2();
            Client.ClientCode(components, visitor2);
        }
    }
}
