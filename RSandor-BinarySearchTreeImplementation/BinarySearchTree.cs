using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_BinarySearchTreeImplementation
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node Root { get; private set; } 
        public BinarySearchTree()
        {
            Root = null;
        }

        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(data);
            }
            else
            {
                // here, I would delegate the adding to a function of the Node class
                // start with the root node and recursively check if the node is greater than or less than its parent
                // I would use the CompareTo method
                Root.Add(data);
            }
        }
    }
}
