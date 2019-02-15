using System;

namespace ConsoleLib
{
    public class AskForDateOptions
    {
        public Func<DateTime, AskForDateValidationResult> CustomValidation { get; set; }

        public string ErrorMessageFormat { get; set; } =
            "The value you entered '{0}' is not valid, try again using the format {1}";

        public string Format { get; set; } = "dd/MM";
    }
}