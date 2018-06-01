using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client: Executing the client code with a real subject:\n");

            RealSubject realSubject = new RealSubject();
            realSubject.Request();

            Console.WriteLine("\n");

            Console.WriteLine("Client: Executing the same client code with a proxy:\n");
            Proxy proxy = new Proxy();
            proxy.Request();
        }
    }

    abstract class Subject
    {
        public abstract void Request();
    }

    class RealSubject : Subject
    {
        public override void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }
    class Proxy : Subject
    {
        RealSubject realSubject;
        public override void Request()
        {
            if (this.checkAccess())
            {
                realSubject = new RealSubject();
                realSubject.Request();

                this.logAccess();
            }

        }

        public bool checkAccess()
        {
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }
        public void logAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }
}

