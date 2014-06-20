using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Et075.Model;

namespace Et075.Tests
{
    class AnalizerTests
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Et075");
            //Analyze(new Zakaz(1500, 2600, 8000, 10000, 1500, 4800));
            //Analyze(new Zakaz(2000, 2000, 4000, 4000, 4000));
            //Analyze(new Zakaz(960, 960, 960, 960, 960, 960, 960, 960, 960, 960));
            //Analyze(new Zakaz(
            //    1800, 500, 900, 1400, 1400, 1400,
            //    1400, 1400, 1400, 1400, 1400, 1400,
            //    1800, 2700, 1400, 3500, 1800, 1400,
            //    1400, 1400, 1400, 900, 900, 1800,
            //    1800, 900, 500, 1800, 3500, 1800,
            //    9000, 40000, 40000, 51000, 4500, 40000,
            //    30500, 16400, 9500, 9500, 12500, 500,
            //    29000, 22000, 1500, 4500, 15500, 1000,
            //    4300, 16400, 4500));
            //Analyze(new Zakaz(
            //   1800, 500, 900, 1400, 1400, 1400,
            //   1400, 1400, 1400, 1400, 1400, 1400,
            //   1800, 2700, 1400, 3500, 1800, 1400,
            //   1400, 1400, 1400, 900, 900, 1800,
            //   1800, 900, 500, 1800, 3500, 1800));
            //Analyze(new Zakaz(
            //    9000, 40000, 40000, 51000, 4500, 40000,
            //    30500, 16400, 9500, 9500, 12500, 500,
            //    29000, 22000, 1500, 4500, 15500, 1000,
            //    4300, 16400, 4500));
            //Analyze(new Zakaz(
            //    9000, 40000, 40000, 51000, 4500, 40000,
            //    30500, 16400, 9500, 9500, 12500, 500,
            //    29000, 22000, 1500, 4500, 15500, 1000,
            //    4300, 16400, 4500));
            //Analyze(new Zakaz(500, 500, 900, 900, 2700, 3500));
            //Analyze(new Zakaz(40000, 30500, 29000, 22000));
            //Analyze(new Zakaz(
            //    9500, 8800, 1400, 4500, 8800, 8800, 65300, 40000, 5300,
            //    21800, 39800, 800, 28300, 11600, 39800, 3100, 94000,
            //    23300, 2200, 1400, 3100, 2200, 13800, 1400, 1000, 6600,
            //    1400, 7900, 5300, 6800, 10100));
            Analyze(new Zakaz(10000, 10000, 20000, 15000, 15000, 10000, 20000, 30000, 10000));
            //Analyze(new Zakaz(6100, 80000, 61000, 32000, 80000, 59000, 3500, 14000, 3500,
            //    6100, 2700, 6100, 1000, 2000, 1000, 3100));
            //Analyze(new Zakaz(30000, 20000, 20000, 3000, 5000, 2000));
            //Zakaz z = DataImporter.LoadFile();
            //if (z != null)
            //{
            //    Analyze(z);  
            //} 

            Console.Write("Completed. Press Enter to exit program...");
            Console.ReadKey();

        }//end:Main

        private static void Analyze(Zakaz ets)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("Given data: ");
            Console.WriteLine("\t");
            foreach (Etyketka et in ets)
                Console.Write("{0} ", et.Run);
            Console.WriteLine();
            Console.WriteLine("Results:");

            //StatsList results = Analizer.ImproveStats(Analizer.FirstPass(ets));
            //StatsList results2 = Analizer.ImproveStats(Analizer.SplitByGcd(ets));
            //StatsList results3 = Analizer.ImproveStats(Analizer.PackIntoSheet(ets));

            StatsList results = (Analizer.FirstPass(ets));
            StatsList results2 = (Analizer.SplitByGcd(ets));
            
            if (results > results2)
                results = results2;


            foreach (var r in results)
            {
                Console.WriteLine("\n\t{0}", r);
                foreach (var e in r.Ets)
                {
                    Console.WriteLine("\t\t{0}", e);
                }
            }
            Console.WriteLine("============================================");
            Console.WriteLine();

            if (ets.Count <= Constants.ETS_ON_SHEET)
            {
                results = Analizer.FindBestRunWithShifting(ets);
                foreach (var r in results)
                {
                    Console.WriteLine("\n\t{0}", r);
                    foreach (var e in r.Ets)
                    {
                        Console.WriteLine("\t\t{0}", e);
                    }
                }
                Console.WriteLine("============================================");
                Console.WriteLine();
            }
        }


    }//end:class AnalizerTests
}
