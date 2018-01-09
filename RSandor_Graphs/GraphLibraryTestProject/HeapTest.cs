using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLib;
using System.Linq;
using System.Collections.Generic;

namespace GraphLibraryTestProject
{
    public class SecondValueFirst : Comparer<(double, double)>
    {
        public override int Compare((double, double) x, (double, double) y)
        {
            if(x.Item2.CompareTo(y.Item2) != 0)
            {
                return x.Item2.CompareTo(y.Item2);
            }
            else if (x.Item1.CompareTo(y.Item1) != 0)
            {
                return x.Item1.CompareTo(y.Item1);
            }
            else
            {
                return 0;
            }
        }
    }

    [TestClass]
    public class HeapTest
    {
        [TestMethod]
        public void HeapConstructionTest()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            Assert.AreEqual(0, maxHeap.Count);
            Assert.AreEqual(0, maxHeap.Capacity);

            MaxHeap<int> secondMaxHeap = new MaxHeap<int>(new[] { 20, 9, 1, 13, 4, 18, 9, 2 });
            Assert.AreEqual(8, secondMaxHeap.Count);
            Assert.AreEqual(15, secondMaxHeap.Capacity);

            MaxHeap<(double x, double y)> thirdMaxHeap = new MaxHeap<(double x, double y)>(new SecondValueFirst());

            MaxHeap<(double x, double y)> fourthMaxHeap = new MaxHeap<(double x, double y)>(new List<(double, double)> { (20, 9), (1, 13), (4, 18), (9, 2) }, new SecondValueFirst());

            MinHeap<int> minHeap = new MinHeap<int>();
            Assert.AreEqual(0, minHeap.Count);
            Assert.AreEqual(0, minHeap.Capacity);

            MinHeap<int> secondMinHeap = new MinHeap<int>(new[] { 20, 9, 1, 13, 4, 18, 9, 2 });
            Assert.AreEqual(8, secondMinHeap.Count);
            Assert.AreEqual(15, secondMinHeap.Capacity);

            MinHeap<(double x, double y)> thirdMinHeap = new MinHeap<(double x, double y)>(new SecondValueFirst());

            MinHeap<(double x, double y)> fourthMinHeap = new MinHeap<(double x, double y)>(new List<(double, double)> { (20, 9), (1, 13), (4, 18), (9, 2) }, new SecondValueFirst());
        }

        [TestMethod]
        public void HeapAddTest()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            Assert.AreEqual(0, maxHeap.Count);
            Assert.AreEqual(0, maxHeap.Capacity);

            maxHeap.Add(10);
            maxHeap.Add(20);
            maxHeap.Add(30);
            maxHeap.Add(40);

            Assert.AreEqual(4, maxHeap.Count);
            Assert.AreEqual(7, maxHeap.Capacity);

            maxHeap.Add(15);
            maxHeap.Add(25);
            maxHeap.Add(35);
            maxHeap.Add(45);

            Assert.AreEqual(8, maxHeap.Count);
            Assert.AreEqual(15, maxHeap.Capacity);

            MinHeap<int> minHeap = new MinHeap<int>();
            Assert.AreEqual(0, minHeap.Count);
            Assert.AreEqual(0, minHeap.Capacity);

            minHeap.Add(10);
            minHeap.Add(20);
            minHeap.Add(30);
            minHeap.Add(40);
            
            Assert.AreEqual(4, minHeap.Count);
            Assert.AreEqual(7, minHeap.Capacity);

            minHeap.Add(15);
            minHeap.Add(25);
            minHeap.Add(35);
            minHeap.Add(45);

            Assert.AreEqual(8, minHeap.Count);
            Assert.AreEqual(15, minHeap.Capacity);
        }

        [TestMethod]
        public void HeapExtractTest()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();
            Assert.AreEqual(0, maxHeap.Count);
            Assert.AreEqual(0, maxHeap.Capacity);

            maxHeap.Add(10);
            maxHeap.Add(20);
            maxHeap.Add(30);
            maxHeap.Add(40);

            Assert.AreEqual(40, maxHeap.ExtractDominating());
            Assert.AreEqual(3, maxHeap.Count);
            Assert.AreEqual(7, maxHeap.Capacity);

            maxHeap.Add(15);
            maxHeap.Add(25);
            maxHeap.Add(35);
            maxHeap.Add(45);

            Assert.AreEqual(45, maxHeap.ExtractDominating());
            Assert.AreEqual(6, maxHeap.Count);
            Assert.AreEqual(7, maxHeap.Capacity);

            MinHeap<int> minHeap = new MinHeap<int>();
            Assert.AreEqual(0, minHeap.Count);
            Assert.AreEqual(0, minHeap.Capacity);

            minHeap.Add(10);
            minHeap.Add(20);
            minHeap.Add(30);
            minHeap.Add(40);

            Assert.AreEqual(10, minHeap.ExtractDominating());
            Assert.AreEqual(3, minHeap.Count);
            Assert.AreEqual(7, minHeap.Capacity);

            minHeap.Add(15);
            minHeap.Add(25);
            minHeap.Add(35);
            minHeap.Add(45);

            Assert.AreEqual(15, minHeap.ExtractDominating());
            Assert.AreEqual(6, minHeap.Count);
            Assert.AreEqual(7, minHeap.Capacity);
        }

        [TestMethod]
        public void ExtractEmptyHeapTest()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>();

            try
            {
                maxHeap.ExtractDominating();
            }
            catch (InvalidOperationException exception)
            {

            }

            MinHeap<int> minHeap = new MinHeap<int>();
            try
            {
                minHeap.ExtractDominating();
            }
            catch (InvalidOperationException exception)
            {

            }
        }

        [TestMethod]
        public void TestHeapBySorting()
        {
            var minHeap = new MinHeap<int>(new[] { 9, 8, 4, 1, 6, 2, 7, 4, 1, 2 });
            AssertHeapSort(minHeap, minHeap.OrderBy(i => i).ToArray());

            minHeap = new MinHeap<int> { 7, 5, 1, 6, 3, 2, 4, 1, 2, 1, 3, 4, 7 };
            AssertHeapSort(minHeap, minHeap.OrderBy(i => i).ToArray());

            var maxHeap = new MaxHeap<int>(new[] { 1, 5, 3, 2, 7, 56, 3, 1, 23, 5, 2, 1 });
            AssertHeapSort(maxHeap, maxHeap.OrderBy(d => -d).ToArray());

            maxHeap = new MaxHeap<int> { 2, 6, 1, 3, 56, 1, 4, 7, 8, 23, 4, 5, 7, 34, 1, 4 };
            AssertHeapSort(maxHeap, maxHeap.OrderBy(d => -d).ToArray());
        }

        private static void AssertHeapSort(Heap<int> heap, IEnumerable<int> expected)
        {
            var sorted = new List<int>();
            while (heap.Count > 0)
            { 
                sorted.Add(heap.ExtractDominating());
            }

            Assert.IsTrue(sorted.SequenceEqual(expected));
        }
    }
}
