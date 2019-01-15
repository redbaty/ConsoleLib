using System;
using System.Threading.Tasks;

namespace ConsoleLib
{
    public class AskForBoolean : IAsker<AskForBooleanOptions, bool>
    {
        private string YesChar => Parameters.DefaultAnswer ? "Y" : "y";
        private string NoChar => Parameters.DefaultAnswer ? "n" : "N";
        private AskForBooleanTimer Timer { get; set; }

        public AskForBoolean(string question, AskForBooleanOptions parameters = null)
        {
            Question = question;
            Parameters = parameters ?? new AskForBooleanOptions();
        }

        public string Question { get; }

        public bool Draw()
        {
            var formattableString = $"{Question} [{YesChar}/{NoChar}]";


            while (true)
            {
                Console.WriteLine(formattableString);

                if (Parameters.Timeout > -1)
                {
                    Timer = new AskForBooleanTimer(Parameters.Timeout, Console.CursorTop - 1,
                        formattableString.Length + 1,
                        Parameters.TimerMessage);
                    Timer.Start();
                }

                var task = Task.Run(() => Console.ReadKey(true));

                var read = task.Wait(Parameters.Timeout > -1
                    ? TimeSpan.FromSeconds(Parameters.Timeout)
                    : TimeSpan.FromMilliseconds(-1));

                Timer?.Stop();

                if (read)
                {
                    var key = task.Result;
                    var consoleKey = key.Key;
                    if (Parameters.PositiveKeys.Contains(consoleKey))
                    {
                        Console.WriteLine(Parameters.PositiveAnswer);
                        return true;
                    }

                    if (Parameters.NegativeKeys.Contains(consoleKey))
                    {
                        Console.WriteLine(Parameters.NegativeAnswer);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine(Parameters.DefaultAnswer ? Parameters.PositiveAnswer : Parameters.NegativeAnswer);
                    return Parameters.DefaultAnswer;
                }
            }
        }

        public AskForBooleanOptions Parameters { get; }
    }
}