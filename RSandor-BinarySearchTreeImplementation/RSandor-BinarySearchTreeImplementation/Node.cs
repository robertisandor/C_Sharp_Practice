using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_BinarySearchTreeImplementation
{
    // add unit tests for this & binary search tree classes

    // should I create a generic Node class that can be used and overridden with multiple data structures
    // much like in the Microsoft MSDN website?
    // in that version, they used a NodeList, which inherited from Collection
    // however, that example is from 2005 and uses some antiquated practices;
    // is the creation of a NodeList which inherits from Collection the same?
    // is there a better way to do this?
    public class Node<T> where T : IComparable<T>
    {
        public T Data { get; private set; }
        // create the left & right children

        public Node<T> Parent { get; private set; }

        public Node<T> Left { get; private set; }

        public Node<T> Right { get; private set; }

        /// <summary>
        /// Gives a default value to the Node
        /// based on the type.
        /// </summary>
        public Node()
        {
            Data = default(T);
            Left = null;
            Right = null;
            Parent = null;
        }

        public Node(Node<T> parentNode, T givenData)
        {
            Data = givenData;
            Left = null;
            Right = null;
            Parent = parentNode;
        }

        // to make Add work, would I use an IComparable interface or an overloaded operator?
        // there's also IComparable<T>, which is apparently different from just IComparable
        // I also see some people refer to an IEquatable for the "==" operator
        // would I use an IComparable interface, an IEquatable interface or something else?


        // besides Add & Remove functions, what else will the Node itself need?
        // should the Node or the Binary Search Tree contain a function that searches for a Node by value?
        // I don't need to worry about how to compare all the various types of objects
        // whoever gives me the objects should provide an implementation of CompareTo for the object in question
        // would the Add function also accept a Node as a parameter or an integer from the CompareTo method?

        /// <summary>
        /// Adds a Node to the Binary Search Tree.
        /// Requires that the Object being passed 
        /// implements the CompareTo method
        /// from the IComparable<T> interface.
        /// </summary>
        /// <param name="parentNode">The parent of the Node to be added.</param>
        /// <param name="data">The specified value to be added</param>
        public void Add(Node<T> parentNode, T data)
        {
            // CompareTo returns a -1 if the object in question should be before the current Node
            // returns a 1 if the object in question should be after the current Node
            // and returns a 0 if the object in question is equivalent to the current Node
            // what am I using parentNode for?
            int locationInLinkedList = data.CompareTo(parentNode.Data);
            if (locationInLinkedList == -1)
            {
                if (parentNode.Left == null)
                {
                    parentNode.Left = new Node<T>(parentNode, data);
                }
                else
                {
                    parentNode.Left.Add(parentNode.Left, data);
                }
            }
            else if (locationInLinkedList >= 0)
            {
                if (parentNode.Right == null)
                {
                    parentNode.Right = new Node<T>(parentNode, data);
                }
                else
                {
                    parentNode.Right.Add(parentNode.Right, data);
                }
            }
        }

        // to use Delete method, I should probably create a Search method that accepts a Node and a value
        // and returns a Node (or null, if it isn't in the BST) 
        // should Delete method take in a Node as a parameter?
        public Node<T> Delete(Node<T> startingNode, T data)
        {
            // have cases that deal with no children, 1 child, & 2 children
            // perhaps use trailing Node method to deal with relationship of parent?
            Node<T> nodeToDelete = Search(startingNode, data);
            if (nodeToDelete == null)
            {
                Console.WriteLine("{0} isn't in the list. {0} can't be deleted.", data);
            }
            else
            {
                Node<T> theParent = nodeToDelete.Parent;
                // check if there's a left child for the node-to-be-deleted
                if (nodeToDelete.Left == null)
                {
                    // in case we're deleting the root node...
                    if (theParent == null)
                    {
                        startingNode = nodeToDelete.Right;
                        startingNode.Parent = null;
                    }
                    // otherwise, if we're deleting the left child of a subtree...
                    else if (theParent.Data.CompareTo(nodeToDelete.Data) > 0)
                    {
                        // replace the node-to-be-deleted with the node immediately
                        theParent.Left = nodeToDelete.Right;
                    }
                    // otherwise if we're deleting the right child of a subtree...
                    else
                    {
                        theParent.Right = nodeToDelete.Right;
                        nodeToDelete.Right.Parent = theParent;
                    }
                }
                // if there is a left child, does that left child itself have a right child?
                // if it doesn't, we can merely replace the node-to-be-deleted with the left node
                else if (nodeToDelete.Left.Right == null)
                {
                    // in case we're deleting the root node...
                    if (theParent == null)
                    {
                        startingNode.Data = nodeToDelete.Left.Data;
                        startingNode.Left = nodeToDelete.Left.Left;
                        startingNode.Parent = null;
                    }
                    // otherwise, if we're deleting the left child of a subtree...
                    else if (theParent.Data.CompareTo(nodeToDelete.Data) > 0)
                    {
                        theParent.Left = nodeToDelete.Left;
                    }
                    // otherwise if we're deleting the right child of a subtree...
                    else
                    {
                        nodeToDelete.Left.Right = nodeToDelete.Right;
                        nodeToDelete.Left.Parent = theParent;
                        theParent.Right = nodeToDelete.Left;
                    }
                }
                else if (nodeToDelete.Left.Right != null)
                {
                    Node<T> traversalNode = nodeToDelete.Left.Right;
                    while(traversalNode.Right != null)
                    {
                        traversalNode = traversalNode.Right;
                    }
                    // in case we're deleting the root node...
                    if (theParent == null)
                    {
                        startingNode.Data = traversalNode.Data;
                        traversalNode = traversalNode.Parent;
                        traversalNode.Right = null;
                        startingNode.Parent = null;
                    }
                    // otherwise, if we're deleting the left child of a subtree...
                    else if (theParent.Data.CompareTo(nodeToDelete.Data) > 0)
                    {
                        theParent.Left.Data = traversalNode.Data;
                        traversalNode = traversalNode.Parent;
                        traversalNode.Right = null;
                    }
                    // otherwise if we're deleting the right child of a subtree...
                    else
                    {
                        theParent.Right.Data = traversalNode.Data;
                        traversalNode = traversalNode.Parent;
                        traversalNode.Right = null;
                    }
                }
            }
            return startingNode;
        }

        /// <summary>
        /// Searches through the binary tree and returns the Node with the specified
        /// value, if found. Otherwise, it returns null.
        /// </summary>
        /// <param name="startingNode">The Node to start searching from</param>
        /// <param name="data"></param>
        /// <returns>Returns the Node if it is in the Binary Search Tree.
        /// Otherwise, it returns null.</returns>
        public Node<T> Search(Node<T> startingNode, T data)
        {
            int locationInLinkedList = data.CompareTo(startingNode.Data);
            if (locationInLinkedList == 0)
            {
                return startingNode;
            }
            else if (locationInLinkedList == -1)
            {
                if (startingNode.Left != null)
                {
                    return Search(startingNode.Left, data);
                }
                else
                {
                    return null;
                }
            }
            else if (locationInLinkedList == 1)
            {
                if (startingNode.Right != null)
                {
                    return Search(startingNode.Right, data);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
