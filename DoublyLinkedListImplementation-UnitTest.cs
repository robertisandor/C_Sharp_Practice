using System;
using NUnit.Framework;

namespace SimpleDataStructures
{
    [TestFixture]
    public class DoublyLinkedListTestClass
    {
        DoublyLinkedList<int> testList = new DoublyLinkedList<int>();

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
        }

        public void AddToFrontTest()
        {
            testList.AddToFront(11);
            Assert.AreEqual(1, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(11, testList.Head.data);
        }

        public void AddToEndTest()
        {
            testList.AddToEnd(55);
            Assert.AreEqual(2, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(11, testList.Head.data);
            Assert.AreEqual(testList.Head, testList.Head.next.previous);
            Assert.AreEqual(55, testList.Head.next.data);
        }
    }
}