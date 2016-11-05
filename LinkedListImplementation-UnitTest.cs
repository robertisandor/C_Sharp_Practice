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
      Assert.Equals(testList.Count, 0);
    }
  }
}
