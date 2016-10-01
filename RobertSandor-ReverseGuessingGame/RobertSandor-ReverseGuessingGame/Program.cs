using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertSandor_ReverseGuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userWantsToPlay = true;
            do
            {

                Console.WriteLine("Please think of a number. Give me the beginning and end of the range. " +
                "I'll try to guess it. Beginning of range: ");

                int bottomOfRange = 0, topOfRange = 0;
                string bottomOfRangeString = Console.ReadLine();

                inputValidation(bottomOfRangeString, ref bottomOfRange);

                Console.WriteLine("Alright, now give me the top of the range: ");
                string topOfRangeString = Console.ReadLine();

                inputValidation(topOfRangeString, ref topOfRange);

                Console.WriteLine("Alright, so I'm gonna guess the first number. Let me know if I'm too high by pressing 1, too low by pressing 2," +
                    " and if I guess it correctly, press 3.");


                int computerGuess;
                String userResponseString = "";
                int userResponse = 0;
                int upperBoundary, lowerBoundary;

                upperBoundary = topOfRange;
                lowerBoundary = bottomOfRange;

                char userDecisionToContinue = ' ';
                while (userDecisionToContinue == ' ') {
                    Random firstGuess = new Random();
                    computerGuess = firstGuess.Next(lowerBoundary, upperBoundary);
                    Console.WriteLine("computerGuess = " + computerGuess);
                    Console.WriteLine("Was that correct? Type '1' if too high, '2' if too low, and '3' if I got it right.");
                    userResponseString = Console.ReadLine();

                    inputValidation(userResponseString, ref userResponse);

                    if (userResponse == 1)
                    {
                        upperBoundary = computerGuess;
                    }
                    else if (userResponse == 2)
                    {
                        lowerBoundary = computerGuess;
                    } else if (userResponse == 3)
                    {
                        Console.WriteLine("Awesome! Wanna play again? Type in 'y' to play again or 'n' to exit the program.");
                        userDecisionToContinue = Console.ReadKey().KeyChar;

                        do
                        {
                            if (userDecisionToContinue == 'y')
                            {
                                userWantsToPlay = true;
                            }
                            else if (userDecisionToContinue == 'n')
                            {
                                userWantsToPlay = false;
                            }
                            else
                            {
                                Console.WriteLine("Please input either 'y' or 'n'.");
                                userDecisionToContinue = Console.ReadKey().KeyChar;
                                Console.WriteLine("\n");
                            }
                        } while (userDecisionToContinue != 'y' && userDecisionToContinue != 'n');
                    }
                }
                userDecisionToContinue = ' ';
                Console.ReadKey();
            } while (userWantsToPlay);
        }

        static void inputValidation(string userInput, ref int rangeNumber)
        {
            try
            {
                rangeNumber = Int32.Parse(userInput);
            }
            catch
            {
                Console.WriteLine("Please input an integer for the bottom of the range: ");
                rangeNumber = Int32.Parse(Console.ReadLine());
            }
        }
    }
}
