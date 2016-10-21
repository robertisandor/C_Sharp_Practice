using System;
using NUnit.Framework;

namespace PracticeTest
{
  [TestFixture]
  public class PracticeClassTest
  {
    [Test]
    public void testHelloWorld()
    {
      PracticeClass example = new PracticeClass();
      Assert.AreEqual("Hello World!", example.helloWorld());
    }
  }
}
