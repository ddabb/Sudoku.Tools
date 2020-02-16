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
        Location,
        Important,
    }

    public class SolveMessage
    {
        public string message;
        public MessageType messageType;

        public SolveMessage(string message, MessageType type = MessageType.Normal)
        {
            this.message = message;
            this.messageType = type;
        }
    }
}
