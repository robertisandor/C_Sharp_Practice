using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    #region Abstract heap class
    public abstract class Heap<T> : IEnumerable<T>
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

        protected Comparer<T> Comparer { get; private set; }
        #endregion

        #region Abstract method
        protected abstract bool Dominates(T x, T y);
        #endregion 

        #region Heap constructors
        protected Heap() : this(Comparer<T>.Default)
        {
        }

        protected Heap(Comparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
        {
        }

        protected Heap(IEnumerable<T> collection) : this(collection, Comparer<T>.Default)
        {
        }

        protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if(collection == null)
            {
                throw new ArgumentNullException("Collection is and should not be null.");
            }

            Comparer = comparer ?? throw new ArgumentNullException("Comparer is and shoult not be null.");

            foreach(var item in collection)
            {
                if(Count == Capacity)
                {
                    Grow();
                }

                heap[tail] = item;
                tail++;
            }

            for(int index = parent(tail - 1); index >= 0; index--)
            {
                SiftUp(index);
            }
        }
        #endregion 

        public void Add(T item)
        {
            if(Count == Capacity)
            {
                Grow();
            }
            heap[tail] = item;
            tail++;
           
            siftUp(tail - 1);
        }

        private void siftUp(int index)
        {
            if(index == 0 || Dominates(heap[parent(index)], heap[index]))
            {
                return;
            }

            swap(index, parent(index));
            siftUp(parent(index));
        }

        private void siftDown(int index)
        {
            int dominatingNode = Dominating(index);
            if(dominatingNode == index)
            {
                return;
            }
            swap(index, dominatingNode);
            siftDown(dominatingNode);
        }

        private int Dominating(int index)
        {
            int dominatingNode = index;

            dominatingNode = GetDominating(leftChild(index), dominatingNode);
            dominatingNode = GetDominating(rightChild(index), dominatingNode);

            return dominatingNode;
        }

        private int GetDominating(int newNode, int dominatingNode)
        {
            if(newNode < tail && !Dominates(heap[dominatingNode], heap[newNode]))
            {
                return newNode;
            }
            else
            {
                return dominatingNode;
            }
        }

        private void swap(int firstIndex, int secondIndex)
        {
            T tempHolder = heap[firstIndex];
            heap[firstIndex] = heap[secondIndex];
            heap[secondIndex] = tempHolder;
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

        #region IEnumerable interface implementation
        public IEnumerator<T> GetEnumerator()
        {
            return heap.Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion 
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
