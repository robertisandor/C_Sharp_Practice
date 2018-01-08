using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public abstract class Heap<T> 
    {
        private const int initialCapacity = 0;
        private const int growthRate = 2;
        private const int minGrowth = 1;
        private int tail = 0;

        private T[] heap = new T[initialCapacity];

        public int Count { get; private set; }
        public int Capacity { get; private set; }

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

        private static int parent(int index)
        {
            return (index + 1) / 2 - 1;
        }

        private static int leftChild(int index)
        {
            return (index * 2) + 1;
        }

        private static int rightChild(int index)
        {
            return leftChild(index) + 1;
        }

        private void Grow()
        {
            int newCapacity = Capacity * growthRate + minGrowth;
            var newHeap = new T[newCapacity];
            Array.Copy(heap, newHeap, Capacity);
            heap = newHeap;
            Capacity = newCapacity;
        }
    }

    public class MinHeap<T> : Heap<T>
    {
        
    }

    public class MaxHeap<T> : Heap<T>
    {

    }
}
