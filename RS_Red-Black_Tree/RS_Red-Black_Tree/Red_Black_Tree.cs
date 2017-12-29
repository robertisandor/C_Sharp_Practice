using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Red_Black_Tree
{
    public class Red_Black_Tree<T> where T : IComparable<T>
    {
        // the instructions suck
        // there needs to be a concrete example that the reader can follow
        public Node<T> Root { get; protected set; }
        // internal is C#'s equivalent of the friend class in C++ , however it's limited to the same assembly;
        // it doesn't work so well in different files

        public Red_Black_Tree()
        {
            Root = null;
        }

        public Red_Black_Tree(T value)
        {
            Root = new Node<T>(value, null, RBColor.Black);
        }

        public void Add(T value)
        {
            Add(value, Root);
        }

        private void UpdateNode(Node<T> nodeToUpdate)
        {
            nodeToUpdate.UpdateHeight();
            nodeToUpdate.UpdateBalanceFactor();
        }

        public void Add(T value, Node<T> currentNode)
        {
            if (currentNode == null)
            {
                // would I use Node.Create here?
                // it won't allow me because it says it needs to be a reference type
                // it won't allow me to use Create without the generic T
                currentNode = new Node<T>(value, null, RBColor.Black);
            }
            else
            {
                // when to do the rule check?
                // check immediately after adding a new node
                // check if the parent's color is red

                // what about deletion?

                // why doesn't the Node Value have a CompareTo function?
                // didn't we guarantee that it's IComparable in the static class?
                if (value.CompareTo(currentNode.Value) >= 0)
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = new Node<T>(value, currentNode);
                        InsertRuleCheck(currentNode.Right);
                    }
                    else
                    {
                        Add(value, currentNode.Right);
                    }
                }
                else
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new Node<T>(value, currentNode);
                        InsertRuleCheck(currentNode.Left);
                    }
                    else
                    {
                        Add(value, currentNode.Left);
                    }
                }
            }

            //currentNode.UpdateHeight();
            //currentNode.UpdateBalanceFactor();
            UpdateNode(currentNode);
        }

        public void Delete(T value)
        {
            Delete(value, Root);
        }

        public void Delete(T value, Node<T> currentNode)
        {
            if (currentNode == null)
            {
                throw new Exception("There is nothing to delete.");
            }
            else
            {
                int locationInTree = value.CompareTo(currentNode.Value);
                // check where the item is
                if (locationInTree > 0)
                {
                    // if the value we're looking for is in the right half of the tree,
                    // but the right node doesn't exist...
                    if (currentNode.Right == null)
                    {
                        throw new Exception($"{value} wan't found in this tree.");
                    }
                    // otherwise if the right half of this subtree exists...
                    else
                    {
                        Delete(value, currentNode.Right);
                    }
                }
                else if (locationInTree < 0)
                {
                    // if the value we're looking for is in the left half of the tree,
                    // but the left node doesn't exist...
                    if (currentNode.Left == null)
                    {
                        throw new Exception($"{value} wan't found in this tree.");
                    }
                    else
                    {
                        Delete(value, currentNode.Left);
                    }
                }
                // you found the right item
                else
                {
                    Node<T> childToReplaceDeleted = null;
                    // check how many children the node has

                    bool hasLeftChild = currentNode.Left == null ? false : true;
                    bool hasRightChild = currentNode.Right == null ? false : true;
                    // if it has 0 children, simply delete

                    // consider if the node I'm deleting is root...
                    bool currentNodeIsRoot = currentNode == Root ? true : false;
                    bool currentNodeIsLeftChild = false;
                    if (!currentNodeIsRoot)
                    {
                        if (currentNode.Parent.Left == currentNode)
                        {
                            currentNodeIsLeftChild = true;
                        }
                    }

                    // if it has no children...
                    if (!hasLeftChild && !hasRightChild)
                    {
                        if (!currentNodeIsRoot)
                        {
                            DeleteRuleCheck(currentNode, childToReplaceDeleted);
                        }

                        // and if it isn't root...
                        // cut the parent's tie to the child
                        if (!currentNodeIsRoot && currentNodeIsLeftChild)
                        {
                            currentNode.Parent.Left = null;
                        }
                        else
                        {
                            currentNode.Parent.Right = null;
                        }
                        // whether it's root or not, get rid of the node to be deleted
                        currentNode = null;
                    }
                    else
                    {
                        // if it has 1 child that is the right child...
                        if (!hasLeftChild && hasRightChild)
                        {
                            // if the currentNode is root...
                            if (currentNodeIsRoot)
                            {
                                childToReplaceDeleted = currentNode.Right;
                                Root = currentNode.Right;
                                currentNode = null;
                            }
                            // otherwise...
                            else
                            {
                                childToReplaceDeleted = currentNode.Left;
                                DeleteRuleCheck(currentNode, childToReplaceDeleted);
                                if (currentNodeIsLeftChild)
                                {
                                    currentNode.Parent.Left = currentNode.Right;
                                }
                                else
                                {
                                    currentNode.Parent.Right = currentNode.Right;
                                }
                                currentNode.Right.Parent = currentNode.Parent;
                                currentNode = null;
                            }
                        }
                        // if it has 1 child that is the left child...
                        else if (hasLeftChild && !hasRightChild)
                        {
                            if (currentNodeIsRoot)
                            {
                                Root = currentNode.Left;
                                currentNode = null;
                            }
                            // otherwise...
                            else
                            {
                                childToReplaceDeleted = currentNode.Left;
                                DeleteRuleCheck(currentNode, childToReplaceDeleted);
                                if (currentNodeIsLeftChild)
                                {
                                    currentNode.Parent.Left = currentNode.Left;
                                }
                                else
                                {
                                    currentNode.Parent.Right = currentNode.Left;
                                }
                                currentNode.Left.Parent = currentNode.Parent;
                                currentNode = null;
                            }
                        }
                        // if it has 2 children...
                        else
                        {
                            // take the biggest value from the left branch
                            // by starting at the left then going down as far right as possible
                            Node<T> traversalNode = currentNode.Left;
                            while (traversalNode.Right != null)
                            {
                                traversalNode = traversalNode.Right;
                            }
                            // if the biggest value has a left child...
                            if (traversalNode.Left != null)
                            {
                                traversalNode.Left.Parent = traversalNode.Parent;
                                traversalNode.Parent.Left = traversalNode.Left;
                            }
                            traversalNode.Parent = currentNode.Parent;
                            // what if the currentNode.Left == traversalNode?
                            // this is to cover the case when the biggest node 
                            // is the node immediately to the left of the node to delete
                            if (currentNode.Left != traversalNode)
                            {
                                traversalNode.Left = currentNode.Left;
                                currentNode.Left.Parent = traversalNode;
                            }
                            if (currentNode.Right != traversalNode)
                            {
                                traversalNode.Right = currentNode.Right;
                                currentNode.Right.Parent = traversalNode;
                            }
                            // childToReplaceDeleted is going to be traversalNode
                            childToReplaceDeleted = traversalNode;
                            DeleteRuleCheck(currentNode, childToReplaceDeleted);
                            // this cuts the tie of the parent to the child if there is a parent
                            if (!currentNodeIsRoot)
                            {
                                if (currentNodeIsLeftChild)
                                {
                                    currentNode.Parent.Left = traversalNode;
                                }
                                else
                                {
                                    currentNode.Parent.Right = traversalNode;
                                }
                            }
                            else
                            {
                                Root = traversalNode;
                            }
                            currentNode = null;
                        }
                    }


                }
                if (currentNode != null)
                {
                    //currentNode.UpdateHeight();
                    //currentNode.UpdateBalanceFactor();
                    UpdateNode(currentNode);
                }
            }
        }

        public void Print()
        {
            if (Root == null)
            {
                // Console.WriteLine("There is nothing to print.");
                throw new Exception("There is nothing to print.");
            }
            else
            {
                Root.Print();
            }
        }

        private Node<T> determineUncle(Node<T> grandparent, Node<T> currentNode)
        {
            if (grandparent != null)
            {
                // if the current node is the left child, the uncle is the right child
                if (grandparent.Left == currentNode.Parent)
                {
                    return grandparent.Right;
                }
                // if the current node is the right child, the uncle is the left child
                else
                {
                    return grandparent.Left;
                }
            }
            return null;
        }

        private void InsertRuleCheck(Node<T> currentNode)
        {
            // if this is the root
            if (currentNode.Parent == null)
            {
                currentNode.Color = RBColor.Black;
                return;
            }
            // if the parent is NOT BLACK (aka RED) or if the current node isn't the root, which we already checked for
            Node<T> grandparent = currentNode.Parent.Parent;

            // if the parent is red...
            if (currentNode.Parent.Color == RBColor.Red)
            {
                Node<T> uncle = null;
                //if (grandparent != null)
                //{
                //    // if the current node is the left child, the uncle is the right child
                //    if (grandparent.Left == currentNode.Parent)
                //    {
                //        uncle = grandparent.Right;
                //    }
                //    // if the current node is the right child, the uncle is the left child
                //    else
                //    {
                //        uncle = grandparent.Left;
                //    }
                //}
                uncle = determineUncle(grandparent, currentNode);

                // this portion should be fine
                // this only happens if the uncle exists and is red
                if (uncle != null && uncle.Color == RBColor.Red)
                {
                    currentNode.Parent.Color = RBColor.Black;
                    uncle.Color = RBColor.Black;
                    grandparent.Color = RBColor.Red;
                    InsertRuleCheck(grandparent);
                }
                // either the uncle is null, which means it's black by default
                // or it's not red... which also means it's black
                else
                {
                    bool parentIsLeftChild = false;
                    bool currentNodeIsLeftChild = false;
                    if (grandparent != null && grandparent.Left == currentNode.Parent)
                    {
                        parentIsLeftChild = true;
                    }

                    if (currentNode == currentNode.Parent.Left)
                    {
                        currentNodeIsLeftChild = true;
                    }

                    if (parentIsLeftChild)
                    {
                        if (currentNodeIsLeftChild)
                        {
                            // rotate grandparent right
                            rightRotation(currentNode.Parent);
                            // this check is needed to make sure the new parent points to their new child
                            // (the one  that was a grandchild but was rotated and became their child)
                            // it's only necessary once the tree gets to height 3/4
                            if (currentNode.Parent != null && currentNode.Parent.Parent != null)
                            {
                                currentNode.Parent.Parent.Right = currentNode.Parent;
                            }
                            swapColor(currentNode.Parent, grandparent);
                        }
                        else
                        {
                            // rotate parent left
                            leftRotation(currentNode);
                            // then rotate grandparent right
                            rightRotation(currentNode);
                            // if the tree is height 4 or greater 
                            // we need to make sure the parent of the new pivot
                            // actually thinks of the new pivot as the child, not the old pivot
                            if (currentNode.Parent != null)
                            {
                                currentNode.Parent.Right = currentNode;
                            }

                            swapColor(currentNode, currentNode.Right);
                        }
                    }
                    else
                    {
                        if (currentNodeIsLeftChild)
                        {
                            // rotate parent right
                            rightRotation(currentNode);
                            // then rotate grandparent left
                            leftRotation(currentNode);
                            // if the tree is height 4 or greater 
                            // we need to make sure the parent of the new pivot
                            // actually thinks of the new pivot as the child, not the old pivot
                            if (currentNode.Parent != null)
                            {
                                currentNode.Parent.Left = currentNode;
                            }

                            swapColor(currentNode, currentNode.Left);
                        }
                        else
                        {
                            // rotate grandparent left
                            leftRotation(currentNode.Parent);
                            // this check is needed to make sure the new parent points to their new child
                            // (the one  that was a grandchild but was rotated and became their child)
                            // it's only necessary once the tree gets to height 3/4
                            if (currentNode.Parent != null && currentNode.Parent.Parent != null)
                            {
                                currentNode.Parent.Parent.Right = currentNode.Parent;
                            }
                            swapColor(currentNode.Parent, grandparent);
                        }
                    }
                }
            }
            Root.Color = RBColor.Black;
        }

        private Node<T> determineSibling(Node<T> nodeToBeDeleted)
        {
            if (nodeToBeDeleted.Parent.Left == nodeToBeDeleted)
            {
                return nodeToBeDeleted.Parent.Right;
            }
            else
            {
                return nodeToBeDeleted.Parent.Left;
            }
        }

        // should this be the function signature?
        private void DeleteRuleCheck(Node<T> nodeToBeDeleted, Node<T> childToReplaceDeleted)
        {
            // if the nodeToBeDeleted is red or the childToReplaceDeleted is red
            if (nodeToBeDeleted.Color == RBColor.Red || (childToReplaceDeleted != null && childToReplaceDeleted.Color == RBColor.Red))
            {
                if (childToReplaceDeleted != null)
                {
                    childToReplaceDeleted.Color = RBColor.Black;
                }
            }
            else
            {
                // how do we mark something as double black?
                Node<T> sibling = null;
                // figure out what the sibling is, if it exists
                if (nodeToBeDeleted.Parent != null)
                {
                    sibling = determineSibling(nodeToBeDeleted);
                }
                // otherwise if the parent is null, then this is the root
                // we make sure the root is black and we exit the function
                else
                {
                    nodeToBeDeleted.Color = RBColor.Black;
                    return;
                }
                bool siblingIsLeftChild = (sibling.Parent.Left == sibling) ? true : false;

                // if the sibling is black...
                if (sibling != null || sibling.Color == RBColor.Black)
                {

                    // check if either of the sibling's children are red
                    // do one of the 4 rotations upon the sibling depending upon the position
                    if (sibling.Left != null && sibling.Left.Color == RBColor.Red)
                    {
                        // figure out which one of the 2 rotations to do...
                        if (siblingIsLeftChild)
                        {
                            // simple right rotation?
                            rightRotation(sibling);
                        }
                        else
                        {
                            // left-right rotation?
                            leftRotation(sibling);
                            rightRotation(sibling);
                        }
                    }
                    else if (sibling.Right != null && sibling.Right.Color == RBColor.Red)
                    {
                        // figure out which one of the 2 rotations to do...
                        if (siblingIsLeftChild)
                        {
                            // right-left rotation?
                            rightRotation(sibling);
                            leftRotation(sibling);
                        }
                        else
                        {
                            // simple left rotation?
                            leftRotation(sibling);
                        }
                    }
                    // but if neither of the children are red...
                    else
                    {
                        sibling.Color = RBColor.Red;
                        // set parent to double black? what would I do?
                        sibling.Parent.Color = RBColor.Black;
                        // TODO: figure out what to do here
                        DeleteRuleCheck(nodeToBeDeleted.Parent, nodeToBeDeleted);
                    }
                }
                // if the sibling is red
                else
                {
                    // do a single rotation, recolor old sibling and old parent. The new sibling is always black. 
                    // This mainly converts the tree to black sibling case (by rotation) and leads to case (a) or (b).
                    if (siblingIsLeftChild)
                    {
                        rightRotation(sibling);
                    }
                    else
                    {
                        leftRotation(sibling);
                    }
                    // recolor old sibling and parent
                    // how would I figure out what colors they should be?
                    // TODO: figure out what to do here
                    sibling.Color = RBColor.Black;
                    sibling.Parent.Color = RBColor.Red;
                    // new sibling is black
                }
            }
            // check color of the sibling, not the uncle

            // double black = black node is deleted and replaced by black child
            // convert double black to single black

            // when deleting a node, the node that will ACTUALLY be deleted always has 1 child
            // (in the case of 2 children, the rightmost child of the left branch becomes deleted 
            // after its value is swapped with the node that was intended to be deleted)

            // nodeToBeDeleted and childToReplaceDeleted
            // if either nodeToBeDeleted or childToReplaceDeleted is red,
            // mark childToReplaceDeleted as black 

            // if both nodeToBeDeleted and childToReplaceDeleted are black,
            // color childToReplaceDeleted as double black? Do I need to add something to the enum?
            // how would I mark it as double black? have a boolean?

            // while the childToBeReplaceDeleted is double black or isn't root,
            // (a) If sibling s is black and at least one of the sibling's children is red, perform rotations on the sibling. 
            // Let the red child of s be r. This case can be divided into 4 subcases depending upon positions of s and r. 
            // (This should remind you of AVL double rotates and the cases in InsertRuleCheck).

            //      (i)Left Right Case
            //     (ii) Left Left Case
            //     (iii) Right Left Case
            //     (iv) Right Right Case

            // (b) If sibling is black and both its' children are black, perform recoloring. 
            // Set sibling to red and parent to double black and recur for the parent. 
            // If parent was red, then we don't need to recur since red +double black = single black.


            // (c) If sibling is red, perform a single rotation to move sibling up, recolor old sibling and old parent.
            // The new sibling is always black. This mainly converts the tree to black sibling case (by rotation) and leads to case (a)or(b).

            // if childToReplaceDeleted is root, make it single black and exit the function
        }

        private void leftRotation(Node<T> pivot)
        {
            Node<T> pivotParent = pivot.Parent;
            pivotParent.Right = pivot.Left;
            pivot.Parent = pivotParent.Parent;
            pivotParent.Parent = pivot;
            pivot.Left = pivotParent;

            if (pivotParent == Root)
            {
                Root = pivot;
            }
        }

        private void rightRotation(Node<T> pivot)
        {
            Node<T> pivotParent = pivot.Parent;
            pivotParent.Left = pivot.Right;
            pivot.Parent = pivotParent.Parent;
            pivotParent.Parent = pivot;
            pivot.Right = pivotParent;

            if (pivotParent == Root)
            {
                Root = pivot;
            }
        }

        private void swapColor(Node<T> parent, Node<T> grandparent)
        {
            RBColor parentColorHolder = parent.Color;
            parent.Color = grandparent.Color;
            grandparent.Color = parentColorHolder;
        }
    }

    // this is the factory pattern which determines which version of the Red_Black_Tree to create
    //public static class Red_Black_Tree
    //{
    //    public static Red_Black_Tree<T> Create<T>(T value) where T : class, IComparable<T>
    //    {
    //        var rootNode = Node.Create(value);
    //        return new Red_Black_Tree<T>(rootNode);
    //    }

    //    public static Red_Black_Tree<T?> Create<T>(T? value) where T : struct, IComparable<T>
    //    {
    //        var rootNode = Node.Create(value);
    //        return new Red_Black_Tree<T?>(rootNode);
    //    }
    //}
}
