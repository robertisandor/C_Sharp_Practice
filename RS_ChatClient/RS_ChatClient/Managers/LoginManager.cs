using RS_ChatClient.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient
{
    public class LoginManager
    {
        public User CurrentUser;
        public char UserResponse;

        public LoginManager(User currentUser = null)
        {
            CurrentUser = currentUser;
            if(CurrentUser == null)
            {
                CurrentUser = new User();
            }
        }

        public void DisplayCurrentUser()
        {
            Console.WriteLine($"Logged in as {CurrentUser.Username}");
        }

        public void DisplayLoginOptions()
        {
            Console.WriteLine("Would you like to login or create a new user?");
            Console.WriteLine("Enter 'l' to login, 'c' to create a new user, or 'e' to exit the entire program");
        }

        private void handleInvalidResponse()
        {
            Console.WriteLine("That is an invalid option.");
            UserResponse = Console.ReadKey(true).KeyChar;
        }

        public void CheckUserResponse(SqlConnection sqlConnection, char userResponse, ref bool validResponse)
        {
            switch (userResponse)
            {
                case 'c':
                case 'l':
                    Console.WriteLine("Please enter in your username: ");
                    CurrentUser.Username = Console.ReadLine();
                    var sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@Username", CurrentUser.Username)
                    };
                    string procName = UserResponse == 'c' ? "usp_AddUser" : "usp_LoginUser";
                    // null is returned when there are no rows that match the query
                    // so I need a nullable variable
                    CurrentUser.UserID = SQLUtilities.ExecuteSQLScalarQuery(sqlConnection, procName, sqlParameters);
                    if (CurrentUser.UserID == null)
                    {
                        Console.WriteLine("Login unsuccessful.");
                    }
                    else
                    {
                        validResponse = true;
                    }
                    break;
                case 'e':
                    Environment.Exit(0);
                    break;
                default:
                    handleInvalidResponse();
                    break;
            }
        }
    }
}
