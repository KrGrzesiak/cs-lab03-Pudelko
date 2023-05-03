using System;
using PudelkoLib;
using Extensions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoApp
{
    class Program
    {
        private static int ComparePudelko(Pudelko p1, Pudelko p2)
        {
            int value = p1.Objetosc.CompareTo(p2.Objetosc);
            
            if(value == 0)
            {
                value = p1.Pole.CompareTo(p2.Pole);
                if (value == 0)
                {
                    return (p1.A + p1.B + p1.C).CompareTo(p2.A + p2.B + p2.C);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        static void Main(string[] args)
        {
            List<Pudelko> listaPudelek = new List<Pudelko>()
            {
                new Pudelko(),
                new Pudelko(1.5),
                new Pudelko(0.45, 0.2),
                new Pudelko(0.37, 0.37, 0.37, UnitOfMeasure.meter),
                new Pudelko(25.5, 10, 15.2, UnitOfMeasure.centimeter),
                new Pudelko(255, 100, 153, UnitOfMeasure.milimeter),
                new Pudelko(10.0, 5.5, 3.8, UnitOfMeasure.meter)
            };

            Comparison<Pudelko> pudelkoCompare = new Comparison<Pudelko>(ComparePudelko);
            listaPudelek.Sort(pudelkoCompare);

            // AFTER
            foreach (var x in listaPudelek)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("\n=================================================================");

            Pudelko pud = new Pudelko(1.5, 0.5, 0.7, UnitOfMeasure.meter);

            // ToString -->
            Console.WriteLine("\n\nToString -->");
            Console.WriteLine(pud.ToString());

            // Objetosc -->
            Console.WriteLine("\nObjetosc -->");
            Console.WriteLine(pud.Objetosc);

            // Pole -->
            Console.WriteLine("\nPole -->");
            Console.WriteLine(pud.Pole);

            // Equals -->
            Console.WriteLine("\nEquals -->");
            Pudelko pud2 = new Pudelko(1.5, 0.5, 0.7, UnitOfMeasure.meter);
            Console.WriteLine($"Equal:\t\t{ pud.Equals(pud2)}");

            // Operators !=, ==, + -->
            Console.WriteLine("\nOperators -->");
            Console.WriteLine($"!=\t\t{ pud != pud2 }");
            Console.WriteLine($"==\t\t{ pud == pud2 }");

            Console.WriteLine("+:");
            Console.WriteLine(pud.ToString());
            Console.WriteLine(pud2.ToString());
            Pudelko pud3 = pud + pud2;
            Console.WriteLine("Product:");
            Console.WriteLine(pud3.ToString());

            // Conversion -->
            Console.WriteLine("\nConversion -->");
            // Explicit
            double[] tab1 = (double[])pud;

            for(int i = 0; i < tab1.Length; i++)
            { Console.Write(tab1[i] + " "); }

            // Implicit
            ValueTuple<int, int, int> tab2 = (10000, 5000, 3000);
            Pudelko pud4 = tab2;

            Console.WriteLine("\n" + pud4.ToString());

            // Indexer & Iterator -->
            Console.WriteLine("\nIndexer & Iterator -->");
            // Indexer
            Console.WriteLine($"Indexer\t\t{ pud[1] }");
            // Iterator
            Console.Write("Iterator\t");
            foreach (var x in pud)
            {
                Console.Write(x);
                Console.Write(" ");
            }

            // Parse -->
            Console.WriteLine("\n\nParse -->");
            string str = "2.500 m × 9.321 m × 0.100 m";
            Console.WriteLine(Pudelko.Parse(str));

            // Kompresuj
            Console.WriteLine("\nKompresuj -->");
            Pudelko pud5 = Extension.Kompresuj(pud);
            Console.WriteLine(pud5.ToString());
        }
    }
}

