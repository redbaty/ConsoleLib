namespace ConsoleLib
{
    public class AskForDateValidationResult
    {
        public string ErrorMessage { get; }

        public bool IsSuccess { get; }

        public AskForDateValidationResult(bool isSuccess = false, string errorMessage = "Custom validation failed.")
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}