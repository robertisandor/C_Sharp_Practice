using System;
using System.Collections.Generic;

namespace SimpleDataStructures
{
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

        public bool IsEmpty()
        {
            return (_lastIndex == 0);
        }
    }

    class Program
    {
        public static void Main()
        {

        }
    }        
}