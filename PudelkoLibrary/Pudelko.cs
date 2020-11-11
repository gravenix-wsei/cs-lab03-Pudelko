using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using PudelkoLibrary.Enums;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        private UnitOfMeasure unit { get; }
        private double a, b, c;
        public double A { get => Convert.ToDouble(a.ToString("0.000")); private set { a = value; } }
        public double B { get => Convert.ToDouble(b.ToString("0.000")); private set { b = value; } }
        public double C { get => Convert.ToDouble(c.ToString("0.000")); private set { c = value; } }

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
            A = UnitOfMeasureHelper.Convert(a ?? defaultVal, unit, UnitOfMeasure.meter);
            B = UnitOfMeasureHelper.Convert(b ?? defaultVal, unit, UnitOfMeasure.meter);
            C = UnitOfMeasureHelper.Convert(c ?? defaultVal, unit, UnitOfMeasure.meter);
            this.unit = UnitOfMeasure.meter;
            ValidateFields();
        }

        public double Objetosc { get => Math.Round(A * B * C, 9); }
        public double Pole { get => Math.Round(2 * A * B + 2 * A * C + 2 * B * C, 6); }

        public static explicit operator double[](Pudelko p) => new double[] { p.A, p.B, p.C };
        public static implicit operator Pudelko(ValueTuple<double, double, double> p) =>
            new Pudelko(p.Item1, p.Item2, p.Item3, UnitOfMeasure.milimeter);

        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string format)
        {
            if (format == null) format = "m";
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            try
            {
                UnitOfMeasure measure = UnitOfMeasureHelper.GetUnitFromFriendlyName(format);
                string a = UnitOfMeasureHelper.DimensionToString(UnitOfMeasureHelper.Convert(A, unit, measure), measure),
                       b = UnitOfMeasureHelper.DimensionToString(UnitOfMeasureHelper.Convert(B, unit, measure), measure),
                       c = UnitOfMeasureHelper.DimensionToString(UnitOfMeasureHelper.Convert(C, unit, measure), measure);
                return $"{a} {format} \u00D7 {b} {format} \u00D7 {c} {format}";
            }
            catch (ArgumentException e)
            {
                throw new FormatException(e.Message);
            }
        }

        public bool Equals(Pudelko other) => Pole == other.Pole && Objetosc == other.Objetosc;
        public static bool operator==(Pudelko p1, Pudelko p2) => p1.Equals(p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !p1.Equals(p2);

        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + unit.GetHashCode();
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
    }

    public class PudelkoEnumerator : IEnumerator<double>
    {
        private int index = 0;
        private readonly Pudelko pudelko;

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
