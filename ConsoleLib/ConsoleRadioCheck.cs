namespace ConsoleLib
{
    public class ConsoleRadioCheck<TResponseType>
    {
        public string Message { get; }

        public bool IsHovering { get; set; }

        public bool IsChecked { get; set; }

        public TResponseType Value { get; }

        public ConsoleRadioCheck(string message, TResponseType value)
        {
            Message = message;
            Value = value;
        }

        public override string ToString()
        {
            var s = IsHovering ? ">" : "";

            if (!IsChecked)
                s += "( )";
            else
                s += "(o)";

            s += " " + Message;
            return s;
        }
    }
}