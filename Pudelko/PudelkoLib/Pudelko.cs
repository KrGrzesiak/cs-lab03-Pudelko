using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PudelkoLib
{
    public enum UnitOfMeasure { meter, centimeter, milimeter }

    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        private readonly double a = 0.1;
        private readonly double b = 0.1;
        private readonly double c = 0.1;

        public double A { get => Math.Truncate(a * 1000) / 1000; }
        public double B { get => Math.Truncate(b * 1000) / 1000; }
        public double C { get => Math.Truncate(c * 1000) / 1000; }

        private UnitOfMeasure unit { get; } 
            = UnitOfMeasure.meter;

        // CONSTRUCTORS
        public Pudelko()
        {
            a = 0.1;
            b = 0.1;
            c = 0.1;
            unit = UnitOfMeasure.meter;
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if (a == null || !a.HasValue) { a = ConvertToUnit(10, UnitOfMeasure.centimeter, unit); }
            if (b == null || !b.HasValue) { b = ConvertToUnit(10, UnitOfMeasure.centimeter, unit); }
            if (c == null || !c.HasValue) { c = ConvertToUnit(10, UnitOfMeasure.centimeter, unit); }

            double aInMeters = ConvertToUnit(a.Value, unit, UnitOfMeasure.meter);
            double bInMeters = ConvertToUnit(b.Value, unit, UnitOfMeasure.meter);
            double cInMeters = ConvertToUnit(c.Value, unit, UnitOfMeasure.meter);

            if ((aInMeters > 10 || aInMeters <= 0.001) ||
                (bInMeters > 10 || bInMeters <= 0.001) ||
                (cInMeters > 10 || cInMeters <= 0.001))
                throw new ArgumentOutOfRangeException();

            this.a = aInMeters;
            this.b = bInMeters;
            this.c = cInMeters;
            this.unit = unit;
        }

        private double ConvertToUnit(double value, UnitOfMeasure fromUnit, UnitOfMeasure toUnit)
        {
            if (fromUnit == toUnit) return value;

            switch (fromUnit)
            {
                case UnitOfMeasure.milimeter:
                    value /= 1000;
                    break;
                case UnitOfMeasure.centimeter:
                    value /= 100;
                    break;
                case UnitOfMeasure.meter:
                    break;
                default:
                    throw new ArgumentException();
            }

            switch (toUnit)
            {
                case UnitOfMeasure.milimeter:
                    value *= 1000;
                    break;
                case UnitOfMeasure.centimeter:
                    value *= 100;
                    break;
                case UnitOfMeasure.meter:
                    break;
                default:
                    throw new ArgumentException();
            }

            return value;
        }


        // ToString
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
            if (String.IsNullOrEmpty(format)) { format = "m"; }
            if (formatProvider == null) { formatProvider = CultureInfo.CurrentCulture; }

            switch(format)
            {
                case "m":
                    return  $"{ConvertToUnit(a, unit, UnitOfMeasure.meter).ToString("0.000", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(b, unit, UnitOfMeasure.meter).ToString("0.000", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(c, unit, UnitOfMeasure.meter).ToString("0.000", formatProvider)} {format}";
                case "cm":
                    return  $"{ConvertToUnit(a, unit, UnitOfMeasure.centimeter).ToString("0.0", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(b, unit, UnitOfMeasure.centimeter).ToString("0.0", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(c, unit, UnitOfMeasure.centimeter).ToString("0.0", formatProvider)} {format}";
                case "mm":
                    return  $"{ConvertToUnit(a, unit, UnitOfMeasure.milimeter).ToString("0", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(b, unit, UnitOfMeasure.milimeter).ToString("0", formatProvider)} {format} \x00D7 " +
                            $"{ConvertToUnit(c, unit, UnitOfMeasure.milimeter).ToString("0", formatProvider)} {format}";
                default:
                    throw new FormatException();
            }
        }


        // Properties 'Objetosc'
        public double Objetosc
        {
            get => Math.Round(A * B * C, 9);
        }

        // Properties 'Pole'
        public double Pole
        {
            get => Math.Round((A * B) * 2 + (A * C) * 2 + (B * C) * 2, 6);
        }


        // Equals
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(this.GetType().Equals(obj.GetType())))
            {
                return false;
            }
            else
            {
                Pudelko pud = (Pudelko)obj;
                return this.Equals(pud);
            }
        }

        public bool Equals(Pudelko other)
        {
            if (ReferenceEquals(other, null)) { return false; }
            if (this.GetHashCode() != other.GetHashCode()) { return false; }

            double[] temp_tab_p1 = { A, B, C };
            double[] temp_tab_p2 = { other.A, other.B, other.C };

            Array.Sort(temp_tab_p1);
            Array.Sort(temp_tab_p2);

            return temp_tab_p1.SequenceEqual(temp_tab_p2);
        }

        // GetHashCode()
        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode();

        // Operator ==
        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null)) { return true; }
            if ((ReferenceEquals(p1, null) && !ReferenceEquals(p2, null)) || (ReferenceEquals(p2, null) && !(ReferenceEquals(p1, null)))) { return false; }
            return p1.Equals(p2);
        }

        // Operator !=
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);


        // Operator +
        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double[] temp_tab_p1 = { p1.A, p1.B, p1.C };
            double[] temp_tab_p2 = { p2.A, p2.B, p2.C };

            Array.Sort(temp_tab_p1);
            Array.Sort(temp_tab_p2);

            Pudelko pud = new Pudelko(temp_tab_p1[0] + temp_tab_p2[0], temp_tab_p1[1] + temp_tab_p2[1], temp_tab_p1[2] + temp_tab_p2[2]);
            return pud;
        }


        // Conversion
        public static explicit operator double[](Pudelko pud)
        {
            double[] tab = new double[3];

            tab[0] = pud.A;
            tab[1] = pud.B;
            tab[2] = pud.C;

            return tab;
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> tab)
        {
            Pudelko pud = new Pudelko(tab.Item1, tab.Item2, tab.Item3, UnitOfMeasure.milimeter);

            return pud;
        }


        // Indexer
        public double this[int i]
        {
            get
            {
                if (i == 0) { return A; }
                else if (i == 1) { return B; }
                else if (i == 2) { return C; }
                else { throw new IndexOutOfRangeException(); }
            }
        }

        // Iterator
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        // Parse
        public static Pudelko Parse(string str)
        {
            string[] elements = str.Split(new[] { ' ', '\x00D7' }, StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != 6) { throw new FormatException(); }

            double temp_A;
            double temp_B;
            double temp_C;

            if (!double.TryParse(elements[0], NumberStyles.Float, CultureInfo.InvariantCulture, out temp_A) ||
                !double.TryParse(elements[2], NumberStyles.Float, CultureInfo.InvariantCulture, out temp_B) ||
                !double.TryParse(elements[4], NumberStyles.Float, CultureInfo.InvariantCulture, out temp_C))
            {
                throw new FormatException();
            }

            UnitOfMeasure temp_unit;

            switch (elements[1])
            {
                case "m":
                    temp_unit = UnitOfMeasure.meter;
                    break;
                case "cm":
                    temp_unit = UnitOfMeasure.centimeter;
                    break;
                case "mm":
                    temp_unit = UnitOfMeasure.milimeter;
                    break;
                default:
                    throw new FormatException();
            }

            return new Pudelko(temp_A, temp_B, temp_C, temp_unit);
        }
    }
}