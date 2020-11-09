using System;

namespace PudelkoLibrary
{
    public class Pudelko
    {
        private UnitOfMeasure unit;
        public double A;
        public double B;
        public double C;

        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.milimeter)
        {
            A = a;
            B = b;
            C = c;
            this.unit = unit;
        }

        public Pudelko() : this(0, 0, 0)
        {
        }
    }
}
