// EN: Observer Design Pattern
//
// Intent: Lets you define a subscription mechanism to notify multiple objects
// about any events that happen to the object they're observing.
//
// Note that there's a lot of different terms with similar meaning associated
// with this pattern. Just remember that the Subject is also called the
// Publisher and the Observer is often called the Subscriber and vice versa.
// Also the verbs "observe", "listen" or "track" usually mean the same thing.
//
// RU: Паттерн Наблюдатель
//
// Назначение: Создаёт механизм подписки, позволяющий одним объектам следить и
// реагировать на события, происходящие в других объектах.
//
// Обратите внимание, что существует множество различных терминов с похожими
// значениями, связанных с этим паттерном. Просто помните, что Субъекта также
// называют Издателем, а Наблюдателя часто называют Подписчиком и наоборот.
// Также глаголы «наблюдать», «слушать» или «отслеживать» обычно означают одно и
// то же.

using System;
using System.Collections.Generic;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Observer.Conceptual
{
    public interface IObserver
    {
        // EN: Receive update from subject
        //
        // RU: Получает обновление от издателя
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        // EN: Attach an observer to the subject.
        //
        // RU: Присоединяет наблюдателя к издателю.
        void Attach(IObserver observer);

        // EN: Detach an observer from the subject.
        //
        // RU: Отсоединяет наблюдателя от издателя.
        void Detach(IObserver observer);

        // EN: Notify all observers about an event.
        //
        // RU: Уведомляет всех наблюдателей о событии.
        void Notify();
    }

    // EN: The Subject owns some important state and notifies observers when the
    // state changes.
    //
    // RU: Издатель владеет некоторым важным состоянием и оповещает наблюдателей
    // о его изменениях.
    public class Subject : ISubject
    {
        // EN: For the sake of simplicity, the Subject's state, essential to all
        // subscribers, is stored in this variable.
        //
        // RU: Для удобства в этой переменной хранится состояние Издателя,
        // необходимое всем подписчикам.
        public int State { get; set; } = -0;

        // EN: List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        //
        // RU: Список подписчиков. В реальной жизни список подписчиков может
        // храниться в более подробном виде (классифицируется по типу события и
        // т.д.)
        private List<IObserver> _observers = new List<IObserver>();

        // EN: The subscription management methods.
        //
        // RU: Методы управления подпиской.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // EN: Trigger an update in each subscriber.
        //
        // RU: Запуск обновления в каждом подписчике.
        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        // EN: Usually, the subscription logic is only a fraction of what a
        // Subject can really do. Subjects commonly hold some important business
        // logic, that triggers a notification method whenever something
        // important is about to happen (or after it).
        //
        // RU: Обычно логика подписки – только часть того, что делает Издатель.
        // Издатели часто содержат некоторую важную бизнес-логику, которая
        // запускает метод уведомления всякий раз, когда должно произойти что-то
        // важное (или после этого).
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    // EN: Concrete Observers react to the updates issued by the Subject they
    // had been attached to.
    //
    // RU: Конкретные Наблюдатели реагируют на обновления, выпущенные Издателем,
    // к которому они прикреплены.
    class ConcreteObserverA : IObserver
    {
        public void Update(ISubject subject)
        {            
            if ((subject as Subject).State < 3)
            {
                Console.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                Console.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // EN: The client code.
            //
            // RU: Клиентский код.
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
}
