using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Rock_Paper_Scissors_with_SQL
{
    enum GameResult
    {
        LOST = -1,
        TIED = 0,
        WON = 1
    }
    enum GameChoice
    {
        NONE = -1,
        ROCK = 0,
        PAPER = 1,
        SCISSORS = 2
    }
    class Program
    {
        static void Main(string[] args)
        {
            // use the static string if I want to share the string across multiple classes, like in a web API
            // put the string in the App.config XML with the tag connectionStrings to prevent others from decompiling
            // and getting the connectionString info...
            // it also may be useful if I have multiple machines that are accessing different databases
            // that require different connection strings
            string connectionString = ConfigurationManager.ConnectionStrings["devServer"].ConnectionString;

            Console.WriteLine("Welcome to Rock, Paper, Scissors - The Game!");
            Console.WriteLine("Would you like to (L)og in or (C)reate an account or (E)xit?");

            string response = "";
            bool validResponse = false;
            int? userID = null;
            Random generator = new Random();

            // helper functions are useful when we're doing similar commands over and over again
            // not the most efficient - in comparison to keeping the connection open; however, it's code reuse
            // create 1 connection and reuse connection; creating and destroying connections is costly
            // only create the connection once
            // creating and destroying commands is cheap; feel free to create and destroy commands freely
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // execute nonquery - void version of sql query
            // for simple stored procedures like CreateUser,
            // I should probably return a UserID with "SELECT SCOPEIDENTITY AS UserID" to confirm that the user was in fact created

            // to make sure username is unique,
            // right-click on column name, click Indexes/Keys, Add, choose the correct column,
            // then choose Yes for IsUnique
            do
            {
                response = Console.ReadLine().ToLower();

                switch (response)
                {
                    // if switch case has no body, it doesn't need a break statement
                    // if it has a body, it needs a break or return statement

                    // I'm going to connect to the DB
                    // if I get a userID back from the DB (which I should get if I successfully login or create a user)
                    // then I should get into the game
                    case "create":
                    case "c":
                    case "login":
                    case "l":
                        Console.WriteLine("Type in your username: ");
                        string username = Console.ReadLine();
                        Console.WriteLine("Type in your password: ");
                        string password = Console.ReadLine();
                        // DataTable table;

                        // scope_identity
                        // SELECT * from table: * refers to which columns we get back
                        // we shouldn't get all of the columns back (we usually don't need all of the info)
                        string storedProcName = response == "c" ? "usp_CreateUser" : "usp_Login";
                        var sqlParams = new[]
                        {
                            new SqlParameter("@Username", username),
                            new SqlParameter("@Password", password)
                        };

                        //If we need to call 2 different procs...
                        // is this necessary now?
                        // executescalar returns decimal
                        // convert to string,
                        // then it needs to parse int after
                        // System.FormatException - input string was not in a correct format
                        // there are issues when it returns null...
                        // does it return an empty string or null after using ToString? null, I think
                        // login doesn't work properly... figure out why
                        // it was because scopeidentity was returning a decimal
                        var holder = GetData<int>(sqlConnection, storedProcName, sqlParams);
                        if (holder != null)
                        {
                            userID = int.Parse(holder.ToString());
                        }

                        // what will I get if I'm unsuccessful with the login?
                        // null
                        if (userID != null)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            // it was an invalid login
                            Console.WriteLine("Type in 'l' to try logging in again or 'c' to create a new username:");
                        }


                        //TODO... change params, proc name, etc.
                        // table = GetData(sqlConnection, storedProcName, sqlParams);                        
                        break;
                    case "exit":
                    case "e":
                        Console.WriteLine("Type a key to exit now");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("That wasn't one of the options provided. Please input either l to log in, c to create an account, or e to exit.");
                        break;
                }
            } while (!validResponse);

            bool userWantsToPlay = false;
            do
            {
                GameChoice userChoice = GameChoice.NONE;
                do
                {
                    Console.WriteLine("Type in 0 for Rock, 1 for Paper, and 2 for Scissors.");
                    string userResponse = Console.ReadLine();
                    while (userChoice == GameChoice.NONE)
                    {
                        try
                        {
                            userChoice = (GameChoice)int.Parse(userResponse);
                        }
                        catch (Exception e)
                        {
                            userChoice = GameChoice.NONE;
                            Console.WriteLine("Please enter valid input.");
                        }
                    }

                    if (userChoice < GameChoice.ROCK || userChoice > GameChoice.SCISSORS)
                    {
                        Console.WriteLine("That number wasn't in the range of valid options.");
                        userChoice = GameChoice.NONE;
                    }
                } while (userChoice == GameChoice.NONE);

                GameChoice computerChoice = (GameChoice)generator.Next(3);
                GameResult result = DetermineWinner(computerChoice, userChoice);
                // I will have a bunch of scores each with their own scoreID
                // will I need a separate table for high scores?

                // DataTable scoreTable;

                // TODO: figure out what I am using scoreID for. Do I need it?
                var scoreID = GetData<int>(sqlConnection, "usp_CreateScore", new SqlParameter[] { new SqlParameter("@ScoreValue", result), new SqlParameter("@UserID", userID) });

                // there shouldn't be a scenario where scoreID returns null
                // because it'll create a score and automatically return the scope identity
                // if I have magic #s, create an enum
                switch (result)
                {
                    case GameResult.LOST:
                        Console.WriteLine("You lost!");
                        break;
                    case GameResult.TIED:
                        Console.WriteLine("You tied!");
                        break;
                    case GameResult.WON:
                        Console.WriteLine("You won!");
                        break;
                }

                bool stayInMenu = true;

                do
                {
                    Console.WriteLine("Would you like to view your [p]ersonal recent scores, the [o]verall recent scores, play [a]gain, or e[x]it");
                    string answer = Console.ReadLine();
                    answer = answer.ToLower();
                    switch (answer)
                    {
                        case "overall":
                        case "o":
                            // revise the getTop5OverallScores query to return the username
                            // this will probably require a join
                            DisplayData(sqlConnection, "usp_Get5MostRecentOverallScores", new SqlParameter[] { });
                            stayInMenu = true;
                            break;
                        case "personal":
                        case "p":
                            // revise the getTop3UserScores to return the 3 most recent scores
                            DisplayData(sqlConnection, "usp_Get3MostRecentUserScores", new SqlParameter[] { new SqlParameter("@UserID", userID) });
                            stayInMenu = true;
                            break;
                        case "again":
                        case "a":
                            stayInMenu = false;
                            userWantsToPlay = true;
                            break;
                        case "exit":
                        case "x":
                            stayInMenu = false;
                            userWantsToPlay = false;
                            break;
                        default:
                            Console.WriteLine("Please enter in either a 'p' to view your personal recent scores, "
                                + "a 'o' to view the overall recent scores, "
                                + "an 'a' to play again, or "
                                + "an 'x' to exit the program");
                            stayInMenu = true;
                            answer = Console.ReadLine();
                            break;
                    }
                } while (stayInMenu);
            } while (userWantsToPlay);

            Console.WriteLine("Thanks for playing Rock, Paper, Scissors!");

            Console.ReadKey();
        }

        public static GameResult DetermineWinner(GameChoice computerChoice, GameChoice userChoice)
        {
            GameResult result = GameResult.TIED;
            // should I have more fields for the score table?
            // maybe I should have fields that indicate what the user choice was
            // and what the computer choice was...
            // TODO: create a function out of this
            if (userChoice == computerChoice)
            {
                result = GameResult.TIED;
            }
            // user lost
            else if (userChoice == computerChoice - 1 || userChoice == computerChoice + 2)
            {
                result = GameResult.LOST;
            }
            else if (computerChoice == userChoice - 1 || computerChoice == userChoice + 2)
            {
                result = GameResult.WON;
            }
            return result;
        }


        public static void DisplayData(SqlConnection sqlConnection, string procName, SqlParameter[] sqlParameters)
        {
            DataTable scores = GetData(sqlConnection, procName, sqlParameters);
            for (int index = 0; index < scores.Rows.Count; index++)
            {
                string user = "";
                if(procName == "usp_Get5MostRecentOverallScores")
                {
                    user = (string)scores.Rows[index].ItemArray[2];
                }
                else if(procName == "usp_Get3MostRecentUserScores")
                {
                    user = "You";
                }
                
                GameResult userPreviousChoice = (GameResult)scores.Rows[index].ItemArray[1];
                switch (userPreviousChoice)
                {
                    case GameResult.LOST:
                        Console.WriteLine($"{user} lost against the computer.");
                        break;
                    case GameResult.TIED:
                        Console.WriteLine($"{user} tied against the computer.");
                        break;
                    case GameResult.WON:
                        Console.WriteLine($"{user} won against the computer.");
                        break;
                }
            }
        }


        // I can check if a DataTable is empty
        // by checking if the Rows.Count is 0
        // returning a table is more common so that they have a user profile
        private static DataTable GetData(SqlConnection sqlConnection, string storedProcName, SqlParameter[] sqlParams)
        {
            // I can return a table if I want multiple values
            DataTable table;

            using (SqlCommand sqlCommand = new SqlCommand(storedProcName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(sqlParams);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                table = new DataTable();

                try
                {
                    sqlConnection.Open();
                    adapter.Fill(table);
                }
                catch (SqlException sqlException)
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        Console.WriteLine("The connection to the database could not be opened.");
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                }
            }

            return table;
        }

        // is making this nullable acceptable?
        // I would need to make a different version if it's a class

        // if you  can't recover from it, don't catch it
        // for not getting the connection, throw

        // I would need to duplicate and make a version for the class (like string))
        // ExecuteScalar is less common
        private static T? GetData<T>(SqlConnection sqlConnection, string storedProcName, SqlParameter[] sqlParams) where T : struct
        {
            // I can return a single result if I'd like
            T? result = null;

            using (SqlCommand sqlCommand = new SqlCommand(storedProcName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(sqlParams);
                // System.Data.SqlClient.SqlException - no connection could be opened
                // how do we handle that?


                // ExecuteScalar() returns the first result/row
                // TODO: invalid cast exception? figure out what causes it
                // I was casting an unknown type
                // into a known type when there's no guarantee for a conversion
                // System.Data.SqlClient.SqlException - can't enter in duplicate data
                // how do we deal with that?
                // should I use a try/catch?
                // yes, because there's no way to anticipate duplicate entries or any 
                // of the myriad other exceptions
                try
                {
                    sqlConnection.Open();
                    var returnValue = sqlCommand.ExecuteScalar();
                    // how do I handle the invalid cast exception?
                    // I prevent it from happening in the first place
                    // scope_identity returns decimal, cast it to an int SQL-side
                    // so there's no issue with the C# code
                    result = (T?)returnValue;
                }
                catch (SqlException sqlException)
                {
                    // sqlException
                    // check for specific error code
                    // if we don't get any of the error codes
                    // we want/are looking out for
                    // then throw
                    // error code for duplicate entry = -2146232060
                    // this is only applicable when logging in,
                    // not when I'm doing any other call
                    // no connection errorcode =  -2146232060 and Number=53
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        Console.WriteLine("The connection to the database could not be opened.");
                    }
                    else if (sqlException.Number == 2601)
                    {
                        Console.WriteLine("That username already exists. Please retry another username and password or login.");
                    }
                    else
                    {
                        // don't throw specific variable/error
                        // because it'll lose the stack trace
                        // throw a new exception if I want to add any extra info and add the original exception as a parameter
                        // throw new Exception("Something went terribly wrong...", sqlException);
                        throw;
                    }
                }
                finally
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                }

                if (result == null && storedProcName == "usp_Login")
                {
                    Console.WriteLine("That username/password combination doesn't exist.");
                }
            }

            return result;
        }

        private static T GetDataFromClass<T>(SqlConnection sqlConnection, string storeddProcName, SqlParameter[] sqlParams) where T : class
        {
            T result = null;

            using (SqlCommand sqlCommand = new SqlCommand(storeddProcName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(sqlParams);

                sqlConnection.Open();

                result = (T)sqlCommand.ExecuteScalar();

                sqlConnection.Close();
            }

            return result;
        }

    }
}
