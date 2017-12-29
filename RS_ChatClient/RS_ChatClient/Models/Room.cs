using RS_ChatClient.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient
{
    public class Room
    {
        public int? RoomID;
        public string RoomName;
        public List<Message> Messages;

        public Room(int? roomID = null, string roomName=null)
        {
            RoomID = roomID;
            RoomName = roomName;
            Messages = new List<Message>();
        }
    }
}
