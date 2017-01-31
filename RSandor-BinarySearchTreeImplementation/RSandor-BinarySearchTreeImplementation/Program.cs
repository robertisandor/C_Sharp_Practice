using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_BinarySearchTreeImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> practiceTree = new BinarySearchTree<int>();
            practiceTree.Delete(11);
            practiceTree.Add(4);
            practiceTree.Add(6);
            practiceTree.Add(5);
            practiceTree.Add(7);
            practiceTree.Add(9);
            practiceTree.Add(8);
            practiceTree.Add(10);
            practiceTree.Add(2);
            practiceTree.Add(1);
            practiceTree.Add(3);
            practiceTree.Add(13);
            practiceTree.Add(12);
            practiceTree.Add(11);
            practiceTree.Add(14);
            practiceTree.Add(16);
            practiceTree.Add(15);
            practiceTree.Add(16);
            practiceTree.Add(16);
            practiceTree.Add(16);
            practiceTree.Add(16);
            Console.WriteLine(practiceTree.Search(6));
            practiceTree.Print(practiceTree.Root);
            practiceTree.Delete(20);
            practiceTree.Delete(4);
            practiceTree.Delete(10);
            practiceTree.Delete(8);
            practiceTree.Delete(6);
            practiceTree.Delete(13);
            practiceTree.Delete(12);
            practiceTree.Print(practiceTree.Root);
            Console.ReadKey();
        }
    }
}
