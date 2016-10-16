// including the "using System" directive is akin to "using std::cout"
// it's there mainly to reduce the amount of keystrokes needed for Console.WriteLine
// and Console.ReadLine
using System;
// had to add "using" directive for the generics to work
using System.Collections.Generic;

// I can also use the "using" keyword to declare an alias for a namespace

namespace DataStructure
{
  public class Node<T>
  {
    private T data;
    private Node<T> next;
    private Node<T> previous;
  }

  public class DoublyLinkedList<T>
  {
    private Node<T> head;
    private int size;

    // don't need to add <T> to constructors
    public DoublyLinkedList()
    {
      size = 0;
      head = null;
    }
    public DoublyLinkedList(int givenSize)
    {
      size = givenSize;
      head = null;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      DoublyLinkedList<int> list = new DoublyLinkedList<int>(5);
      Console.ReadKey();
    }
  }
}
