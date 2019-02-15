using System;
using ConsoleLib;

namespace Console.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = AskFor.Date("What day is it?",
                new AskForDateOptions
                {
                    CustomValidation = time => time.Date != DateTime.Today
                        ? new AskForDateValidationResult(errorMessage: "This is not today.")
                        : new AskForDateValidationResult(true)
                });

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