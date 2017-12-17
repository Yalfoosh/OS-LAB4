using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LAB4;

namespace LAB4
{
    public class Opisnik
    {
        public Dretva Aktivna;

        public List<Dretva> ZaDodati;
        public List<Dretva> Pripravne;

        //Parametar s kojim simuliramo protok vremena.
        private ulong t;

        public Opisnik(List<Dretva> zaDodati)
        {
            Aktivna = null;

            ZaDodati = zaDodati.OrderBy(x => x.Birthtime).ToList();
            ZaDodati.TrimExcess();

            Pripravne = new List<Dretva>(ZaDodati.Count);

            t = 0;

            Console.WriteLine("Dretve: ");
            foreach (Dretva d in ZaDodati)
                Console.WriteLine(d);

            Console.Write("\n\n");

            Tick();
        }

        /// <summary>
        /// Metoda koja dodaje parametar u red pripravnih, silazno sortirano po prioritetima.
        /// </summary>
        /// <param name="dretva">Dretva koju dodajemo u red pripravnih</param>
        private void DodajSortirano(Dretva dretva)
        {
            if (Pripravne.Count == 0 || Pripravne[Pripravne.Count - 1].Priority > dretva.Priority)
                Pripravne.Add(dretva);
            else
                for (int i = 0; i < Pripravne.Count; ++i)
                    if (dretva.Priority > Pripravne[i].Priority)
                    {
                        Pripravne.Insert(i, dretva);
                        break;
                    }
        }

        /// <summary>
        /// Metoda koja služi za dodavanje novih dretvi u opisnik.
        /// Nakon stavljanja aktivne u red pripravnih te inicijalne u red pripravnih (silazno sortirano po prioritetima),
        /// prva dretva iz reda pripravnih stavlja se u red aktivnih.
        /// </summary>
        /// <param name="dretva">Dretva za koju simuliramo pokretanje</param>
        public void Dodaj(Dretva dretva)
        {
            Console.WriteLine("Dodajem dretvu " + dretva);

            if (Aktivna != null)
                DodajSortirano(Aktivna);

            DodajSortirano(dretva);

            PripToAkt();
        }

        /// <summary>
        /// Metoda koja stavlja prvu dretvu iz reda pripravnih u aktivnu.
        /// </summary>
        private void PripToAkt()
        {
            Aktivna = Pripravne[0].DeepCopy();
            Pripravne.RemoveAt(0);
        }

        /// <summary>
        /// Rekurzivna metoda koja oponaša rad dretvi. Rad je opisan liniju po liniju.
        /// </summary>
        public void Tick()
        {
            if (ZaDodati.Count > 0 || Pripravne.Count > 0 || Aktivna != null)    //Uvjet da dretve obavljaju posao.
            {
                if (Aktivna != null && Aktivna.Tick())   //Provjeri je li trenutna dretva umrla.
                    Aktivna = null;

                if (Aktivna != null && Aktivna?.Type == TType.RoundRobin     //U slučaju RR dretve, a da čekaju dretve većeg ili jednakog prioriteta u redu, Aktivnu stavljamo u Pripravne.
                    && Pripravne.Count > 0 && Pripravne[0].Priority >= Aktivna?.Priority)
                {
                    DodajSortirano(Aktivna.DeepCopy());
                    Aktivna = null;
                }

                if (ZaDodati.Count > 0 && ZaDodati[0].Birthtime == t)   //Na kraju u red dodajemo dretve koje se rađaju.
                {
                    int counter = 0;

                    for (int i = 0; i < ZaDodati.Count && ZaDodati[i].Birthtime == t; ++i, ++counter)
                        Dodaj(ZaDodati[i].DeepCopy());  //Dodaj je drukčija od DodajSortirano jer se koristi samo za prvo dodavanje dretvi.

                    for (int i = 0; i < counter; ++i)
                        ZaDodati.RemoveAt(0);
                }

                if (Aktivna == null && Pripravne.Count > 0)
                    PripToAkt();

                Console.WriteLine(this);
                Thread.Sleep(1000);
                ++t;

                Tick();
            }
            else
                Console.Write("Kraj rada.");
        }

        /// <summary>
        /// ToString Opisnika vraća Vrijeme, Aktivnu dretvu, te ispis svih pripravnih dretvi.
        /// </summary>
        public override string ToString()
        {
            string toRet = "Vrijeme: " + t + "\nAktivna: " + Aktivna + "\nPripravne:\n";

            foreach (Dretva d in Pripravne)
                toRet += d + "\n";

            return toRet;
        }
    }
}