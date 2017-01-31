// including the "using System" directive is akin to "using std::cout"
// it's there mainly to reduce the amount of keystrokes needed for Console.WriteLine
// and Console.ReadLine
using System;
// had to add "using" directive for the generics to work
// should use generics instead of Object, because Object requires casting
using System.Collections.Generic;

// I can also use the "using" keyword to declare an alias for a namespace

namespace SimpleDataStructures
{
    public class DoublyLinkedList<T>
    {
        // am I implementing a nested class correctly?
        // I'm getting a warning related to 
        // the templated parameter of Node - should it be templated?
        // Yes, it should be templated; however, it shouldn't have the same parameter
        // as the class it's nested in
        public class Node<T2>
        {
            public T2 data;
            public Node<T2> next;
            public Node<T2> previous;
        }

        // is the head a separate Node that doesn't contain a value?
        // or is it the first node in the list?
        private Node<T> _head;
        public Node<T> Head
        {
            get
            {
                return _head;
            }
        }

        // I've provided accessors but not mutators
        // because I don't want the ability to change the head, tail, or size
        // outside of the class
        private Node<T> _tail;
        public Node<T> Tail
        {
            get
            {
                return _tail;
            }
        }

        private int _size;
        public int Size
        {
            get
            {
                return _size;
            }
        }

        // don't need to add <T> to constructors - why again?
        public DoublyLinkedList()
        {
            _size = 0;
            _head = null;
            _tail = null;
        }

        // overloaded index operator
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size)
                {
                    throw new ArgumentOutOfRangeException("Out of range!");
                }
                Node<T> traversalNode = this.Head;
                for (int currentIndex = 0; currentIndex < index; currentIndex++)
                {
                    traversalNode = traversalNode.next;
                }
                return traversalNode.data;
            }
            set
            {
                if (index < 0 || index >= Size)
                {
                    throw new ArgumentOutOfRangeException("Out of range!");
                }
                Node<T> currentNode = this.Head;
                for (int currentIndex = 0; currentIndex < index; currentIndex++)
                {
                    currentNode = currentNode.next;
                }
                currentNode.data = value;
            }
        }

        public void Append(T info)
        {
            var nodeToAdd = new Node<T>
            {
                data = info,
                previous = null,
                next = null
            };

            if (_tail == null)
            {
                _tail = _head = nodeToAdd;
            }
            else
            {
                nodeToAdd.previous = _tail;
                _tail.next = nodeToAdd;
                _tail = nodeToAdd;
            }

            _size++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public bool Contains(T info)
        {
            int index = IndexOf(info);
            return (index != -1);
        }

        public int IndexOf(T info)
        {
            int currentIndex = 0;
            bool found = false;
            Node<T> traversalNode = _head;
            while (currentIndex < _size && traversalNode != null && !found)
            {
                // EqualityComparer is best used here. Why would overloading the operator ==
                // or using .Equals() not be the best choices?
                if (EqualityComparer<T>.Default.Equals(info, traversalNode.data))
                {
                    found = true;
                }
                else
                {
                    traversalNode = traversalNode.next;
                    currentIndex++;
                }
            }

            // In case it's not in the list, give a negative index and tell the user
            if (currentIndex >= _size)
            {
                currentIndex = -1;
                Console.WriteLine("That isn't in the list.");
            }
            return currentIndex;
        }

        public void Insert(T info, int index)
        {
            
            if (index < 0 || index >= Size)
            {
                throw new ArgumentOutOfRangeException("Out of range!");
            }

            var nodeToAdd = new Node<T>
            {
                data = info,
                next = null,
                previous = null
            };

            int currentIndex = 0;
            Node<T> currentNode = _head;
            Node<T> previousNode = null;

            while (currentIndex < index)
            {
                previousNode = currentNode;
                currentNode = currentNode.next;
                currentIndex++;
            }

            if (index == 0)
            {
                nodeToAdd.previous = _head.previous;
                nodeToAdd.next = _head;
                _head.previous = nodeToAdd;
                _head = nodeToAdd;
            }
            else if (index == Size - 1)
            {
                previousNode.next = nodeToAdd;
                nodeToAdd.previous = previousNode;
                nodeToAdd.next = _tail = currentNode;
                _tail.previous = nodeToAdd;
            }
            else
            {
                nodeToAdd.previous = previousNode;
                previousNode.next = nodeToAdd;
                nodeToAdd.next = currentNode;
                currentNode.previous = nodeToAdd;
                //nodeToAdd.next = previousNode.next;
                //previousNode = nodeToAdd;
                //nodeToAdd.previous = currentNode.previous;
                //currentNode.previous = nodeToAdd;
            }
            _size++;
        }

        public void RemoveAt(int position)
        {
            if (position < 0 || position >= Size)
            {
                throw new ArgumentOutOfRangeException("Out of range!");
            }

            Node<T> currentNode = Head;
            Node<T> previousNode = null;
            for (int currentIndex = 0; currentIndex < position; currentIndex++)
            {
                previousNode = currentNode;
                currentNode = currentNode.next;
            }

            if (previousNode == null)
            {
                _head = currentNode.next;
                _head.previous = null;
            }
            else if (position == _size - 1)
            {
                previousNode.next = currentNode.next;
                _tail = previousNode;
                currentNode = null;
            }
            else
            {
                previousNode.next = currentNode.next;
                currentNode.next.previous = previousNode;
            }
            if (_size > 0)
            {
                _size--;
            }
        }

        public void PrintAllNodes()
        {
            Node<T> currentNode = new Node<T>
            {
                data = Head.data,
                next = Head.next,
                previous = Head.previous
            };

            while (currentNode != null)
            {
                Console.WriteLine(currentNode.data);
                currentNode = currentNode.next;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();
            list.Append(3);
            list.Insert(1, 0);
            list.Append(5);
            list.Insert(2, 0);
            list.PrintAllNodes();
            Console.WriteLine("The index of the data, 5, is {0}", list.IndexOf(5));
            Console.WriteLine("It's {0} that the list contains 1", list.Contains(1));
            Console.WriteLine("It's {0} that the list contains 8", list.Contains(8));
            list.RemoveAt(0);
            list.RemoveAt(1);
            list.PrintAllNodes();
            Console.ReadKey();
        }
    }
}