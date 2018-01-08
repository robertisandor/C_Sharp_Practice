using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    #region Abstract heap class
    public abstract class Heap<T> 
    {
        #region Class variables
        private const int initialCapacity = 0;
        private const int growthRate = 2;
        private const int minGrowth = 1;
        private int tail = 0;

        private T[] heap = new T[initialCapacity];
        #endregion

        #region Class properties
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        #endregion 

        public Heap()
        {

        }

        public void Add(T item)
        {
            if(Count == Capacity)
            {
                Grow();
            }
            heap[tail] = item;
            tail++;
            // what exactly does this do?
            Heapify(tail - 1);
        }

        private void Heapify(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the parent of a given index
        /// </summary>
        /// <param name="index">Index of the child</param>
        /// <returns>Index of the parent</returns>
        private static int parent(int index)
        {
            return (index + 1) / 2 - 1;
        }

        /// <summary>
        /// Finds the left child of a given index
        /// </summary>
        /// <param name="index">Index of the parent</param>
        /// <returns>Index of the left child</returns>
        private static int leftChild(int index)
        {
            return (index * 2) + 1;
        }

        /// <summary>
        /// Finds the right child of a given index
        /// </summary>
        /// <param name="index">Index of the parent</param>
        /// <returns>Index of the right child</returns>
        private static int rightChild(int index)
        {
            return leftChild(index) + 1;
        }

        /// <summary>
        /// Helper function to expand the heap
        /// </summary>
        private void Grow()
        {
            int newCapacity = Capacity * growthRate + minGrowth;
            var newHeap = new T[newCapacity];
            Array.Copy(heap, newHeap, Capacity);
            heap = newHeap;
            Capacity = newCapacity;
        }
    }

    #endregion

    #region MinHeap class
    public class MinHeap<T> : Heap<T>
    {
        
    }
    #endregion

    #region MaxHeap class
    public class MaxHeap<T> : Heap<T>
    {

    }
    #endregion 
}
