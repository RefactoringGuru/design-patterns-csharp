// EN: Prototype Design Pattern
//
// Intent: Lets you copy existing objects without making your code dependent on
// their classes.
//
// RU: Паттерн Прототип
//
// Назначение: Позволяет копировать объекты, не вдаваясь в подробности их
// реализации.

using System;

namespace RefactoringGuru.DesignPatterns.Prototype.Conceptual
{
    public class Person
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Person ShallowCopy()
        {
            return (Person) this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person) this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.IdInfo = new IdInfo(666);

            // EN: Perform a shallow copy of p1 and assign it to p2.
            //
            // RU: Выполнить поверхностное копирование p1 и присвоить её p2.
            Person p2 = p1.ShallowCopy();
            // EN: Make a deep copy of p1 and assign it to p3.
            //
            // RU: Сделать глубокую копию p1 и присвоить её p3.
            Person p3 = p1.DeepCopy();

            // EN: Display values of p1, p2 and p3.
            //
            // RU: Вывести значения p1, p2 и p3.
            Console.WriteLine("Original values of p1, p2, p3:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);

            // EN: Change the value of p1 properties and display the values of
            // p1, p2 and p3.
            //
            // RU: Изменить значение свойств p1 и отобразить значения p1, p2 и
            // p3.
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values (reference values have changed):");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayValues(p3);
        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.Name, p.Age, p.BirthDate);
            Console.WriteLine("      ID#: {0:d}", p.IdInfo.IdNumber);
        }
    }
}
