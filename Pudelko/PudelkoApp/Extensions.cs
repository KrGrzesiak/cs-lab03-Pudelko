using System;
using PudelkoLib;

namespace Extensions
{
    public static class Extension
    {
        public static Pudelko Kompresuj(Pudelko pud)
        {
            double volume = pud.Objetosc;

            double edges = Math.Cbrt(volume);

            return new Pudelko(edges, edges, edges);
        }
    }
}