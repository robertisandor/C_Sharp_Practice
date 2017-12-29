using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient
{
    public class User
    {
        public int? UserID;
        public string Username;
        // TODO: add userColor into the database
        // add passwords?
        public ConsoleColor UserColor;

        public User(int? userID = null, string username=null)
        {
            Username = username;
            UserID = userID;
        }
    }
}
