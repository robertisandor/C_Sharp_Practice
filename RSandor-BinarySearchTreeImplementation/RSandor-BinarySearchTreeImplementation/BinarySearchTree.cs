using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_BinarySearchTreeImplementation
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        /// <summary>
        /// Initializes the Binary Search Tree
        /// and sets the Root to null.
        /// </summary>
        public BinarySearchTree()
        {
            Root = null;
        }

        /// <summary>
        /// Adds a Node with the specified value to the Binary Search Tree.
        /// </summary>
        /// <param name="data">The value to be added</param>
        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(null, data);
            }
            else
            {
                Root.Add(Root, data);
            }
        }

        public void Delete(T data)
        {
            if (Root == null)
            {
                Console.WriteLine("The list is empty. There is nothing to delete.");
            }
            else
            {
                Root = Root.Delete(Root, data);
            }
        }

        /// <summary>
        /// Prints the values of the Nodes in order.
        /// Requires the Object being passed to have a ToString method.
        /// </summary>
        /// <param name="nodeToPrint">The node currently being looked at.</param>
        public void Print(Node<T> nodeToPrint)
        {
            // check if the current node is null/empty
            // to avoid a NullReferenceException
            if (nodeToPrint != null)
            {
                // tries to find the leftmost/smallest #
                if (nodeToPrint.Left != null)
                {
                    Print(nodeToPrint.Left);
                }

                Console.WriteLine("{0}", nodeToPrint.Data);

                if (nodeToPrint.Right != null)
                {
                    Print(nodeToPrint.Right);
                }
            }
        }

        /// <summary>
        /// Searches the Binary Search Tree for the specified value
        /// and returns true if found.
        /// </summary>
        /// <param name="data">The value to look for</param>
        /// <returns>Returns true if the Node already exists and false if it doesn't.</returns>
        public bool Search(T data)
        {
            return (Root.Search(this.Root, data) != null);
        }
    }
}
