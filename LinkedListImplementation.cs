using System;
namespace SimpleDataStructures
{
  public class Node
  {
    public Object data;
    public Node next;
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

    public Node Start
    {
      get
      {
        return head;
      }
    }

    public LinkedList()
    {
      size = 0;
      head = null;
    }

    public void printAllNodes()
    {
      Node traversalNode = head;

      while(traversalNode != null)
      {
        Console.WriteLine("In this node is the data: {0}", traversalNode.data);
        traversalNode = traversalNode.next;
      }
    }

    public void AddFirst(Object inputData)
    {
      var nodeToAdd = new Node()
      {
        data = inputData
      };
      // if the head is empty
      if(head == null)
      {
        head = nodeToAdd;
      }
      else
      {
        var nodeToMove = new Node()
        {
          data = head.data,
          next = head.next
        };

        head = nodeToAdd;
        nodeToAdd.next = nodeToMove;
      }
      size++;
    }
  }

  public class Program
  {
    static void Main(string[] Args)
    {
      LinkedList testList = new LinkedList();
      testList.AddFirst(20);
      testList.printAllNodes();
      testList.AddFirst(30);
      testList.printAllNodes();
      Console.ReadKey();
    }
  }
}
