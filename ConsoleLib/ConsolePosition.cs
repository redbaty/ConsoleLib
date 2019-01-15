using System;

namespace ConsoleLib
{
    public class ConsolePosition
    {
        public int Top { get; }
        public int Left { get; }

        public ConsolePosition(int top, int left)
        {
            Top = top;
            Left = left;
        }

        public void Write(string message, int topOffset = 0)
        {
            ConsoleUtilities.SetTemporaryConsolePosition(() => Console.Write(message), Top + topOffset, Left);
        }

        public void WriteCleaningLine(string message, int topOffset = 0)
        {
            ConsoleUtilities.ClearLine(Top + topOffset, Left);
            Write(message, topOffset);
        }
    }
}