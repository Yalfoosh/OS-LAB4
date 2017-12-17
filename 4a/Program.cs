using System;
using System.Collections.Generic;

namespace LAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong MaxID = 0;    //MaxID prati koliko ima dretvi. Teoretski je moguće koristiti Guid za dretve, ali treba nam jednostavan ispis :)

            int ThreadNum = int.Parse(args[0]);

            List<Dretva> Dretve = new List<Dretva>(ThreadNum);
            Random RNG = new Random();

            for (int i = 0; i < ThreadNum; ++i)
                Dretve.Add(new Dretva(ref MaxID, (ulong)RNG.Next(0, ThreadNum), (ulong)RNG.Next(1, 10), (ulong)RNG.Next(0, ThreadNum)));

            new Opisnik(Dretve);    //Stvaranjem novog opisnika, pokreće se simulacija.

            Console.Read();     //Ekvivalenta getchar() u C-u.
        }
    }
}