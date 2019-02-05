using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Prototype.Structural
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.ClientCode();
        }
    }

    class Client
    {
        public void ClientCode()
        {
            Prototype prototype = new Prototype();
            prototype.Primitive = 245;
            prototype.Component = new DateTime();
            prototype.CircularReference = new ComponentWithBackReference(prototype);

            Prototype clone = prototype.Clone();

            if(prototype.Primitive == clone.Primitive)
            {
                Console.Write("Primitive field values have been carried over to a clone. Yay!\n");
            }
            else
            {
                Console.Write("Primitive field values have not been copied. Booo!\n");
            }

            if (prototype.Component != clone.Component)
            {
                Console.Write("Simple component has been cloned. Yay!\n");
            }
            else
            {
                Console.Write("Simple component has not been cloned. Booo!\n");
            }

            if (prototype.CircularReference != clone.CircularReference)
            {
                Console.Write("Component with back reference has been cloned. Yay!\n");
            }
            else
            {
                Console.Write("Component with back reference has not been cloned. Booo!\n");
            }

            if (clone.CircularReference.Prototype != prototype)
            {
                Console.Write("Component with back reference is not linked to original object. Yay!\n");
            }
            else
            {
                Console.Write("Component with back reference is linked to original object. Booo!\n");
            }
        }
    }

    public class Prototype
    {
        public int Primitive { get; set; }

        public DateTime Component { get; set; }

        public ComponentWithBackReference CircularReference { get; set; }

        public Prototype()
        { }
        
        public Prototype Clone()
        {
            Prototype clone = this.MemberwiseClone() as Prototype;

            clone.Component = this.Component.MemberwiseClone() as DateTime;

            clone.CircularReference = this.CircularReference.MemberwiseClone() as ComponentWithBackReference;
            clone.CircularReference.Prototype = this;

            return clone;
        }
    }

    public class ComponentWithBackReference
    {
        public Prototype Prototype { get; set; }

        public ComponentWithBackReference(Prototype p)
        {
            Prototype = p;
        }
    }
}
