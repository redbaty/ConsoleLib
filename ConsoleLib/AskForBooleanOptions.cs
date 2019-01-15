using System;
using System.Collections.Generic;

namespace ConsoleLib
{
    public class AskForBooleanOptions
    {
        public int Timeout { get; set; } = -1;

        public bool DefaultAnswer { get; set; }

        public string PositiveAnswer { get; set; } = "Yes";

        public string NegativeAnswer { get; set; } = "No";

        public Func<int, string> TimerMessage { get; set; } =
            i => i > 0 ? $"{i}s left" : "Time\'s up, default answer was chosen";

        public List<ConsoleKey> PositiveKeys { get; set; } = new List<ConsoleKey>
        {
            ConsoleKey.Y
        };

        public List<ConsoleKey> NegativeKeys { get; set; } = new List<ConsoleKey>
        {
            ConsoleKey.N
        };
    }
}