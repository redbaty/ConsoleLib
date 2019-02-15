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
                Console.WriteLine($"{Question} ({Parameters.Format})" + (Parameters.DefaultDate != null
                                      ? $" [{Parameters.DefaultDate.Value.ToString(Parameters.Format)}]"
                                      : string.Empty));

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
                    if (Parameters.DefaultDate != null)
                    {
                        ConsoleUtilities.SetTemporaryConsolePosition(() =>
                            {
                                Console.WriteLine(Parameters.DefaultValueMessage,
                                    Parameters.DefaultDate.Value.ToString(Parameters.Format));
                            }, Console.CursorTop - 1, 0);

                        return Parameters.DefaultDate.Value;
                    }

                    if (!string.IsNullOrEmpty(Parameters.ErrorMessageFormat))
                        Console.WriteLine(Parameters.ErrorMessageFormat, input, Parameters.Format);
                }
            }
        }
    }
}