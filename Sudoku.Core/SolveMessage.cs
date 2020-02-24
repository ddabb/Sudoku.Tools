namespace Sudoku.Core
{
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

        public override string ToString()
        {
            return this.content;
        }
    }
}