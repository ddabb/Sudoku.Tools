using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Core
{
    /// <summary>
    /// 解题信息
    /// </summary>
    public class SolveMessage
    {
        public string content;
        public MessageType messageType;

        public SolveMessage(string message, MessageType type = MessageType.Normal)
        {
            this.content = message;
            this.messageType = type;
        }

        public static implicit operator SolveMessage(string str)
        {
            return new SolveMessage(str);
        }


        public static implicit operator SolveMessage(decimal str)
        {
            return new SolveMessage("  "+str, MessageType.Important);
        }

        public static implicit operator SolveMessage(AllR1C1 str)
        {
            return new SolveMessage(""+str, MessageType.Location);
        }

        public static implicit operator SolveMessage(AllA1I9 str)
        {
            return new SolveMessage(""+str, MessageType.Location);
        }


        public static implicit operator SolveMessage(List<LocationGroup> groups)
        {
            return new SolveMessage(groups.Select(c=>c.LocationDesc).JoinString(), MessageType.Location);
        }

        public override string ToString()
        {
            return this.content;
        }
    }
}