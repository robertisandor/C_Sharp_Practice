using System;

public class Node
{
  private Object data;
  private Node next;
}

public class LinkedList
{
  private Node head;
  private int size;

  public LinkedList()
  {
    size = 0;
    head = null;
  }
}
