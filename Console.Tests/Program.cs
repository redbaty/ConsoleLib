using System;
using ConsoleLib;

namespace Console.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var boolean = AskFor.Boolean("Do you like me?", 5);

            var choice = AskFor.Choice(new AskForChoice<string>("Testing",
                new AskForChoiceOptions {CanChooseMultiple = true, Required = true}, "Hello", "World"));

            foreach (var s in choice)
            {
                System.Console.WriteLine(s);
            }
        }
    }
}