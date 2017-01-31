using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSandor_TrieImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_TrieImplementation.Tests
{
    [TestClass()]
    public class TrieNodeTests
    {
        [TestMethod()]
        public void TrieNodeConstructorTest()
        {
            // I could probably do this with tuples
            TrieNode practiceTrieNode = new TrieNode();
            Assert.AreEqual(null, practiceTrieNode.Parent);
            Assert.AreEqual(0, practiceTrieNode.Depth);
            Assert.AreEqual('^', practiceTrieNode.Data);
            Assert.AreEqual(false, practiceTrieNode.IsWord);

            TrieNode anotherPracticeTrieNode = new TrieNode('a', 1, practiceTrieNode, false);
            Assert.AreEqual(practiceTrieNode, anotherPracticeTrieNode.Parent);
            Assert.AreEqual(1, anotherPracticeTrieNode.Depth);
            Assert.AreEqual('a', anotherPracticeTrieNode.Data);
            Assert.AreEqual(false, anotherPracticeTrieNode.IsWord);

            TrieNode lastPracticeTrieNode = new TrieNode('t', 2, anotherPracticeTrieNode, true);
            Assert.AreEqual(anotherPracticeTrieNode, lastPracticeTrieNode.Parent);
            Assert.AreEqual(2, lastPracticeTrieNode.Depth);
            Assert.AreEqual('t', lastPracticeTrieNode.Data);
            Assert.AreEqual(true, lastPracticeTrieNode.IsWord);
        }
    }
}