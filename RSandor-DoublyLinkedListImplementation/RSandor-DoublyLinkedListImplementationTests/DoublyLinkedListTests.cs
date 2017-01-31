using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataStructures.Tests
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        [TestMethod]
        public void DoublyLinkedListTest()
        {
            DoublyLinkedList<int> testList = new DoublyLinkedList<int>();
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
            Assert.AreEqual(null, testList.Tail);
        }

        [TestMethod]
        public void AppendTest()
        {
            DoublyLinkedList<int> testList = new DoublyLinkedList<int>();
            testList.Append(1);
            Assert.AreEqual(1, testList.Size);
            Assert.AreEqual(1, testList.Head.data);
        }

        [TestMethod]
        public void ClearTest()
        {
            DoublyLinkedList<int> testList = new DoublyLinkedList<int>();
            testList.Append(1);
            testList.Append(2);
            testList.Clear();
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
            Assert.AreEqual(null, testList.Tail);
        }

        [TestMethod]
        public void ContainsTest()
        {
            DoublyLinkedList<int> testList = new DoublyLinkedList<int>();
            testList.Append(1);
            Assert.AreEqual(true, testList.Contains(1));
            Assert.AreEqual(false, testList.Contains(49));
        }

        [TestMethod]
        public void IndexOfTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void InsertTest()
        {
            DoublyLinkedList<int> testList = new DoublyLinkedList<int>();
            testList.Append(1);
            testList.Append(4);

            //TODO: Supply data to the test via attributes
            //Position / value data
            Tuple<int, int>[] data =
            {
                new Tuple<int, int>(0, 9),
                new Tuple<int, int>(2, 20),
                new Tuple<int, int>(1, 5)
            };

            for(int i = 0; i < data.Length; i++)
            {
                testList.Insert(data[i].Item2, data[i].Item1);
                Assert.AreEqual(data[i].Item2, testList[i], $"Broke on {data[i].Item2}");
            }

            //testList.Insert(9, 0);
            //Assert.AreEqual(9, testList[0], "Broke on 9");
            //testList.Insert(20, 2);
            //Assert.AreEqual(20, testList[2], "Broke on 20");
            //testList.Insert(5, 1);
            //Assert.AreEqual(5, testList[1], "Broke on 5");
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            Assert.Fail();
        }
    }
}