using System;
using System.Collections.Generic;
using LAB4;

namespace _4b
{
    class Visekriterijsko
    {
        static void Main(string[] args)
        {
            ulong MaxID = 0;

            int ThreadNum = int.Parse(args[0]);

            var Dretve = new List<Dretva>(ThreadNum);
            var RNG = new Random();

            for (int i = 0; i < ThreadNum; ++i)
                Dretve.Add(new Dretva(ref MaxID, (ulong)RNG.Next(0, ThreadNum), (ulong)RNG.Next(1, 10), (ulong)RNG.Next(0, ThreadNum), (TType)RNG.Next(1, 3)));

            new Opisnik(Dretve);

            Console.Read();
        }
    }
}