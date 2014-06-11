using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Et075.Model;

namespace Tests_Et075
{
    [TestClass]
    public class GenericTests
    {
        [TestMethod]
        public void FirstPassTests()
        {
            Zakaz zak;
            StatsList actual;

            zak = new Zakaz(10000, 10000, 20000, 15000, 15000, 10000, 20000, 30000, 10000);
            actual = Analizer.FirstPass(zak);
            Assert.AreEqual(1, actual.Count, "FirstPass cannot split zakaz with count <= ETS_ON_SHEET");
            Assert.AreEqual(10000, actual[0].Run, "Optimal run for this zakaz is 10000");
            Assert.AreEqual(15, actual[0].EtsOnSheetCount, "For run 10000 ets on sheet count is 15");
            Assert.AreEqual(5000 + 5000, actual[0].OverprintsSum, "For run 10000 overprint sum is 10000");
            Assert.AreEqual(5000 + 5000 + 10000, actual[0].CorrectedOverprintsSum, "For run 10000 corrected overprint sum is 20000");
            
        }
    }
}
