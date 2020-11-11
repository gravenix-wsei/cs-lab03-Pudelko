using System;
using PudelkoLibrary;
namespace cs_lab03_Pudelko.Extensions
{
    public static class PudelkoExtensionMethods
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            double size = Math.Cbrt(p.Objetosc);
            return new Pudelko(size, size, size);
        }
    }
}
