using System;

namespace PudelkoLibrary.Enums
{

    public class UnitOfMeasureHelper
    {
        /// <summary>
        /// Converts value from one unit to other
        /// </summary>
        /// <param name="val">Value to be converted</param>
        /// <param name="from">Unit of measure from which to convert</param>
        /// <param name="to">Unit of measure to which val will be converted</param>
        /// <returns>Converted Value</returns>
        public static double Convert(double val, UnitOfMeasure from, UnitOfMeasure to)
        {
            double diff = (double)to / (double)from;
            val *= diff;
            return ToUnitPrecision(val, to);
        }

        /// <summary>
        /// Converts value to unit's precision
        /// </summary>
        /// <param name="val">value</param>
        /// <param name="m">unit</param>
        /// <returns>converted value</returns>
        public static double ToUnitPrecision(double val, UnitOfMeasure m)
        {
            int precision = UnitOfMeasureHelper.GetPrecisionForUnit(m);
            return Math.Floor(val * Math.Pow(10, precision)) / Math.Pow(10, precision);
        }

        /// <summary>
        /// Converts unit of measure to string with precision specified to its unit
        /// </summary>
        /// <param name="val">value to be converted</param>
        /// <param name="unit">unit, in which is the value</param>
        /// <returns>string with specified precision</returns>
        public static string DimensionToString(double val, UnitOfMeasure unit)
        {
            int precision = GetPrecisionForUnit(unit);
            string format = "0." + new string('0', precision);
            return precision > 0 ? val.ToString(format) : ((int)val).ToString();
        }

        public static string GetUnitFriendlyName(UnitOfMeasure m)
        {
            switch (m)
            {
                case UnitOfMeasure.milimeter:
                    return "mm";
                case UnitOfMeasure.centimeter:
                    return "cm";
                case UnitOfMeasure.meter:
                    return "m";
                default:
                    throw new ArgumentException("Not defined unit");
            }
        }


        public static UnitOfMeasure GetUnitFromFriendlyName(string m)
        {
            switch (m)
            {
                case "mm":
                    return UnitOfMeasure.milimeter;
                case "cm":
                    return UnitOfMeasure.centimeter;
                case "m":
                    return UnitOfMeasure.meter;
                default:
                    throw new ArgumentException("Not defined unit");
            }
        }

        private const int DECIMAL_PLACES = 4;

        /// <summary>
        /// Gets precision for unit
        /// </summary>
        /// <param name="m">unit</param>
        /// <returns>decimal places</returns>
        public static int GetPrecisionForUnit(UnitOfMeasure m) => DECIMAL_PLACES - ((int)m).ToString().Length;
    }
}
