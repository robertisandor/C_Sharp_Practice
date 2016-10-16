using System;

class Node
{
  public Node next;
  public Object data;
}

// the reason for implicitly defined variables is more for readability than performance
// if a type name is really long, implicitly defined variables would be best option
// it also allows me to alter the return types of functions without altering other pieces of code
class LinkedList
{
  private Node head;
  private int size;

  public LinkedList()
  {
    size = 0;
    head = null;
  }

  /// <summary>
  /// Prints out the size of the entire list as well as each node on its own line
  /// </summary>
  public void PrintAllNodes()
  {
    Console.WriteLine("This list has {0} nodes.", size);
    Node current = head;
    while(current != null)
    {
      Console.WriteLine(current.data);
      current = current.next;
    }
  }

  /// <summary>
  /// Adds the first node in the list
  /// </summary>
  /// <param name="data">This is a piece of data that will be put into the node</param>
  public void AddFirst(Object data)
  {
    Node toAdd = new Node();
    toAdd.data = data;
    // head == null at this point so we're putting the data in and the next is null
    toAdd.next = head;
    // at this point, we point head to the new object and garbage collection will
    // get rid of the old head
    head = toAdd;
    size++;
  }

  /// <summary>
  /// Appends a node to the end of a list, whether it's empty or not
  /// </summary>
  /// <param name="data">This is a piece of data that will be put into the node</param>
  public void AddLast(Object data)
  {
    if (head == null)
    {
      head = new Node();
      head.data = data;
      head.next = null;
    }
    else
    {
      Node toAdd = new Node();
      toAdd.data = data;

      Node current = head;
      while(current.next != null)
      {
        current = current.next;
      }
      current.next = toAdd;
    }
    size++;
  }

  // I need to go through the list until I find the data I want,
  // then connect the next of the node in front of that to
  // the node after the data
  // would I remove a node based on its contents or its position in the list?

  // the reason I had an error was because I was off-by-one

  /// <summary>
  /// This method removes a node based on its position
  /// </summary>
  /// <param name="position">The node at this position will be removed</param>
  public void RemoveNode(int position)
  {
    if (position == 1)
    {
      head = null;
      size = 0;
    }
    // rather than have a pointer that reaches through the current node
    // to find the next node,
    // C# has two nodes, one to keep track of the current node
    // and one to keep track of the previous node
    else if (position > 1 && position < size)
    {
      Node currentNode = head;
      Node previousNode = null;
      int currentPosition = 0;
      while(currentNode != null)
      {
        if (currentPosition == position - 1)
        {
          previousNode.next = currentNode.next;
          Console.WriteLine("The node at position {0} has been deleted", position);
        }
        previousNode = currentNode;
        currentNode = currentNode.next;
        currentPosition++;
      }
      size--;
    }
  }

  /// <summary>
  /// This function will find the first node with the data specified
  /// </summary>
  /// <param name="data">This is the data to look for</param>
  /// <returns></returns>
  public int Find(Object data)
  {
    int position = -1;
    int currentPosition = 0;
    bool found = false;
    Node dataToLookFor = new Node();
    dataToLookFor.next = head;
    // I need to check for the data; when I find it, return the position
    while(dataToLookFor.next != null && !found)
    {
      if(!data.Equals(dataToLookFor.data))
      {
        dataToLookFor = dataToLookFor.next;
        currentPosition++;
      }
      else
      {
        position = currentPosition;
        found = true;
      }
    }
    return position;
  }
}

class Program
{
  static void Main(string[] args)
  {
    LinkedList firstList = new LinkedList();
    firstList.AddFirst(5);
    firstList.AddLast(6);
    firstList.AddLast(7);
    firstList.AddLast(8);
    firstList.RemoveNode(2);
    Console.WriteLine("The position to find 7 is at: {0}", firstList.Find(7));
    firstList.PrintAllNodes();
  }
}
