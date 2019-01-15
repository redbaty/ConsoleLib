using System;
using System.Linq;

namespace ConsoleLib
{
    internal static class ConsoleUtilities
    {
        public static void ClearLine(int line, int start = 0, int end = -1)
        {
            SetTemporaryConsolePosition(
                () => Console.Write(
                    new string(Enumerable.Range(0, end > -1 ? end : Console.BufferWidth - start).Select(i => ' ')
                        .ToArray())), line,
                start);
        }

        public static void SetTemporaryConsolePosition(Action action, int top, int left)
        {
            var oldTopPosition = Console.CursorTop;
            var oldLeftPosition = Console.CursorLeft;

            Console.SetCursorPosition(left, top);
            action.Invoke();
            Console.SetCursorPosition(oldLeftPosition, oldTopPosition);
        }
    }
}