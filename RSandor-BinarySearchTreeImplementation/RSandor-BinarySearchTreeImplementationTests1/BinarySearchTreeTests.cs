using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSandor_BinarySearchTreeImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_BinarySearchTreeImplementation.Tests
{
    [TestClass()]
    public class BinarySearchTreeTests
    {
        [TestMethod()]
        public void BinarySearchTreeTest()
        {
            BinarySearchTree<int> practiceTree = new BinarySearchTree<int>();
            Assert.AreEqual(null, practiceTree.Root);
        }

        [TestMethod()]
        public void AddTest()
        {
            BinarySearchTree<int> practiceTree = new BinarySearchTree<int>();
            practiceTree.Add(4);
            Assert.AreEqual(4, practiceTree.Root.Data, "The value of the root is supposed to be 4.");
            Assert.AreNotEqual(null, practiceTree.Root, "There is suppoed to be something at the root.");
            Assert.AreEqual(null, practiceTree.Root.Parent, "The parent of the root is supposed to be null.");
            Assert.AreEqual(null, practiceTree.Root.Left, "At this point, the root isn't supposed to have a left child.");
            Assert.AreEqual(null, practiceTree.Root.Right, "At this point, the root isn't supposed to have a right child.");
            practiceTree.Add(2);
            Assert.AreNotEqual(null, practiceTree.Root.Left, "At this point, the root is supposed to have a left child.");
            Assert.AreEqual(2, practiceTree.Root.Left.Data, "At this point, the root is supposed to have a left child with a value of 2.");
            practiceTree.Add(1);
            Assert.AreNotEqual(null, practiceTree.Root.Left.Left, "At this point, the root is supposed to have a left child with a left child.");
            Assert.AreEqual(1, practiceTree.Root.Left.Left.Data, "At this point, the root is supposed to have a left child with a left child with a value of 1.");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            BinarySearchTree<int> practiceTree = new BinarySearchTree<int>();
            practiceTree.Add(4);
            practiceTree.Add(2);
            practiceTree.Add(1);
            practiceTree.Add(3);
            practiceTree.Delete(3);
            Assert.AreEqual(null, practiceTree.Root.Left.Right, "This node should be null now.");
            practiceTree.Delete(2);
            Assert.AreEqual(1, practiceTree.Root.Left.Data, "This node should have the value of 1 now.");
        }

        [TestMethod()]
        public void SearchTest()
        {
            BinarySearchTree<int> practiceTree = new BinarySearchTree<int>();
            practiceTree.Add(4);
            practiceTree.Add(2);
            practiceTree.Add(1);
            practiceTree.Add(3);
            Assert.AreEqual(false, practiceTree.Search(5));
            Assert.AreEqual(true, practiceTree.Search(4));
            Assert.AreEqual(true, practiceTree.Search(1));
        }
    }
}