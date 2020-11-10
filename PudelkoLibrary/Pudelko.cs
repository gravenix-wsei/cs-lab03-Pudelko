using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace PudelkoLibrary
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        private UnitOfMeasure unit;
        public double A;
        public double B;
        public double C;

        public Pudelko(double a = 0, double b = 0, double c = 0, UnitOfMeasure unit = UnitOfMeasure.milimeter)
        {
            A = a;
            B = b;
            C = c;
            this.unit = unit;
        }

        public static explicit operator Pudelko(double[] p) => new Pudelko(p[0], p[1], p[2]);
        public static explicit operator double[](Pudelko p) => new double[] { p.A, p.B, p.C };
        public static implicit operator ValueTuple<double, double, double>(Pudelko p) => (p.A, p.B, p.C);
        public static implicit operator Pudelko(ValueTuple<double, double, double> p) =>
            new Pudelko(p.Item1, p.Item2, p.Item3);

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new IndexOutOfRangeException("For Pudelko, you can use only 0-2 indexes!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        A = value;
                        break;
                    case 1:
                        B = value;
                        break;
                    case 2:
                        C = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("For Pudelko, you can use only 0-2 indexes!");
                }
            }
        }

        public override string ToString()
        {
            return ToString("A B C");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            string measure = unit.ToString();
            return format.Replace("A", A.ToString() + measure)
                .Replace("B", B.ToString() + measure)
                .Replace("C", C.ToString() + measure);
        }

        public bool Equals(Pudelko other)
        {
            return A == other.A && B == other.B && C == other.C && unit == other.unit;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new PudelkoEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class PudelkoEnumerator : IEnumerator<double>
    {
        private int index = 0;
        private Pudelko pudelko;

        public PudelkoEnumerator(Pudelko p)
        {
            pudelko = p;
        }

        public object Current => pudelko[index];
        double IEnumerator<double>.Current => pudelko[index];


        public bool MoveNext()
        {
            return index++ < 2;
        }

        public void Reset()
        {
            index = 0;
        }

        public void Dispose()
        {
        }
    }
}
