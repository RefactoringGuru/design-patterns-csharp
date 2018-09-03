using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactoringGuru.DesignPatterns.Composite.Structural
{
    abstract class Component
    {
        public Component() { }

        public abstract void Operation();

        public abstract void Add(Component c);

        public abstract void Remove(Component c);

        public abstract bool IsComposite();
    }

    class Composite : Component
    {
        List<Component> children = new List<Component>();

        public Composite() 
		{ 
		
		}

        public override void Add(Component component)
        {
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void Operation()
        {
            int i = 0;

            Console.Write("Branch(");
            foreach (Component component in children)
            {
                component.Operation();
                if(i != children.Count-1)
				{
					Console.Write("+");
				}
                i++;
            }
            Console.Write(")");
        }
    }

    class Leaf : Component
    {
        public Leaf()
        {

        }

        public override void Operation()
        {
            Console.Write("LEAF");
        }

        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public override bool IsComposite()
        {
            return false;
        }
    }
	
	class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Component leaf = new Leaf();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(leaf);
            Console.WriteLine("\n");

            Composite tree = new Composite();
            Composite branch1 = new Composite();
            branch1.Add(new Leaf());
            branch1.Add(new Leaf());
            Composite branch2 = new Composite();
            branch2.Add(new Leaf());
            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I get a composite tree:");
            client.ClientCode(tree);
            Console.WriteLine("\n");

            Console.Write("Client: I can merge two components without checking their classes:\n");
            client.ClientCode2(tree, leaf);
        }
    }

    class Client
    {
        public void ClientCode(Component leaf)
        {
            Console.Write("RESULT: ");
            leaf.Operation();
        }

        public void ClientCode2(Component component1, Component component2)
        {
            if(component1.IsComposite())
            {
                component1.Add(component2);
            }
            Console.Write("RESULT: ");
            component1.Operation();

            Console.WriteLine();
        }
    }
}
