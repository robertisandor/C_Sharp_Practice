using System;
using System.Collections.Generic;

namespace RS_AVL_Tree
{
    public class AVLNode<T>
    {
        public T Data { get; private set; }
        public AVLNode<T> Left { get; private set; }
        public AVLNode<T> Right { get; private set; }
        public int Height { get; private set; }
        public int BalanceFactor { get; private set; }

        public AVLNode(T Data)
        {
            Left = null;
            Right = null;
        }

        // I would need to go down the left subtree if the left node isn't null,
        // otherwise, print the left node, print the current node,
        // then go down the right subtree
        // and go down the left subtree again
        // use recursion
        public void Print()
        {
            if (Left != null)
            {
                   
            }
        }
    }
}