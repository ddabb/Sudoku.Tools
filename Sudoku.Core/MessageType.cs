using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    public enum MessageType
    {
        Normal,
        Postive,
        Nagetive,
        Result,
        Important,
        Reason,
    }

    public class SolveMessage
    {
        public string content;
        public MessageType messageType;

        public SolveMessage(string message, MessageType type = MessageType.Normal)
        {
            this.content = message;
            this.messageType = type;
        }
    }
}
