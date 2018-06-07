using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Structural
{
    interface IRoundForm
    {
        double GetRadius();
    }

    class RoundHole
    {
        private readonly double _radius;

        public RoundHole(double radius)
        {
            _radius = radius;
        }

        public double GetRadius()
        {
            return _radius;
        }

        public bool Fits(IRoundForm form)
        {
            return _radius >= form.GetRadius();
        }
    }

    class RoundPeg : IRoundForm
    {
        private readonly double _radius;

        public RoundPeg(double radius)
        {
            _radius = radius;
        }

        public double GetRadius()
        {
            return _radius;
        }
    }

    class SquarePeg
    {
        private readonly double _width;

        public SquarePeg(double width)
        {
            _width = width;
        }

        public double GetWidth()
        {
            return _width;
        }

        public double GetSquare()
        {
            return Math.Pow(_width, 2);
        }
    }

    class SquarePegAdapter : IRoundForm
    {
        private readonly SquarePeg _peg;

        public SquarePegAdapter(SquarePeg peg)
        {
            _peg = peg;
        }

        public double GetRadius()
        {
            return Math.Sqrt(Math.Pow(_peg.GetWidth() / 2, 2) * 2);
        }
    }

    class Client
    {
        public void Main()
        {
            RoundHole hole = new RoundHole(5);
            RoundPeg rpeg = new RoundPeg(5);

            if (hole.Fits(rpeg))
            {
                Console.WriteLine("Round peg r5 fits round hole r5.");
            }

            SquarePeg smallSqPeg = new SquarePeg(2);
            SquarePeg largeSqPeg = new SquarePeg(20);
            // hole.fits(smallSqPeg); // not compiled

            SquarePegAdapter smallSqPegAdapter = new SquarePegAdapter(smallSqPeg);
            SquarePegAdapter largeSqPegAdapter = new SquarePegAdapter(largeSqPeg);

            if (hole.Fits(smallSqPegAdapter))
            {
                Console.WriteLine("Square peg w2 fits round hole r5.");
            }

            if (!hole.Fits(largeSqPegAdapter))
            {
                Console.WriteLine("Square peg w20 does not fit into round hole r5.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            new Client().Main();
        }
    }
}
