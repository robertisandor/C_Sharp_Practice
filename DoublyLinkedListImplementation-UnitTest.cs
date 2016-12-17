using System;
using NUnit.Framework;
using System.Collections;

namespace SimpleDataStructures
{
    // is this the proper way to set up parameterized tests?
    // how do I set up parameterized test fixtures that are generic?
    // with no parameters, the tests don't run
    [TestFixture]
    // with 1 parameter though, I get the error:
    // "The type `NUnit.Framework.TestFixtureAttribute' does not contain a constructor that takes `1' arguments"
    //[TestFixture(typeof(DoublyLinkedList<int>))]
    // what do the TList and Ilist mean in this context? what does where and new do in this context?
    // where - generic type constraint, 
    // new() - constructor constraint; any type argument supplied must have 
    // an accessible parameterless/default constructor
    //public class DoublyLinkedListTestClass<TList> //where TList : IList, new()
    public class DoublyLinkedListTestClass<TList> where TList : IList, new() 
    {
        // why does the example have this as IList? should this be something else?
        private IList testList;
        //DoublyLinkedList<int> testList;
        //private TList testList;

        [SetUp]
        public void CreateDoublyLinkedList()
        {
            this.testList = new TList();
            //this.testList = new DoublyLinkedList<int>();
        }

        [Test]
        public void ConstructorTest(DoublyLinkedList<int> testList)
        {
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
        }

        [Test]
        public void InsertWhenEmptyTest(DoublyLinkedList<int> testList)
        {
            testList.Insert(11, 0);
            Assert.AreEqual(1, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(11, testList.Head.data);
        }

        [Test]
        public void InsertWhenNotEmptyTest(DoublyLinkedList<int> testList)
        {
            testList.Insert(11, 0);
            testList.Insert(22, 1);
            Assert.AreEqual(2, testList.Size);
            Assert.AreEqual(22, testList.Head.data);
            Assert.AreEqual(11, testList.Head.next.data);
        }

        [Test]
        public void AppendWhenEmptyTest(DoublyLinkedList<int> testList)
        {
            testList.Append(55);
            Assert.AreEqual(1, testList.Size);
            Assert.AreNotEqual(null, testList.Head);
            Assert.AreEqual(55, testList.Head.data);
        }

        [Test]
        public void AppendWhenNotEmptyTest(DoublyLinkedList<int> testList)
        {
            testList.Append(55);
            testList.Append(66);
            Assert.AreEqual(2, testList.Size);
            Assert.AreNotEqual(null, testList.Head.next);
            Assert.AreEqual(66, testList.Head.next.data);
            Assert.AreEqual(66, testList.Tail.data);
            Assert.AreEqual(55, testList.Tail.previous.data);
        }

        [Test]
        public void RemoveAtHeadTest(DoublyLinkedList<int> testList)
        {
            testList.Insert(10, 0);
            testList.RemoveAt(0);
            Assert.AreEqual(0, testList.Size);
            Assert.AreEqual(null, testList.Head);
            Assert.AreEqual(null, testList.Tail);
        }

        [Test]
        public void RemoveAtMiddleTest(DoublyLinkedList<int> testList)
        {
            
        }

        [Test]
        public void RemoveAtTailTest(DoublyLinkedList<int> testList)
        {

        }

        // how do I test a function that prints out messages/data?
        [Test]
        public void PrintAllNodesTest(DoublyLinkedList<int> testList)
        {
            testList.Insert(1, 0);
            testList.Append(2);
            testList.Append(3);
            testList.Append(5);
        }

        [Test]
        public void IndexOfTest(DoublyLinkedList<int> testList)
        {

        }
    }
}