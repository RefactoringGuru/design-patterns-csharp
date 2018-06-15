using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Bridge.Structural
{
    abstract class Abstraction
    {
        protected Implementor implementor;
		
        public Implementor Implementor
        {
            set { implementor = value; }
        }
		
        public Abstraction(Implementor imp)
        {
            implementor = imp;
        }
		
        public virtual string Operation()
        {
            return "Abstract: Base operation with:\n" +  implementor.operationImplementation();
        }
    }

    abstract class Implementor
    {
        public abstract string operationImplementation();
    }

    class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(Implementor imp) : base(imp)
        { 
		}
		
        public override string Operation()
        {
            return "ExtendedAbstraction: Extended operation with:\n" + base.implementor.operationImplementation();
        }
    }

    class ConcreteImplementorA : Implementor
    {
        public override string operationImplementation()
        {
            return "ConcreteImplementationA: The result in platform A.\n";
        }
    }

    class ConcreteImplementorB : Implementor
    {
        public override string operationImplementation()
        {
            return "ConcreteImplementationA: The result in platform B.\n";
        }
    }
	
	class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Abstraction abstraction;
            abstraction = new ExtendedAbstraction(new ConcreteImplementorA());
            client.ClientCode(abstraction);
            abstraction.Implementor = new ConcreteImplementorB();
            client.ClientCode(abstraction);
        }
    }

    class Client
    {
        public void ClientCode(Abstraction abstraction)
        {
            Console.WriteLine(abstraction.Operation());
        }
    }
}
