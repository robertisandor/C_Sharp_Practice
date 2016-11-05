using System;
using NUnit.Framework;

namespace SimpleDataStructures
{
  [TestFixture]
  public class LinkedListTestClass
  {
    [Test]
    public void ConstructorTest()
    {
      LinkedList testList = new LinkedList();
      Assert.AreEqual(testList.Count, 0);
      Assert.AreEqual(testList.Start, null);
    }

    [Test]
    public void AddFirstTest()
    {
      LinkedList testList = new LinkedList();
      testList.AddFirst(10);
      Assert.AreEqual(testList.Count, 1);
      Assert.AreEqual(testList.Start.data, 10);
      testList.AddFirst(20);
      Assert.AreEqual(testList.Count, 2);
      Assert.AreEqual(testList.Start.data, 20);
    }

    [Test]
    public void AddLastTest()
    {
      LinkedList testList = new LinkedList();
      testList.AddLast(5);
      Assert.AreEqual(testList.Count, 1);
      Assert.AreEqual(testList.Start.data, 5);
    }

    [Test]
    public void PrintAllNodesTest()
    {
      // Figure out how to test console output
    }
  }
}
