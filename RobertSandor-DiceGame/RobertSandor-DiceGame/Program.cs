using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertSandor_DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("We're gonna play a dice game. Give me the amount of side on a die:");
            string userInput = Console.ReadLine();
            int numberOfSidesOnDie = 0;
            bool inputIsValid = false;


            while(!int.TryParse(Console.ReadLine(), out numberOfSidesOnDie))
            {
                Console.WriteLine("Please input a number: ");
            }

            while (inputIsValid)
            {
                try
                {
                    numberOfSidesOnDie = Int32.Parse(userInput);
                }
                catch
                {
                    Console.WriteLine("Please input a number: ");
                    numberOfSidesOnDie = Int32.Parse(Console.ReadLine());
                }
            }
            
            Console.ReadKey();
        }

        bool TryConvert(string input, out int number)
        {
            try
            {
                number = Int32.Parse(input);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
