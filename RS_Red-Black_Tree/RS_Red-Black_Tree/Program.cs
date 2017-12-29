using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Red_Black_Tree;

namespace RS_Red_Black_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Red_Black_Tree<int>(50);
            tree.Add(25);
            tree.Add(0);
            tree.Add(-25);
            tree.Add(37);
            tree.Add(40);
            tree.Add(100);
            tree.Add(125);
            tree.Print();

            try
            {
                tree.Delete(1);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error handling is working!");
            }
            tree.Delete(125);
            Console.WriteLine("After deleting 125, the tree is now: ");
            tree.Print();

            tree.Delete(100);
            Console.WriteLine("After deleting 100, the tree is now: ");
            tree.Print();

            tree.Delete(25);
            Console.WriteLine("After deleting 25, the tree is now: ");
            tree.Print();

            tree.Delete(40);
            Console.WriteLine("After deleting 40, the tree is now: ");
            tree.Print();

            var testTree = new Red_Black_Tree<int>(15);
            for (int input = 0; input < 10; input++)
            {
                testTree.Add(input);
            }
            Console.WriteLine("testTree is now: ");
            testTree.Print();

            Console.ReadKey();
        }
    }
}
