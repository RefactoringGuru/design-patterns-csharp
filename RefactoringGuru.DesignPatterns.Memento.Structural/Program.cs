using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Memento.Structural
{
    class Originator
    {
        string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.Write("Originator: My initial state is: " + _state + "\n");
        }

        public void doSomething()
        {
            Console.Write("Originator: I'm doing something important.\n");
            this._state = this.generateRandomString(30);
            Console.Write($"Originator: and my state has changed to: {_state}\n");
        }

        private string generateRandomString(int length = 10)
        {
            string allowedSymbs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string result = string.Empty;

            while(length > 0)
            {
                result += allowedSymbs[new Random().Next(0, allowedSymbs.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        public Memento save()
        {
            return new ConcreteMemento(this._state);
        }

        public void restore(Memento memento)
        {
            if(!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.getState();
            Console.Write($"Originator: My state has changed to: {_state}");
        }
    }

    interface Memento
    {
        string getName();

        string getState();

        DateTime getDate();
    }

    class ConcreteMemento : Memento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        public string getState()
        {
            return this._state;
        }

        public string getName()
        {
            return this._date + " / (" + _state.Substring(0, 9) + "...)";
        }

        public DateTime getDate()
        {
            return this._date;
        }
    }

    class Caretaker
    {
        private List<Memento> _mementos = new List<Memento>();

        private Originator originator = null;

        public Caretaker(Originator originator)
        {
            this.originator = originator;
        }

        public void backup()
        {
            Console.Write("\nCaretaker: Saving Originator's state...\n");
            this._mementos.Add(this.originator.save());
        }

        public void undo()
        {
            if (_mementos.Count == 0)
            {
                return;
            }

            var memento = _mementos.Last();
            _mementos.Remove(memento);

            Console.Write("Caretaker: Restoring state to: " + memento.getName() + "\n");

            try
            {
                this.originator.restore(memento);
            }
            catch(Exception ex)
            {
                this.undo();
            }
        }

        public void showHistory()
        {
            Console.Write("Caretaker: Here's the list of mementos:\n");

            foreach (var memento in _mementos)
            {
                Console.Write(memento.getName() + "\n");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client.ClientCode();
        }
    }

    class Client
    {
        public static void ClientCode()
        {
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.backup();
            originator.doSomething();

            caretaker.backup();
            originator.doSomething();

            caretaker.backup();
            originator.doSomething();

            Console.WriteLine();
            caretaker.showHistory();

            Console.Write("\nClient: Now, let's rollback!\n\n");
            caretaker.undo();

            Console.Write("\n\nClient: Once more!\n\n");
            caretaker.undo();

            Console.WriteLine();
        }
    }
}
