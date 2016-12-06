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
  public class Node<T>
  {
    public T data;
    public Node<T> next;
    public Node<T> previous;
  }

  public class DoublyLinkedList<T>
  {
    private Node<T> head;
    public Node<T> Head
    {
      get
      {
        return head;
      }
    }
    private int size;
    public int Size
    {
      get
      {
        return size;
      }
    }

    // don't need to add <T> to constructors - why again?
    public DoublyLinkedList()
    {
      size = 0;
      head = null;
    }

    public void AddToFront(T info)
    {
      var nodeToAdd = new Node<T>
      {
        data = info,
        previous = null,
        next = null
      };
      if(head == null)
      {
        head = nodeToAdd;
      }
      else
      {
        var nodeToMove = new Node<T>
        {
          data = head.data,
          next = head.next,
          previous = nodeToAdd
        };

        head = nodeToAdd;
        nodeToAdd.next = nodeToMove;
      }
      size++;
    }

    public void AddToEnd(T info)
    {
      
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      DoublyLinkedList<int> list = new DoublyLinkedList<int>();
      list.AddToFront(1);
      Console.ReadKey();
    }
  }
}
