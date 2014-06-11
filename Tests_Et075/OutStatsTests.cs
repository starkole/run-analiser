using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Et075.Model;

namespace Tests_Et075
{
    [TestClass]
    public class OutStatsTests
    {
        [TestMethod]
        public void OutStatsTest()
        {
            OutStats a = new OutStats();
            OutStats b = new OutStats();
            Assert.AreEqual(a.Equals(b), true);
            Assert.AreEqual(b.Equals(a), true);
            Assert.AreEqual(a.Equals(1), false);
            Assert.AreEqual(a.Equals(null), false);

            a.OverprintsSum = 1000;
            Assert.AreNotEqual(a, b);
            Assert.AreEqual(a > b, true);

            b.OverprintsSum = 1000;
            Assert.AreEqual(a, b);

            b.OverprintsSum = 2000;
            Assert.AreNotEqual(a, b);
            Assert.AreEqual(a < b, true);

            a.EtsOnSheetCount = 14;
            a.Run = 1000;
            a.OverprintsSum = 1000;
            b.OverprintsSum = 3000;
            Assert.AreEqual(a, b);

            a.EtsOnSheetCount = Constants.ETS_ON_SHEET + 1;
            Assert.AreEqual(a.IsValid, false);

        }
    }
}
