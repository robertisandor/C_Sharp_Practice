using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_AVL_Tree
{
    public class AVLTree<T>
    {
        public readonly AVLNode<T> Root;

        public AVLTree()
        {

        }

        public void Add()
        {

        }

        public void Remove()
        {

        }

        public void Print(AVLNode<T> root)
        {
            // this feels wrong
            if (root.Left != null)
            {
                root.Left.Print();
            }
            Console.WriteLine(Root.Data);
            if (root.Right != null)
            {
                root.Right.Print();
            }
        }
    }
}
