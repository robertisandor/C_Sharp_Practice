using System;
using NUnit.Framework;

namespace SimpleDataStructures
{
    [TestFixture]
    public class DoublyLinkedListTestClass
    {
        DoublyLinkedList<int> testList = new DoublyLinkedList<int>();

        [Test]
        public void A_ConstructorTest()
        {
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
        }

        [Test]
        public void B_AddToFrontTest()
        {
            testList.AddToFront(11);
            Assert.AreEqual(1, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(11, testList.Head.data);
            testList.AddToFront(22);
            Assert.AreEqual(2, testList.Size);
            Assert.AreEqual(22, testList.Head.data);
            Assert.AreEqual(11, testList.Head.next.data);
        }

        [Test]
        [Ignore("Ignoring this test for now.")]
        public void AddToEndTest()
        {
            testList.AddToEnd(55);
            Assert.AreEqual(1, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(55, testList.Head.data);
            testList.AddToEnd(66);
            Assert.AreEqual(2, testList.Size);
            Assert.AreNotEqual(null, testList.Head.next);
            Assert.AreEqual(66, testList.Head.next.data);
        }
    }
}