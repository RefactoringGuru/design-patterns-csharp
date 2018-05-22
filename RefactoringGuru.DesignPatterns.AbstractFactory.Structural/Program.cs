using System;

namespace RefactoringGuru.DesignPatterns.AbstractFactory.Structural
{

    interface AbstractFactory
    {
        AbstractProductA createProductA();

        AbstractProductB createProductB();
    }

    class ConcreteFactory1 : AbstractFactory
    {
        public AbstractProductA createProductA()
        {
            return new ConcreteProductA1();
        }

        public AbstractProductB createProductB()
        {
            return new ConcreteProductB1();
        }
    }

    class ConcreteFactory2 : AbstractFactory
    {
        public AbstractProductA createProductA()
        {
            return new ConcreteProductA2();
        }

        public AbstractProductB createProductB()
        {
            return new ConcreteProductB2();
        }
    }


    interface AbstractProductA
    {
        string usefulFunctionA();
    }

    class ConcreteProductA1 : AbstractProductA
    {
        public string usefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    class ConcreteProductA2 : AbstractProductA
    {
        public string usefulFunctionA()
        {
            return "The result of the product A2.";
        }
    }


    interface AbstractProductB
    {
        string usefulFunctionB();

        string anotherUsefulFunctionB(AbstractProductA collaborator);
    }

    class ConcreteProductB1 : AbstractProductB
    {
        public string usefulFunctionB()
        {
            return "The result of the product B1.\n";
        }

        public string anotherUsefulFunctionB(AbstractProductA collaborator)
        {
            string result = collaborator.usefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    class ConcreteProductB2 : AbstractProductB
    {
        public string usefulFunctionB()
        {
            return "The result of the product B2.\n";
        }

        public string anotherUsefulFunctionB(AbstractProductA collaborator)
        {
            string result = collaborator.usefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }


    class Client
    {
        public void main()
        {
            Console.Write("Client: Testing client code with the first factory type:\n");
            clientMethod(new ConcreteFactory1());
            Console.Write("\n\n");

            Console.Write("Client: Testing the same client code with the second factory type:\n");
            clientMethod(new ConcreteFactory2());
        }

        public void clientMethod(AbstractFactory factory)
        {
            AbstractProductA product_a = factory.createProductA();
            AbstractProductB product_b = factory.createProductB();

            Console.Write(product_b.usefulFunctionB());
            Console.Write(product_b.anotherUsefulFunctionB(product_a));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            (new Client()).main();
        }
    }
}
