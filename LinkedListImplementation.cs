using System;
namespace SimpleDataStructures
{
  public class Node
  {
    private Object data;
    private Node next;
  }

  public class LinkedList
  {
    private Node head;
    private int size;
    public int Count
    {
      get
      {
        return size;
      }
    }

    public LinkedList()
    {
      size = 0;
      head = null;
    }
  }

  public class Program
  {
    static void Main(string[] Args)
    {

      Console.ReadKey();
    }
  }
}
