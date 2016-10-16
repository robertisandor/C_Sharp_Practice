using System;
using System.Collections.Generic;

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
