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

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double defaultVal = 0.1;
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    defaultVal = 100;
                    break;
                case UnitOfMeasure.centimeter:
                    defaultVal = 10;
                    break;
            }
            A = a ?? defaultVal;
            B = b ?? defaultVal;
            C = c ?? defaultVal;
            this.unit = unit;
            ConvertToMeters();
            ValidateFields();
        }

        public double Objetosc { get => A * B * C; }
        public double Pole { get => 2 * A * B + 2 * A * C + 2 * B * C; }

        public static explicit operator Pudelko(double[] p) => new Pudelko(p[0], p[1], p[2]);
        public static explicit operator double[](Pudelko p) => new double[] { p.A, p.B, p.C };
        public static implicit operator ValueTuple<double, double, double>(Pudelko p) => (p.A, p.B, p.C);
        public static implicit operator Pudelko(ValueTuple<double, double, double> p) =>
            new Pudelko(p.Item1, p.Item2, p.Item3, UnitOfMeasure.milimeter);

        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            UnitOfMeasure measure;
            try
            {
                measure = EnumHelper.GetUnitFromFriendlyName(format);
                return $"{A} {format} x {B} {format} x {C} {format}";
            }
            catch (ArgumentException e)
            {
                throw new FormatException(e.Message);
            }
        }

        public bool Equals(Pudelko other)
        {
            return A == other.A && B == other.B && C == other.C && unit == other.unit;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new PudelkoEnumerator(this);
        }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateFields()
        {
            if (A <= 0 || B <= 0 || C <= 0)
            {
                throw new ArgumentOutOfRangeException("Dimensions of Pudelko can't be less than 0!");
            }
            if (unit == UnitOfMeasure.meter && (A >= 10 || B >= 10 || C >= 10))
            {
                throw new ArgumentOutOfRangeException("Pudelko can't be larger than 10 meters!");
            }
        }

        private const int PRECISION = 3;

        private void ConvertToMeters()
        {
            int divideBy=1;
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    divideBy = 1000;
                    break;
                case UnitOfMeasure.centimeter:
                    divideBy = 100;
                    break;
            }
            A = FixPrecision(A / divideBy, PRECISION);
            B = FixPrecision(B / divideBy, PRECISION);
            C = FixPrecision(C / divideBy, PRECISION);
            unit = UnitOfMeasure.meter;
        }

        private double FixPrecision(double d, int precision) => Math.Floor(d * Math.Pow(10, precision)) / Math.Pow(10, precision);
    }

    public class PudelkoEnumerator : IEnumerator<double>
    {
        private int index = 0;
        private Pudelko pudelko;

        public PudelkoEnumerator(Pudelko p)
        {
            pudelko = p;
        }

        public object Current => pudelko[index++];
        double IEnumerator<double>.Current => pudelko[index++];


        public bool MoveNext()
        {
            return index < 2;
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
