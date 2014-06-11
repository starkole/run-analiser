using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Et075.Model;

namespace Tests_Et075
{
    [TestClass]
    public class AnalizerTests : Analizer
    {
        [TestMethod]
        public void GetStatsForRunTests()
        {
            Zakaz zak;
            int run;
            OutStats result;

            zak = new Zakaz(16000);
            run = 1000;
            result = new OutStats(zak, run, 0, 16);
            Assert.AreEqual(Analizer.GetStatsForRun(zak, run), result);

            zak = new Zakaz(15000);
            run = 1000;
            result = new OutStats(zak, run, 0, 15);
            Assert.AreEqual(Analizer.GetStatsForRun(zak, run), result);

            zak = new Zakaz(800, 2000, 2000, 26000);
            run = 2000;
            result = new OutStats(zak, run, 1200, 16);
            Assert.AreEqual(Analizer.GetStatsForRun(zak, run), result);

            zak = new Zakaz(69600, 91500);
            run = 2000;
            result = new OutStats(zak, run, 1200, 16);
        }

        [TestMethod]
        public void DecreaseRunTests()
        {
            Zakaz zak;
            int run;
            OutStats result;

            zak = new Zakaz(4500);
            run = 1000;
            result = new OutStats(zak, run, 0, 16);
            Assert.AreEqual((Analizer.DecreaseRun(zak, run)).ToString(), result.ToString());

        }

    }
}
