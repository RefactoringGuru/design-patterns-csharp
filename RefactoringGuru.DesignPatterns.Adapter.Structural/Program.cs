using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Adapter.Structural
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Console.Write("Client: I can work just fine with the Target objects:\n");
            Target targ = new Target();
            client.ClientCode(targ);
            Console.WriteLine();

            Adaptee adapt = new Adaptee();
            Console.Write("Client: The Adaptee class has a weird interface. See, I don't understand it:\n");
            Console.Write(adapt.SpecificRequest());
            Console.Write("\n\n");

            Console.Write("Client: But I can work with it via the Adapter:\n");
            Adapter adapter = new Adapter(adapt);
            client.ClientCode(adapter);
        }
    }

    class Client
    {
        public void ClientCode(Target target)
        {
            Console.WriteLine(target.Request());
        }
    }
    
    // класс, к которому надо адаптировать другой класс   
    public class Target
    {
        public virtual string Request()
        {
            return "Target: The default target's behavior.";
        }
    }

    // Адаптер
    class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();

        public Adapter(Adaptee adaptee)
        {
            this.adaptee = adaptee;
        }

        public override string Request()
        {
            return "Adapter: (TRANSLATED) " + new string(adaptee.SpecificRequest().Reverse().ToArray());
        }
    }

    // Адаптируемый класс
    public class Adaptee
    {
        public string SpecificRequest()
        {
            return ".eetpadA eht fo roivaheb laicepS";
        }
    }
}
