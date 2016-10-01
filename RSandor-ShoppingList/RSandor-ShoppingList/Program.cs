using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_ShoppingList
{
    class Program
    {
        static void Main(string[] args)
        {
            int initialSize;
            String userInput;
            bool validInput;
            Console.WriteLine("This is the shopping list program. Please give me the initial size of your shopping list. Initial size: ");
            userInput = Console.ReadLine();
            validInput = Int32.TryParse(userInput, out initialSize);
            //Console.WriteLine("validInput = {0}, userInput = {1}, initialSize = {2}", validInput, userInput, initialSize);

            Console.WriteLine("Please start inputting the items on your shopping list.");
            String[] shoppingList = new String[initialSize];

            for (int index = 0; index < initialSize; index++)
            {
                Console.Write("Shopping list item #{0}: ", index+1);
                shoppingList[index] = Console.ReadLine();
            }

            char userOption;
            Console.WriteLine("Would you like to add or remove something to the list, view the entire list, or exit the program?");
            int modifiedSize = initialSize;
            do {
                Console.WriteLine("Please input 'a' for add, 'r' for remove, 'v' for view or 'e' to exit: ");
                userOption = Console.ReadKey().KeyChar;
                if (userOption == 'v')
                {
                    // perhaps change initialSize to modifiedSize
                    for (int index = 0; index < modifiedSize; index++)
                    {
                        Console.WriteLine("Shopping list item #{0} = {1}", (index+1), shoppingList[index]);
                    }
                }
                else if (userOption == 'e')
                {
                    Console.WriteLine("\nYou are exiting the program now. Goodbye.");
                }
                else if (userOption == 'a')
                {
                    // need a different size variable for the modified size
                    String[] tempArray = new String[++modifiedSize];
                    for (int index = 0; index < modifiedSize-1; index++)
                    {
                        tempArray[index] = shoppingList[index];
                    }
                    Console.WriteLine("\nPlease enter the item you want to add to your shopping list: ");
                    tempArray[modifiedSize - 1] = Console.ReadLine();
                    Console.WriteLine("\nInput was successful! {0} was successfully added!", tempArray[modifiedSize-1]);
                    shoppingList = tempArray;
                    //shoppingList[modifiedSize - 1] = tempArray[modifiedSize - 1];
                }
                else if (userOption == 'r')
                {
                    String itemToRemove;
                    Console.WriteLine("\nPlease enter the item you want to remove from your shopping list: ");
                    itemToRemove = Console.ReadLine();
                    String[] anotherTempArray = new String[--modifiedSize];
                    
                    // this loop copies the shopping list into the new array
                    for (int index = 0, nextIndex = 0; index < modifiedSize || nextIndex < modifiedSize; index++, nextIndex++)
                    {
                        if (itemToRemove == shoppingList[index])
                        {
                            nextIndex++;
                            anotherTempArray[index] = shoppingList[nextIndex];
                            continue;
                        }
                        anotherTempArray[index] = shoppingList[nextIndex];
                    }
                    Console.WriteLine("\nInput was successful! {0} was succesfully removed!", itemToRemove);
                    shoppingList = anotherTempArray;
                }
                else
                {
                    Console.WriteLine("That wasn't one of the listed options.");
                    continue;
                }
            } while (userOption != 'e');
            
            Console.ReadKey();
        }
    }
}
