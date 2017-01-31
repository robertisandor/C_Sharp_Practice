using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_TrieImplementation
{
    public class Trie
    {
        // given a list of 50K words, I should be able to look through and check if a typed word
        // is included within the list within 0.5 sec
        // the entire Trie should have a root node
        // I notice that the root node is empty; could this be readonly?
        // readonly - no one can change this member once instantiated
        // const - not really used by variables in objects; used more by variables outside of objects
        // if I wanted to have only the class itself change a member, then I would use a public variable
        // with a public get and no set 
        public readonly TrieNode Root;

        public Trie()
        {
            Root = new TrieNode();
        }

        // when inserting something, I would just pass the string
        // and iterate through it, character by character
        // adding nodes if necessary
        // first, I have to determine if there's a common prefix
        // so I wouldn't need to add nodes for the letters that I have already input

        // when inserting, I should probably sanitize the input to make sure it isn't case-sensitive.
        // what about input that has other symbols, like hyphens or numbers?

        public void Insert(string inputString)
        {
            // this should be changed once I figure out the SanitizeInput function
            string sanitizedString = SanitizeInput(inputString);
            // the currentNode will iterate through the list
            var currentLetter = FindCommonPrefix(sanitizedString);
            // I found the common prefix; 
            // once I find it, I need to make sure that only letters
            // beyond the prefix are added 
            var commonPrefix = currentLetter;

            // this loop will iterate through the string, starting from the common prefix
            // and add new nodes for all of the new letters that will be added
            for (int index = currentLetter.Depth; index < sanitizedString.Length; index++)
            {
                // I should be checking if the currentNode is equivalent to the common prefix
                TrieNode newLetter;
                if (index == sanitizedString.Length - 1)
                {
                    newLetter = new TrieNode(sanitizedString[index], currentLetter.Depth + 1, currentLetter, true);
                }
                else
                {
                    newLetter = new TrieNode(sanitizedString[index], currentLetter.Depth + 1, currentLetter, false);
                }
                
                currentLetter.Children.Add(newLetter);
                currentLetter = newLetter;
            }
        }

        public TrieNode FindCommonPrefix(string inputString)
        {
            // start at the root
            var currentNode = Root;
            var result = currentNode;

            // for every character in the string, find the child
            // this iterates through the trie, checking if the current letter
            // has a child that is the next letter
            // if the currentNode doesn't have the next letter as a child, it returns null
            foreach (char character in inputString)
            {
                currentNode = currentNode.FindChild(character);
                // is there a more concise way of checking if something is null?
                // perhaps something in C# 6
                if (currentNode == null)
                {
                    break;
                }
                result = currentNode;
            }

            return result;
        }

        // since this really isn't a function for the trie, where should I put this?
        // I want the Insert function to use it
        // do I keep it inside the namespace, but outside of the Trie?
        // I can't just put it directly into the namespace...

        // I should check if it's null first, then
        // I should trim inputString, make inputString all lowercase, then remove any extraneous symbols, then check if it's an empty string
        public string SanitizeInput(string inputString)
        {
            // why would I rethrow an exception vs generating a new exception?
            // we can also change the exception, but why?
            // throwing a named exception variable changes the stack trace. Why would I want to do that?
            // I'm not supposed to throw NullReferenceException from my own code. Why?
            if (inputString == null)
            {
                // throw new NullReferenceException("The input string can't be null.");
            }
            string sanitizedString = inputString.ToLower().Trim();

            // I need to iterate through each character to see if it's a letter;
            // if it isn't, I need to remove it
            // can't use foreach loop to remove or add characters; I must use a for loop
            for (int index = 0; index < sanitizedString.Length; index++)
            {
                if (sanitizedString[index] < 'a' || sanitizedString[index] > 'z')
                {
                    sanitizedString = sanitizedString.Remove(index, 1);
                }
            }

            // why can't I use IsNullOrEmpty()? 
            // I don't think I need to do anything if I have an empty string
            return sanitizedString;
        }
    }
}
