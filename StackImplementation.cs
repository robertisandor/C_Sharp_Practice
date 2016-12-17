using System;
using System.Collections.Generic;

namespace SimpleDataStructures
{
    // I've seen someone use an interface,
    // then have the class from the interface
    // is this customary? is this a preferable implementation?

    // I've also seen an implementation that uses an IEnumerator
    // to support foreach
    // then, in the same implementation with the IEnumerator,
    // they use a Contains method that utilizes the foreach

    // should I create a private helper function to automatically resize the array?
    // I could use it for both the Push & Pop methods

    public class Stack<T>
    {
        private int _lastIndex;
        public int LastIndex
        {
            get
            {
                return _lastIndex;
            }
        }

        private T[] _stack;
        public Stack()
        {
            _lastIndex = 0;
            _stack = new T[10];
        }

        public Stack(int initialCapacity)
        {
            if(initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException("Out of range!");
            }   
            _stack = new T[initialCapacity];
            _lastIndex = 0;
        }

        public void Clear()
        {
            _lastIndex = 0;
            _stack[_lastIndex] = default(T);
        }

        public bool IsEmpty()
        {
            return (_lastIndex == 0);
        }

        public T Peek()
        {
            if(_lastIndex == 0)
            {
                return default(T);
            }
            return _stack[_lastIndex - 1];
        }

        public T Pop()
        {
            // in case the stack is empty, throw an error
            if(_lastIndex == 0)
            {
                throw new InvalidOperationException("The stack is empty.");
            }
            T data  = _stack[--_lastIndex];
            // why is the default keyword a good idea here?
            // it basically sets a default value based upon the type
            // null - if it's a reference type or 0 if it's a value type
            _stack[_lastIndex] = default(T);
            return data;
        }

        public void Push(T data)
        {
            // use .Length property of array to figure out capacity
            if(_lastIndex + 1 >= _stack.Length)
            {
                Array.Resize(ref _stack, (_stack.Length + 1) * 2);
            }
            _stack[_lastIndex] = data;
            _lastIndex++;
        }
    }

    class Program
    {
        public static void Main()
        {

        }
    }        
}