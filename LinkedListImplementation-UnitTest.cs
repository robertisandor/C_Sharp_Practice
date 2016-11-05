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
      Assert.AreEqual(10, testList.Start.data);
      testList.AddFirst(20);
      Assert.AreEqual(testList.Count, 2);
      Assert.AreEqual(20, testList.Start.data);
    }

    [Test]
    public void PrintAllNodesTest()
    {
      // Figure out how to test console output
    }
  }
}
