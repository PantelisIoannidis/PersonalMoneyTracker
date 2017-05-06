using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMT.Common;
using System.Linq;
using System.Collections.Generic;

namespace PMT.Web.Tests.Common
{
    [TestClass]
    public class UtilsTest
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }
        public class SampleObject
        {
            public string ItemA { get; set; }
            public int ItemB { get; set; }
            public int ItemC { get; set; }
        }
        [TestMethod]
        public void DistinctByMustRemoveDuplicatesValuesInComplexTypes()
        {
            List<SampleObject> withDuplicates = new List<SampleObject>();
            withDuplicates.Add(new SampleObject() { ItemA = "Foo", ItemB = 10 ,ItemC = 1 });
            withDuplicates.Add(new SampleObject() { ItemA = "Bar", ItemB = 20, ItemC = 2 });
            withDuplicates.Add(new SampleObject() { ItemA = "Foo", ItemB = 30, ItemC = 3 });
            withDuplicates.Add(new SampleObject() { ItemA = "Bar", ItemB = 10, ItemC = 4 });

            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemA).Count() == 2);
            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemB).Count() == 3);
            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemC).Count() == 4);

        }
    }
}
