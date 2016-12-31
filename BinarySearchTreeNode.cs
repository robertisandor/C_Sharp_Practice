using System;
using System.Collections.Generic;

// I've seen an implementation of a binary search tree
// that uses a NodeList that inherits from Collections
// should I do this myself?

// one implementation has add, contains & remove methods

// a different implementation has isLeaf,
// insert, search and disply methods

// a separate implementation in C++ has nodesCount,
// height and printMaxPath methods

// another implementation in C++ has inorder,
// preorder and postorder methods

// preorder traversal - useful for:
// cloning a tree 
// I don't see any methods to copy a tree
// or a linked list in any implementation

// convert expression tree to prefix/Polish notation
// postorder traversal - useful for:
// destroying a tree
// inorder traversal - useful for:
// sorting a tree

// the C# implementation uses a non-recursive solution
// because recursive solutions are sub-optimal

// clearing the tree would use postorder traversal
// do I need to have an algorithm for that though?
// with C#, if I just set the root to null, it'll manage
// the memory for me
// I don't see many BSTs with an empty method

// printing the tree would use inorder traversal
// if I choose to copy the tree, use preorder traversal

// create an overriden ToString method 
namespace SimpleDataStructures
{
    class BinarySearchTreeNode<T>
    {
        public T Data { get; private set; }
        public BinarySearchTreeNode<T> RightLeaf;
        public BinarySearchTreeNode<T> LeftLeaf;

        public BinarySearchTreeNode()
        {
            Data = default(T);
            RightLeaf = null;
            LeftLeaf = null;
        }

        public BinarySearchTreeNode(T value)
        {
            Data = value;
            RightLeaf = null;
            LeftLeaf = null;
        }

        //public bool isEmpty()
        //{

        //}
    }
}