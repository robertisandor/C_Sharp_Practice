using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_TrieImplementation
{
    // should I make the Trie generic?
    // is it used for more than just letters?
    public class TrieNode
    {
        // each TrieNode should have something that indicates whether it's a complete word or not
        // a bool might be a good idea for that data member...

        // should each Node have a list of other nodes that are its children?
        // should each node have a list of up to 26 children (for the 26 letters in the alphabet?)
        // it seems excessive; it feels like it would become large rather quickly
        // should I have a NodeList that inherits from Collection?

        // I could overload the subscript operator

        // should I have an overriden ToString function?

        // difference between readonly and get; private set members?
        
        // I should probably have Add, IsMember, & Remove operations

        // one implementation has a field that counts how many words have the same prefix
        
        public char Data { get; private set; }
        public bool IsWord { get; private set; }
        public TrieNode Parent { get; private set; }
        public List<TrieNode> Children { get; private set; }
        public int Depth { get; private set; }

        public TrieNode()
        {
            IsWord = false;
            Data = '^';
            Parent = null;
            Children = new List<TrieNode>();
            Depth = 0;
        }

        public TrieNode(char inputCharacter, int depth, TrieNode theParent, bool isWord)
        {
            IsWord = isWord;
            Data = inputCharacter;
            Parent = theParent;
            Children = new List<TrieNode>();
            Depth = depth;
        }

        public TrieNode FindChild(char character)
        {
            foreach (var child in Children)
            {
                if (child.Data == character)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
