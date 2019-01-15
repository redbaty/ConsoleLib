using System;

namespace ConsoleLib
{
    public class AskForChoiceOptions
    {
        public bool Required { get; set; } = false;
        public bool CanChooseMultiple { get; set; } = true;
        public Func<object, string> NameResolver { get; set; } = o => o.ToString();
    }
}