using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace karacsonyCLI
{
    internal class Program
    {
       

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("diszek.txt");
            List<NapiMunka> list = new List<NapiMunka>();

            while (!sr.EndOfStream)
            {
                NapiMunka nm = new NapiMunka(sr.ReadLine());
                list.Add(nm);
            }

            sr.Close();

            //4.Feladat

            Console.WriteLine("4.Feladat:Összesen "+NapiMunka.KeszultDb+" darab dísz készült.");

            Console.WriteLine();
            //5.Feladat

            bool yes = false;
            foreach (var item in list)
            {
                if (item.HarangKesz==0 && item.AngyalkaKesz==0 && item.FenyofaKesz==0 )
                {
                    yes = true;
                }
            }

            if (yes==true)
            {
                Console.WriteLine("5.Feladat:Volt olyan nap,amikor egyetlen dísz sem készült.");
            }
            else
            {
                Console.WriteLine("5.Feladat:Nem volt olyan nap,mikor egyetlen dísz sem készült.");
            }

            Console.WriteLine();
            //6.Feladat
            Console.WriteLine("6.Feladat:");
            Console.Write("Adja meg a keresett napot [1...40]:");
            int beirt = Convert.ToInt32(Console.ReadLine());

            while (beirt>40 || beirt<0)
            {
                Console.Write("Adja meg a keresett napot [1...40]:");
                beirt = Convert.ToInt32(Console.ReadLine());
            }

            foreach (var item in list)
            {
                if (beirt==item.Nap)
                {
                    Console.WriteLine("A(z) "+item.Nap+" nap végén "+item.HarangKesz+" harang "+item.AngyalkaKesz+" angyalka és "+item.FenyofaKesz+" fenyőfa maradt készleten.");
                }
                
            }
            Console.WriteLine();
            //7.Feladat

            int fenyo = 0;
            int angyal = 0;
            int harang = 0;

            foreach (var item in list)
            {
                fenyo += item.FenyofaEladott*-1;
                angyal += item.AngyalkaEladott*-1;
                harang += item.HarangEladott * -1;
            }

            Dictionary<string, int> osszesitett = new Dictionary<string,int>();
            osszesitett.Add("Angyal", fenyo);
            osszesitett.Add("Harang", harang);
            osszesitett.Add("Fenyőfa", fenyo);


            int telj = 0;
            List<string> legtobb = new List<string>();
            foreach (var item in osszesitett)
            {
                 telj = osszesitett.Values.Max();
               
                if (item.Value==osszesitett.Values.Max())
                {
                    legtobb.Add(item.Key);
                }

                
            }
            Console.WriteLine("7.Feladat: Legtöbbet eladott dísz: " + telj);
            foreach (var item in legtobb)
            {
                Console.WriteLine(item);
            }



            //8.Feladat:

            StreamWriter sw = new StreamWriter("bevetel.txt");

            foreach (var item in list)
            {
                
                if (item.NapiBevetel()>=10000)
                {
                    sw.WriteLine(item.Nap + " napon "+item.NapiBevetel()+" Forint volt a bevétel" );
                }
               // Console.WriteLine(item.NapiBevetel());
            }


            sw.Close();

            Console.ReadKey();
        }



        class NapiMunka
        {
            public static int KeszultDb { get; private set; }
            public int Nap { get; private set; }
            public int HarangKesz { get; private set; }
            public int HarangEladott { get; private set; }
            public int AngyalkaKesz { get; private set; }
            public int AngyalkaEladott { get; private set; }
            public int FenyofaKesz { get; private set; }
            public int FenyofaEladott { get; private set; }

            public NapiMunka(string sor)
            {
               
                
                string[] s = sor.Split(';');
                Nap = Convert.ToInt32(s[0]);
                HarangKesz = Convert.ToInt32(s[1]);
                HarangEladott = Convert.ToInt32(s[2]);
                AngyalkaKesz = Convert.ToInt32(s[3]);
                AngyalkaEladott = Convert.ToInt32(s[4]);
                FenyofaKesz = Convert.ToInt32(s[5]);
                FenyofaEladott = Convert.ToInt32(s[6]);
                
                NapiMunka.KeszultDb += HarangKesz + AngyalkaKesz + FenyofaKesz;

              
            }

            public int NapiBevetel()
            {
                return -(HarangEladott * 1000 + AngyalkaEladott * 1350 + FenyofaEladott * 1500);
            }
        }
    }
}
