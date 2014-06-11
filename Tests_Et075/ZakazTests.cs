using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Et075.Model;

namespace Tests_Et075
{
    [TestClass]
    public class ZakazTests
    {
        [TestMethod]
        public void SplitMarkersTests()
        {
            Zakaz z;
            List<int> actual;
            
            z = new Zakaz();
            actual = z.SplitMarkers;
            Assert.AreEqual(0, actual.Count, "There is no split markers for empty zakaz");

            z = new Zakaz(1000);
            actual = z.SplitMarkers;
            Assert.AreEqual(0, actual.Count, "There is no split markers for small zakaz");

            z = new Zakaz(1000, 2000, 3000, 4000);
            actual = z.SplitMarkers;
            Assert.AreEqual(0, actual.Count, "There is no split markers for small zakaz");

            z = new Zakaz(1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000,
                          1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 2000, 1000);
            actual = z.SplitMarkers;
            Assert.AreEqual(0, actual.Count, "There is no split markers for unsorted zakaz");

            z = new Zakaz(1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000,
                          1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000);
            actual = z.SplitMarkers;
            Assert.AreEqual(1, actual.Count, "There is 1 split marker for 1000x20 zakaz");
            Assert.AreEqual(Constants.ETS_ON_SHEET, actual[0], "Split marker for 1000x20 zakaz equals ETS_ON_SHEET");

            z = new Zakaz(1000, 1000, 1000, 1000, 2000, 2000, 2000, 2000, 2000, 2000,
                          2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 2000, 
                          2000, 2000);
            actual = z.SplitMarkers;
            Assert.AreEqual(2, actual.Count, "There is 2 split markers for 1000x4 + 2000x18 zakaz");
            Assert.AreEqual(4, actual[0], "First split marker for 1000x4 + 2000x18 zakaz is 4");
            Assert.AreEqual(20, actual[1], "Second split marker for 1000x4 + 2000x18 zakaz equals ETS_ON_SHEET");
        }
    }
}
