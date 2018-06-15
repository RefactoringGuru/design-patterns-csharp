using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Strategy.Structural
{
    class Context
    {
        private Strategy _strategy;

        public Context()
        { }

        public Context(Strategy strategy)
        {
            this._strategy = strategy;
        }

        public void setStrategy(Strategy strategy)
        {
            this._strategy = strategy;
        }

        public void doSomeBusinessLogic()
        {
            Console.Write("Context: Sorting data using the strategy (not sure how it'll do it)\n");
            var result = this._strategy.doAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string result_str = string.Empty;
            foreach(var element in result as List<string>)
            {
                result_str += element + ",";
            }

            Console.Write(result_str);
        }
    }

    interface Strategy
    {
        object doAlgorithm(object data);
    }

    class ConcreteStrategyA : Strategy
    {
        public object doAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    class ConcreteStrategyB : Strategy
    {
        public object doAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    class Client
    {
        public static void ClientCode()
        {
            var context = new Context();

            Console.Write("Client: Strategy is set to normal sorting.\n");
            context.setStrategy(new ConcreteStrategyA());
            context.doSomeBusinessLogic();
            Console.Write("\n");
            Console.Write("Client: Strategy is set to reverse sorting.\n");
            context.setStrategy(new ConcreteStrategyB());
            context.doSomeBusinessLogic();

            Console.Write("\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client.ClientCode();
        }
    }
}
