using System;
using System.ComponentModel;
namespace PudelkoLibrary.Enums
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
}
