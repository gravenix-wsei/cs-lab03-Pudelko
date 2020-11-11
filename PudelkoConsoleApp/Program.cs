using System;
using System.Collections.Generic;
using PudelkoLibrary;
using PudelkoLibrary.Enums;
using cs_lab03_Pudelko.Extensions;

namespace cs_lab03_Pudelko
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> pudelka = new List<Pudelko>();
            pudelka.AddRange(new Pudelko[]{
                new Pudelko(6.03, 2, 2.012),
                new Pudelko(6.03, 2, 2.012).Kompresuj(),
                new Pudelko(1, 1.5, 5),
                new Pudelko(3, 2.5, 1),
                new Pudelko(312.2, 2.5, 101.2, UnitOfMeasure.centimeter),
                new Pudelko(312, 25, 1705, UnitOfMeasure.milimeter)
            });
            pudelka.Sort(SortPudelko);
            foreach (Pudelko p in pudelka)
            {
                Console.WriteLine(p.ToString());
            }
        }

        private static int SortPudelko(Pudelko p1, Pudelko p2)
        {
            if (p1.Objetosc == p2.Objetosc)
            {
                if (p1.Pole == p2.Pole)
                {
                    return (int)((p1.A + p1.B + p1.C) - (p2.A + p2.B + p2.C)*1000);
                }
                return p1.Pole < p2.Pole ? -1 : 1;
            }
            return p1.Objetosc < p2.Objetosc ? -1 : 1;
        }
    }
}
