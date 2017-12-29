using RS_ChatClient.Classes;
using RS_ChatClient.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RS_ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["developerServer"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            //Console.SetWindowSize(240, 71);
            Console.WriteLine("Welcome to the ChatClient program!");
            LoginManager loginManager = new LoginManager();

            loginManager.CurrentUser.Username = "";
            bool validResponse = false;
            // can I abstract any of this into a function?
            do
            {
                loginManager.DisplayLoginOptions();
                loginManager.UserResponse = Console.ReadKey(true).KeyChar;
                loginManager.CheckUserResponse(sqlConnection, loginManager.UserResponse, ref validResponse);
            } while (!validResponse && loginManager.CurrentUser.UserID == null);

            Console.Clear();
            loginManager.DisplayCurrentUser();

            RoomManager roomManager = new RoomManager();
            DataTable roomsUserBelongsTo = roomManager.FetchRooms(sqlConnection, loginManager.CurrentUser.UserID, "usp_ListRoomsForUser");
            roomManager.AddAvailableRoomsForUser(roomsUserBelongsTo);
            Console.WriteLine("Rooms that you belong to: ");
            roomManager.DisplayRooms(roomManager.AvailableRooms);

            roomManager.DisplayRoomOptions();

            loginManager.UserResponse = ' ';
            validResponse = false;
            do
            {
                loginManager.UserResponse = Char.ToLower(Console.ReadKey(true).KeyChar);
                roomManager.CheckUserResponse(sqlConnection, loginManager, ref validResponse);
            } while (!validResponse);

            StringBuilder currentMessage = new StringBuilder();
            roomManager.DisplayMessageBuffer(currentMessage.ToString());
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            int minimumLinesForBuffer = 3;
            int currentMaxLinesForBuffer = minimumLinesForBuffer;
            Queue<Message> currentMessages = new Queue<Message>();
            bool gotNewChar = false;
            
            while (consoleKeyInfo.Key != ConsoleKey.Escape)
            {
                if (gotNewChar)
                {
                    if (consoleKeyInfo.Key == ConsoleKey.Backspace)
                    {
                        if (currentMessage.Length > 0)
                        {
                            currentMessage.Remove(currentMessage.Length - 1, 1);
                        }
                    }
                    else if (consoleKeyInfo.Key == ConsoleKey.Enter)
                    {
                        SQLUtilities.ExecuteSQLScalarQuery(sqlConnection, "usp_CreateMessage",
                            new SqlParameter[]
                            {
                            new SqlParameter("@MessageFrom", loginManager.CurrentUser.UserID),
                            new SqlParameter("@RoomName", roomManager.AvailableRooms[roomManager.CurrentRoomIndex].RoomName),
                            new SqlParameter("@MessageContent", currentMessage.ToString())
                            });
                        currentMessage.Clear();
                    }
                    else if (consoleKeyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("You are now exiting the ChatClient program.");
                        // TODO: insert logic to log out the user once I add the "active" flag in the database
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                    {
                        currentMessage.Append(consoleKeyInfo.KeyChar);
                    }
                    gotNewChar = false;
                } 

                // rather than using clear, just reset the cursor position
                // and space extend the extend of the line 
                // (so that any remnants of the previous message don't remain)

                Console.SetCursorPosition(0, 0);
                loginManager.DisplayCurrentUser();
                Console.WriteLine(roomManager.AvailableRooms[roomManager.CurrentRoomIndex].RoomName);
                Console.WriteLine("Type your message below. To exit the program, press the Escape key.");
                Console.CursorVisible = true;


                var updatedMessages = roomManager.FetchMessagesInRoom(sqlConnection, loginManager.CurrentUser);

                // I need to determine the amount of lines, not the amount of messages
                // there's a good chance that there are messages that are longer than one line,
                // so I should only display the # of lines 

                int sumOfLinesOccupied = roomManager.SumLinesOccupied(updatedMessages);
                // this adds messages to the queue and adds the amount of lines all of the messages would take
                currentMessages = roomManager.FillQueueWithMessages(updatedMessages, currentMessages);

                // this removes messages until the amount of messages can fit on the screen

                // TODO: 
                // boolean "active" flag - not so great because it presumes successful logout
                // can't presume that because there could be machine failure
                // create SQL procedure to update the GUID that checks if the GUID is valid by checking the timestamp
                // GUID is generated each session, timestamp is generated/updated each query
                // if the GUID is null, (this indicates a successful logout) generate a new GUID
                // if the GUID isn't null, check if the GUID matches and if it does, it indicates the user is still logged in
                // if the GUID doesn't match, something funny is going on
                // when logging in, set GUID to null
                // update the timestamp every X seconds if the GUID matches
                sumOfLinesOccupied = roomManager.FitMessagesOnScreen(currentMessages, sumOfLinesOccupied + RoomManager.linesOfInfo, currentMaxLinesForBuffer);
                roomManager.ViewMessagesInRoom(currentMessages);

                roomManager.DisplayMessageBuffer(currentMessage.ToString(), minimumLinesForBuffer, ref currentMaxLinesForBuffer);

                if (Console.KeyAvailable)
                {
                    consoleKeyInfo = Console.ReadKey();
                    gotNewChar = true;
                }
            }
            Console.Clear();
            Console.WriteLine("Thanks for using the ChatClient program!");
            Console.ReadKey();


        }
    }
}
