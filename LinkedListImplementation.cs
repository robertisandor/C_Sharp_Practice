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

    public void PrintAllNodes()
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

    public void AddLast(Object inputData)
    {
      Node traversalNode = head;
      var nodeToAdd = new Node()
      {
        data = inputData
      };

      if (head == null)
      {
        head = nodeToAdd;
      }
      else
      {
        while(traversalNode.next != null)
        {
          traversalNode = traversalNode.next;
        }
        traversalNode.next = nodeToAdd;
      }
      size++;
    }

    public bool RemoveNode(int position)
    {
      Node traversalNode = head;
      Node previousNode = null;
      int count = 0;


      if (position == 1)
      {
        head = traversalNode.next;
        size--;
        return true;
      }
      if (head == null)
      {
        Console.WriteLine("List was empty. Nothing to remove.");
        size = 0;
        return true;
      }
      else if(head != null)
      {
        while(traversalNode.next != null && count < size)
        {

          if (count == position - 1)
          {
            previousNode.next = traversalNode.next;
            size--;
            return true;
          }
          count++;
          previousNode = traversalNode;
          traversalNode = traversalNode.next;
        }
      }
      return false;
    }
  }

  public class Program
  {
    static void Main(string[] Args)
    {
      LinkedList testList = new LinkedList();
      testList.AddFirst(20);
      testList.PrintAllNodes();
      testList.AddFirst(30);
      testList.PrintAllNodes();
      testList.AddLast(50);
      testList.PrintAllNodes();
      Console.ReadKey();
    }
  }
}
