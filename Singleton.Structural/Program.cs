using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
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
            Singleton s1 = Singleton.getInstance();
            Singleton s2 = Singleton.getInstance();

            if(s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }

    class Singleton
    {
        private static Singleton instance;

        private static object obj = new object();

        private Singleton()
        { }

        public static Singleton getInstance()
        {
            lock(obj)
            {
                if (instance == null)
                    instance = new Singleton();
            }

            return instance;
        }
    }
}
