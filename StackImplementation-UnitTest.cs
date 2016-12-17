using System;
using NUnit.Framework;
using System.Collections;

namespace SimpleDataStructures
{
    [TestFixture]
    public class StackTest
    {
        [Test]
        public void DefaultConstructorTest(Stack<int> testStack)
        {
            //Assert.AreEqual(0, testStack.LastIndex);
            //Assert.AreEqual(10, testStack.Stack.Length);
        }

        [Test]
        public void ParameterizedConstructorTest(Stack<int> testStack)
        {
            //int initialStackCapacity = 30;
            //testStack = new Stack<int>(initialStackCapacity);
            //Assert.AreEqual(0, testStack.LastIndex);
            //Assert.AreEqual(initialStackCapacity, testStack.Stack.Length);
        }

        // how do I check to make sure that an error is thrown?
        [Test]
        public void ParameterizedConstructorErrorTest(Stack<int> testStack)
        {
            int initialStackCapacity = -20;
            //testStack = new Stack<int>(initialStackCapacity);
            // code to make sure the error is thrown and the message is displayed
        }

        [Test]
        public void ClearTest(Stack<int> testStack)
        {

        }

        [Test]
        public void ItIsEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void ItIsNotEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PeekWhenEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PeekWhenNotEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PopWhenEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PopWhenNotEmptyTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PushNoResizeTest(Stack<int> testStack)
        {

        }

        [Test]
        public void PushAndResizeTest(Stack<int> testStack)
        {

        }
    }
}