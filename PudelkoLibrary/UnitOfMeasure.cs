using System;
using System.ComponentModel;
namespace PudelkoLibrary
{
    public enum UnitOfMeasure
    {
        [Description("mm")]
        milimeter=1000,
        [Description("cm")]
        centimeter=100,
        [Description("m")]
        meter=1
    }
    public class EnumHelper
    {
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
    }
}
