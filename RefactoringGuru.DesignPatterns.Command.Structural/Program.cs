using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Command.Structural
{
    abstract class Command
    {
        public abstract void Execute();
    }

    class SimpleCommand : Command
    {
        string _payLoad = string.Empty;

        public SimpleCommand(string payLoad)
        {
            _payLoad = payLoad;
        }

        public override void Execute()
        {
            Console.Write($"SimpleCommand: See, I can do simple things like printing ({_payLoad})\n");
        }
    }

    class ComplexCommand : Command
    {
        Receiver receiver;

        string a;

        string b;

        public ComplexCommand(Receiver r, string a, string b)
        {
            receiver = r;
            this.a = a;
            this.b = b;
        }

        public override void Execute()
        {
            Console.Write("ComplexCommand: Complex stuff should be done by a receiver object.\n");
            receiver.doSomething(a);
            receiver.doSomethingElse(b);
        }
    }

    class Receiver
    {
        public void doSomething(string a)
        {
            Console.Write("Receiver: Working on (" + a + ".)\n");
        }

        public void doSomethingElse(string b)
        {
            Console.Write("Receiver: Also working on (" + b + ".)\n");
        }
    }

    class Invoker
    {
        Command onStart;

        Command onFinish;

        public void setOnStart(Command c)
        {
            onStart = c;
        }

        public void setOnFinish(Command c)
        {
            onFinish = c;
        }

        public void doSomethingImportant()
        {
            Console.Write("Invoker: Does anybody want something done before I begin?\n");
            if (onStart is Command)
            {
                onStart.Execute();
            }
            Console.Write("Invoker: ...doing something really important...\n");
            Console.Write("Invoker: Does anybody want something done after I finish?\n");
            if (onFinish is Command)
            {
                onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Invoker invoker = new Invoker();
            invoker.setOnStart(new SimpleCommand("Say Hi!"));
            Receiver r = new Receiver();
            invoker.setOnFinish(new ComplexCommand(r, "Send email", "Save report"));

            invoker.doSomethingImportant();
        }
    }
}
