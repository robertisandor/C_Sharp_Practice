using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient.Classes
{
    public class Message
    {
        public DateTime TimeSent;
        public string SenderUsername;
        public string MessageContent;
        
        public Message(DateTime timeSent, string senderUsername, string messageContent)
        {
            TimeSent = timeSent;
            SenderUsername = senderUsername;
            MessageContent = messageContent;
        }

        public int GetLineCount()
        {
            return (int)Math.Ceiling((float)MessageContent.Length / Console.WindowWidth);
        }
    }
}
