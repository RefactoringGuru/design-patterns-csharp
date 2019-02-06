using System;
using System.Collections.Generic;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Observer.Conceptual
{
    public interface IObserver
    {
        void update(ISubject subject);
    }

    public interface ISubject
    {
        void attach(IObserver observer);

        void detach(IObserver observer);

        void notify();
    }

    public class Subject : ISubject
    {
        public int State { get; set; } = -0;

        private List<IObserver> _observers = new List<IObserver>();

        public void attach(IObserver observer)
        {
            Console.Write("Subject: Attached an observer.\n");
            this._observers.Add(observer);
        }

        public void detach(IObserver observer)
        {
            foreach (var elem in _observers)
            {
                if (elem == observer)
                {
                    _observers.Remove(observer);
                    Console.Write("Subject: Detached an observer.\n");
                    break;
                }
            }
        }

        public void notify()
        {
            Console.Write("Subject: Notifying observers...\n");

            foreach (var observer in _observers)
            {
                observer.update(this);
            }
        }

        public void someBusinessLogic()
        {
            Console.Write("\nSubject: I'm doing something important.\n");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.Write("Subject: My state has just changed to: " + this.State + "\n");
            this.notify();
        }
    }

    class ConcreteObserverA : IObserver
    {
        public void update(ISubject subject)
        {
            if (!(subject is Subject))
            {
                return;
            }
            
            if ((subject as Subject).State < 3)
            {
                Console.Write("ConcreteObserverA: Reacted to the event.\n");
            }
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void update(ISubject subject)
        {
            if (!(subject is Subject))
            {
                return;
            }

            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                Console.Write("ConcreteObserverB: Reacted to the event.\n");
            }
        }
    }

    class Client
    {
        public static void ClientCode()
        {
            var subj = new Subject();
            var o1 = new ConcreteObserverA();
            subj.attach(o1);

            var o2 = new ConcreteObserverB();
            subj.attach(o2);

            subj.someBusinessLogic();
            subj.someBusinessLogic();

            subj.detach(o2);

            subj.someBusinessLogic();
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
