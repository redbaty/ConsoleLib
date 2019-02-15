using System;
using System.Globalization;

namespace ConsoleLib
{
    public class AskForDate : IAsker<AskForDateOptions, DateTime>
    {
        public AskForDateOptions Parameters { get; }
        public string Question { get; }

        public AskForDate(string question, AskForDateOptions parameters = null)
        {
            Question = question;
            Parameters = parameters ?? new AskForDateOptions();
        }

        public DateTime Draw()
        {
            while (true)
            {
                Console.WriteLine($"{Question} ({Parameters.Format})");
                var input = Console.ReadLine();

                if (DateTime.TryParseExact(input, Parameters.Format, null, DateTimeStyles.None, out var dateTime))
                {
                    if (Parameters.CustomValidation != null)
                    {
                        var validationResult = Parameters.CustomValidation.Invoke(dateTime);
                        if (validationResult.IsSuccess) return dateTime;

                        Console.WriteLine(validationResult.ErrorMessage);
                    }
                    else
                    {
                        return dateTime;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Parameters.ErrorMessageFormat))
                        Console.WriteLine(Parameters.ErrorMessageFormat, input, Parameters.Format);
                }
            }
        }
    }
}