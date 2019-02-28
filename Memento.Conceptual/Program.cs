// EN: Memento Design Pattern
//
// Intent: Lets you save and restore the previous state of an object without
// revealing the details of its implementation.
//
// RU: Паттерн Снимок
//
// Назначение: Позволяет делать снимки состояния объектов, не раскрывая
// подробностей их реализации. Затем снимки можно использовать, чтобы
// восстановить прошлое состояние объектов.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Memento.Conceptual
{
    // EN: The Originator holds some important state that may change over time.
    // It also defines a method for saving the state inside a memento and
    // another method for restoring the state from it.
    //
    // RU: Создатель содержит некоторое важное состояние, которое может со
    // временем меняться. Он также объявляет метод сохранения состояния внутри
    // снимка и метод восстановления состояния из него.
    class Originator
    {
        // EN: For the sake of simplicity, the originator's state is stored
        // inside a single variable.
        //
        // RU: Для удобства состояние создателя хранится внутри одной
        // переменной.
        private string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.WriteLine("Originator: My initial state is: " + state);
        }

        // EN: The Originator's business logic may affect its internal state.
        // Therefore, the client should backup the state before launching
        // methods of the business logic via the save() method.
        //
        // RU: Бизнес-логика Создателя может повлиять на его внутреннее
        // состояние. Поэтому клиент должен выполнить резервное копирование
        // состояния с помощью метода save перед запуском методов бизнес-логики.
        public void DoSomething()
        {
            Console.WriteLine("Originator: I'm doing something important.");
            this._state = this.GenerateRandomString(30);
            Console.WriteLine($"Originator: and my state has changed to: {_state}");
        }

        private string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        // EN: Saves the current state inside a memento.
        //
        // RU: Сохраняет текущее состояние внутри снимка.
        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // EN: Restores the Originator's state from a memento object.
        //
        // RU: Восстанавливает состояние Создателя из объекта снимка.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.Write($"Originator: My state has changed to: {_state}");
        }
    }

    // EN: The Memento interface provides a way to retrieve the memento's
    // metadata, such as creation date or name. However, it doesn't expose the
    // Originator's state.
    //
    // RU: Интерфейс Снимка предоставляет способ извлечения метаданных снимка,
    // таких как дата создания или название. Однако он не раскрывает состояние
    // Создателя.
    public interface IMemento
    {
        string GetName();

        string GetState();

        DateTime GetDate();
    }

    // EN: The Concrete Memento contains the infrastructure for storing the
    // Originator's state.
    //
    // RU: Конкретный снимок содержит инфраструктуру для хранения состояния
    // Создателя.
    class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // EN: The Originator uses this method when restoring its state.
        //
        // RU: Создатель использует этот метод, когда восстанавливает своё
        // состояние.
        public string GetState()
        {
            return this._state;
        }
        
        // EN: The rest of the methods are used by the Caretaker to display
        // metadata.
        //
        // RU: Остальные методы используются Опекуном для отображения
        // метаданных.
        public string GetName()
        {
            return $"{this._date} / ({this._state.Substring(0, 9)})...";
        }

        public DateTime GetDate()
        {
            return this._date;
        }
    }

    // EN: The Caretaker doesn't depend on the Concrete Memento class.
    // Therefore, it doesn't have access to the originator's state, stored
    // inside the memento. It works with all mementos via the base Memento
    // interface.
    //
    // RU: Опекун не зависит от класса Конкретного Снимка. Таким образом, он не
    // имеет доступа к состоянию создателя, хранящемуся внутри снимка. Он
    // работает со всеми снимками через базовый интерфейс Снимка.
    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Here's the list of mementos:");

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // EN: Client code.
            //
            // RU: Клиентский код.
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            caretaker.Backup();
            originator.DoSomething();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nClient: Now, let's rollback!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nClient: Once more!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}
