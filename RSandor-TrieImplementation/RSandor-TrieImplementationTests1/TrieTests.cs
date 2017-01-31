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
    public class TrieTests
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            Trie practiceTrie = new Trie();
            Assert.AreEqual('^', practiceTrie.Root.Data, "The root's data should be '^'");
            Assert.AreEqual(false, practiceTrie.Root.IsWord, "The root by itself isn't a word.");
            Assert.AreEqual(0, practiceTrie.Root.Depth, "The depth of the root should be 0.");
            Assert.AreEqual(null, practiceTrie.Root.Parent, "The parent of the root should be null.");
        }
        [TestMethod()]
        public void InsertTest()
        {
            Trie practiceTrie = new Trie();
            practiceTrie.Insert("at");
            Assert.AreEqual('a', practiceTrie.Root.Children[0].Data, "The first child" +
                "of the root should be the letter 'a'.");
            Assert.AreEqual(false, practiceTrie.Root.Children[0].IsWord, "The first letter (by itself) shouldn't be a complete word at this point.");
            Assert.AreEqual(1, practiceTrie.Root.Children[0].Depth, "The depth of the first letter should be 1.");
            Assert.AreEqual(practiceTrie.Root, practiceTrie.Root.Children[0].Parent, "The parent of the first letter should be the root.");
            Assert.AreEqual('t', practiceTrie.Root.Children[0].Children[0].Data, "The first " + 
                "child of the first child is 't'.");
            Assert.AreEqual(true, practiceTrie.Root.Children[0].Children[0].IsWord, "The second letter should indicate that it's the end of a complete word.");
            Assert.AreEqual(2, practiceTrie.Root.Children[0].Children[0].Depth, "The second letter should have a depth of 2.");
            Assert.AreEqual(practiceTrie.Root.Children[0], practiceTrie.Root.Children[0].Children[0].Parent, "The parent of the child's child" +
                "should be the first child of the root.");
            practiceTrie.Insert("");
            Assert.AreEqual(1, practiceTrie.Root.Children.Count, "After inputting an empty string, I still should have only 1 child.");
            // I should test by inserting weird input, such as an empty string, a null string, and a string with non-letter characters
            try
            {
                practiceTrie.Insert(null);
                Assert.Fail("The null reference exception wasn't caught");
            }
            catch (NullReferenceException exception)
            {

            }
        }
    }
}