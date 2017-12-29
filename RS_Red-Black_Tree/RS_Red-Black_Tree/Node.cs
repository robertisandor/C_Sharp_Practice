using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Red_Black_Tree
{
    public enum RBColor { Red, Black };

    public class Node<T>
    {
        public T Value { get; private set; }
        public int Height { get; private set; }
        public int BalanceFactor { get; private set; }
        public RBColor Color { get; set; }

        public Node<T> Parent;
        public Node<T> Left;
        public Node<T> Right;

        // how do I deal with the root if we're not using nil nodes?
        public Node(T value, Node<T> parent = null, RBColor color = RBColor.Red)
        {
            Value = value;
            Height = 1;
            BalanceFactor = 0;
            // by default, nodes should be red when added
            Color = color;
            Parent = parent;
            Left = null;
            Right = null;
        }

        public void Print()
        {
            if(Left != null)
            {
                Left.Print();
            }

            Console.Write(Value);
            if(Color == RBColor.Black)
            {
                Console.WriteLine(" Black");
            }
            else
            {
                Console.WriteLine(" Red");
            }

            if(Right != null)
            {
                Right.Print();
            }
        }

        public void UpdateHeight()
        {
            int leftHeight = Left == null ? 0 : Left.Height;
            int rightHeight = Right == null ? 0 : Right.Height;
            Height = 1 + (rightHeight > leftHeight ? rightHeight : leftHeight);
        }

        public void UpdateBalanceFactor()
        {
            // if I do this in get, it'll only update when I try to get the height
            // it seems awkward if I try to do it in set; what would the value be?
            int leftHeight = Left == null ? 0 : Left.Height;
            int rightHeight = Right == null ? 0 : Right.Height;
            BalanceFactor = 1 + (rightHeight - leftHeight);
        }

        public bool IsNil => Value == null;

    }

    //public static class Node
    //{
    //    public static Node<T> Create<T>(T value) where T : class, IComparable<T>
    //    {
    //        return new Node<T>(value);
    //    }

    //    public static Node<T?> Create<T>(T? value) where T : struct, IComparable<T>
    //    {
    //        return new Node<T?>(value);
    //    }
    //}
}
