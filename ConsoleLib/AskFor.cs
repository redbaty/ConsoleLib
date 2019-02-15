using System;
using System.Collections.Generic;

namespace ConsoleLib
{
    public static class AskFor
    {
        public static List<TResponseType> Choice<TResponseType>(AskForChoice<TResponseType> choice)
        {
            return choice.Draw();
        }

        public static DateTime Date(string question, AskForDateOptions options = null)
        {
            return new AskForDate(question, options).Draw();
        }

        public static bool Boolean(string question, int timeout = -1, bool defaultAnswer = false)
        {
            return new AskForBoolean(question, new AskForBooleanOptions
            {
                Timeout = timeout,
                DefaultAnswer = defaultAnswer
            }).Draw();
        }
    }
}