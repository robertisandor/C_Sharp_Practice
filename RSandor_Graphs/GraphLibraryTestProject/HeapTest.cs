using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLib;

namespace GraphLibraryTestProject
{
    [TestClass]
    public class HeapTest
    {
        [TestMethod]
        public void HeapConstructionTest()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            Assert.AreEqual(0, maxHeap.Count);
            Assert.AreEqual(0, maxHeap.Capacity);

            maxHeap.Add(10);
            maxHeap.Add(20);
            maxHeap.Add(30);
            maxHeap.Add(40);

            Assert.AreEqual(, maxHeap.);
            maxHeap.Add(15);
            maxHeap.Add(25);
            maxHeap.Add(35);
            maxHeap.Add(45);
        }
    }
}
