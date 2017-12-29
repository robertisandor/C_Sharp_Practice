using RS_ChatClient.Classes;
using RS_ChatClient.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient
{
    public class RoomManager
    {
        public static int linesOfInfo = 3;

        public List<Room> AvailableRooms;
        public int CurrentRoomIndex;

        public RoomManager()
        {
            AvailableRooms = new List<Room>();
        }

        public void AddAvailableRoomsForUser(DataTable rooms)
        {
            for (int index = 0; index < rooms.Rows.Count; index++)
            {
                AvailableRooms.Add(new Room((int?)rooms.Rows[index].ItemArray[1], (string)rooms.Rows[index].ItemArray[0]));
            }
        }

        public void DisplayRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                Console.WriteLine(room.RoomName);
            }
        }

        public void DisplayRooms(DataTable rooms)
        {
            for(int index = 0; index < rooms.Rows.Count; index++)
            {
                Console.WriteLine((string)rooms.Rows[index].ItemArray[0]);
            }
        }

        public void DisplayRoomOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) [C]reate a new room");
            Console.WriteLine("2) [V]iew messages in existing room");
            Console.WriteLine("3) [J]oin a room");
            Console.WriteLine("4) [E]xit the entire program");
        }

        public void CheckUserResponse(SqlConnection sqlConnection, LoginManager loginManager, ref bool validResponse)
        {
            string userRoomChoice = "";
            switch (loginManager.UserResponse)
            {
                case 'c':
                    CreateRoom(sqlConnection, loginManager.CurrentUser.UserID);
                    // I probably don't need to use validResponse
                    validResponse = true;
                    break;
                case 'v':
                    Console.Clear();
                    Console.WriteLine("Rooms that you belong to: ");
                    DisplayRooms(AvailableRooms);
                    
                    Console.WriteLine("Please enter the name of the room in which you want to view the messages.");
                    
                    userRoomChoice = Console.ReadLine();
                    // I need to check if the room they entered is one they belong to
                    // if it is, then display the messages, 
                    if (AvailableRooms.Exists(roomName => roomName.RoomName == userRoomChoice))
                    {
                        AvailableRooms[CurrentRoomIndex].RoomName = userRoomChoice;

                        Console.Clear();
                        loginManager.DisplayCurrentUser();
                        Console.WriteLine(AvailableRooms[CurrentRoomIndex].RoomName);
                        Console.WriteLine("Type your message below. To exit the program, press the Escape key.");

                        var messagesTable = FetchMessagesInRoom(sqlConnection, loginManager.CurrentUser);
                        int sumOfLinesOccupied = SumLinesOccupied(messagesTable);
                        Queue<Message> messages = new Queue<Message>();
                        messages = FillQueueWithMessages(messagesTable, messages);
                        FitMessagesOnScreen(messages, sumOfLinesOccupied + 3, 1);
                        ViewMessagesInRoom(messages);
                        validResponse = true;
                    }
                    // otherwise, tell them they're not part of the room
                    else
                    {
                        Console.WriteLine("You are currently not a member of that room. You can not view messages of a room you are not a member of.");
                        Console.WriteLine("Please enter 'c' to create a new room," +
                        " 'v' to view messages in a room you are a member of," +
                        "'j' to join a room" +
                        "or 'e' to exit the program.");
                    }

                    break;
                case 'j':
                    // use AddUserToRoom stored proc
                    // to use it, I need to know the roomID of the room I want to joinand my userID 
                    Console.WriteLine("Please enter the name of the room which you want to join.");
                    userRoomChoice = Console.ReadLine();
                    Room roomToJoin = null;
                    // I should confirm that the room isn't a room that the user is already a member of
                    // use ListRoomsUserDoesNotBelongTo to return a table of rooms that the user isn't in
                    DataTable table = SQLUtilities.ExecuteSQLTabularQuery(sqlConnection, "usp_ListRoomsUserDoesNotBelongTo", new SqlParameter[]
                        {
                            new SqlParameter("UserID",  loginManager.CurrentUser.UserID)
                        }
                    );
                    
                    for(int index = 0; index < table.Rows.Count; index++)
                    {
                        if ((string)table.Rows[index].ItemArray[1] == userRoomChoice)
                        {
                            roomToJoin = new Room((int?)table.Rows[index].ItemArray[0], (string)table.Rows[index].ItemArray[1]);
                            // issue with int? cast in the call to this function
                            SQLUtilities.ExecuteSQLScalarQuery(sqlConnection, "usp_AddUserToRoom", new SqlParameter[]
                            {
                                new SqlParameter("RoomID", roomToJoin.RoomID),
                                new SqlParameter("UserID", loginManager.CurrentUser.UserID)
                            }
                            );

                            AvailableRooms.Add(roomToJoin);
                            CurrentRoomIndex = AvailableRooms.IndexOf(roomToJoin);

                            var messagesTable = FetchMessagesInRoom(sqlConnection, loginManager.CurrentUser);
                            int sumOfLinesOccupied = SumLinesOccupied(messagesTable);
                            Queue<Message> messages = new Queue<Message>();
                            messages = FillQueueWithMessages(messagesTable, messages);
                            ViewMessagesInRoom(messages);
                            validResponse = true;
                        }
                    }
                    
                    break;
                case 'e':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("That is an invalid option.");
                    Console.WriteLine("Please enter 'c' to create a new room," +
                        " 'v' to view messages in a room you are a member of," +
                        "'j' to join a room" +
                        "or 'e' to exit the program.");
                    break;
            }
        }

        public int SumLinesOccupied(DataTable table)
        {
            int currentSum = 0;
            for (int index = 0; index < table.Rows.Count; index++)
            {
                Message message = new Message((DateTime)table.Rows[index].ItemArray[0], (string)table.Rows[index].ItemArray[1], (string)table.Rows[index].ItemArray[2]);
                currentSum += message.GetLineCount();
            }
            return currentSum;
        }

        // do I need the Queue parameter?
        public Queue<Message> FillQueueWithMessages(DataTable table, Queue<Message> messages)
        {
            for (int index = messages.Count; index < table.Rows.Count; index++)
            {
                Message message = new Message((DateTime)table.Rows[index].ItemArray[0], (string)table.Rows[index].ItemArray[1], (string)table.Rows[index].ItemArray[2]);

                messages.Enqueue(message);
            }
            return messages;
        }

        public int FitMessagesOnScreen(Queue<Message> messages,int sumOfLinesOccupied, int currentMaxLinesForBuffer)
        {
            while (sumOfLinesOccupied > Console.WindowHeight - currentMaxLinesForBuffer)
            {
                Message message = messages.Dequeue();
                sumOfLinesOccupied -= message.GetLineCount();
            }
            return sumOfLinesOccupied;
        }

        public DataTable FetchRooms(SqlConnection sqlConnection, int? userID, string procName)
        {
            return SQLUtilities.ExecuteSQLTabularQuery(sqlConnection, procName,
                new SqlParameter[] { new SqlParameter("@UserID", userID) });
        }

        public DataTable FetchMessagesInRoom(SqlConnection sqlConnection, User currentUser)
        {
            return SQLUtilities.ExecuteSQLTabularQuery(sqlConnection, "usp_ListAllMessagesInRoom",
                new SqlParameter[]
                { new SqlParameter("@RoomName", AvailableRooms[CurrentRoomIndex].RoomName),
                              new SqlParameter("@UserID", currentUser.UserID)
                });
        }

        // TODO: limit # of messages on screen to how many lines are available (minus the buffer)
        public void ViewMessagesInRoom(Queue<Message> messages)
        {
            if (messages.Count > 0)
            {
                // TODO: rather than getting rid of all of the messages and reloading the entire thing,
                // just load the difference
                AvailableRooms[CurrentRoomIndex].Messages.Clear();
                int index = 0;
                while(messages.Count > 0)
                {
                    Message currentMessage = messages.Dequeue();
                    AvailableRooms[CurrentRoomIndex].Messages.Add(new Message(currentMessage.TimeSent, currentMessage.SenderUsername,currentMessage.MessageContent));
                    Console.Write($"{AvailableRooms[CurrentRoomIndex].Messages[index].TimeSent} - " +
                        $"{AvailableRooms[CurrentRoomIndex].Messages[index].SenderUsername}: " +
                        $"{AvailableRooms[CurrentRoomIndex].Messages[index].MessageContent}");
                    FillLineWithSpaces();
                    index++;
                }
            }
            else
            {
                Console.WriteLine("There are no messages in that room.");
            }
        }

        public void DisplayMessageBuffer(string currentMessage)
        {
            int minimumLinesForBuffer = 3;

            if (currentMessage.Length < Console.WindowWidth)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - minimumLinesForBuffer);
            }
            else
            {
                Console.SetCursorPosition(0, Console.WindowHeight - (minimumLinesForBuffer + currentMessage.Length / Console.WindowWidth));
            }
            string separator = new String('═', Console.WindowWidth);     //ASCII 205
            Console.Write(separator);
            Console.CursorVisible = true;

            Console.Write(currentMessage);
            FillLineWithSpaces();

            Console.CursorTop -= 1;
            Console.CursorLeft = currentMessage.Length;
        }

        public void DisplayMessageBuffer(string currentMessage, int minimumLinesForBuffer, ref int currentMaxLinesForBuffer)
        {
            if (currentMessage.Length < Console.WindowWidth)
            {
                Console.SetCursorPosition(0, Console.WindowHeight - currentMaxLinesForBuffer);
                for (int lineNumber = 0; lineNumber < currentMaxLinesForBuffer - minimumLinesForBuffer; lineNumber++)
                {
                    FillLineWithSpaces();
                }
            }
            else
            {
                currentMaxLinesForBuffer = minimumLinesForBuffer + currentMessage.Length / Console.WindowWidth;
                Console.SetCursorPosition(0, Console.WindowHeight - currentMaxLinesForBuffer);
            }
            string separator = new String('═', Console.WindowWidth);     //ASCII 205
            Console.Write(separator);
            Console.CursorVisible = true;

            Console.Write(currentMessage);
            FillLineWithSpaces();

            Console.CursorTop -= 1;
            if (currentMessage.Length < Console.WindowWidth)
            {
                Console.CursorLeft = currentMessage.Length;
            }
            else
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }

        private void CreateRoom(SqlConnection sqlConnection, int? userID)
        {
            Console.WriteLine("Please enter the name of the new room: ");

            Room newRoom = new Room();
            newRoom.RoomName = Console.ReadLine();
            newRoom.RoomID = SQLUtilities.ExecuteSQLScalarQuery(sqlConnection, "usp_CreateRoom",
                new SqlParameter[]
                { new SqlParameter("@RoomName", newRoom.RoomName) });
            AvailableRooms.Add(newRoom);
            CurrentRoomIndex = AvailableRooms.IndexOf(newRoom);

            Console.Clear();
            Console.WriteLine($"Created room {AvailableRooms[CurrentRoomIndex].RoomName} successfully! Joining room {AvailableRooms[CurrentRoomIndex].RoomName} now.");
            Console.WriteLine(AvailableRooms[CurrentRoomIndex].RoomName);

            JoinRoom(sqlConnection, userID);
        }

        public void JoinRoom(SqlConnection sqlConnection, int? userID)
        {
            SQLUtilities.ExecuteSQLTabularQuery(sqlConnection, "usp_AddUserToRoom",
                new SqlParameter[]
                { new SqlParameter("@RoomID", AvailableRooms[CurrentRoomIndex].RoomID),
                              new SqlParameter("@UserID", userID) });
        }


        public void FillLineWithSpaces()
        {
            if (Console.CursorLeft < Console.WindowWidth)
            {
                Console.CursorVisible = false;
                while (Console.CursorLeft < Console.WindowWidth - 1)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
                Console.CursorVisible = true;
            }
        }
    }
}
