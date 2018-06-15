using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Facade.Structural
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Subsystem1 s1 = new Subsystem1();
            Subsystem2 s2 = new Subsystem2();
            Facade facade = new Facade(s1, s2);
            client.ClientCode(facade);
        }
    }

    class Client
    {
        internal void ClientCode(Facade facade)
        {
            Console.WriteLine(facade.Operation());
        }
    }

    public class Subsystem1
    {
        public string operation1()
        {
            return "Subsystem1: Ready!\n";
        }

        public string operationN()
        {
            return "Subsystem1: Go!\n";
        }
    }
	
    public class Subsystem2
    {
        public string operation1()
        {
            return "Subsystem2: Get ready!\n";
        }

        public string operationZ()
        {
            return "Subsystem2: Fire!\n";
        }
    }

    public class Facade
    {
        Subsystem1 Subsystem1;
		
        Subsystem2 Subsystem2;

        public Facade(Subsystem1 s1, Subsystem2 s2)
        {
            this.Subsystem1 = s1;
            this.Subsystem2 = s2;
        }
		
        public string Operation()
        {
            string result = "Facade initializes subsystems:\n";
            result += Subsystem1.operation1();
            result += Subsystem2.operation1();
            result += "Facade orders subsystems to perform the action:\n";
            result += Subsystem1.operationN();
            result += Subsystem2.operationZ();
            return result;
        }
    }
}
