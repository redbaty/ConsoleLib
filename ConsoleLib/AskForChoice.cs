using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleLib
{
    public class AskForChoice<TResponseType> : IAsker<AskForChoiceOptions, List<TResponseType>>
    {
        public List<ConsoleRadioCheck<TResponseType>> Options { get; }
        public ConsolePosition Position { get; }

        private int CurrentIndex { get; set; } = -1;


        private static ICollection<ConsoleKey> DownKeys { get; } =
            new List<ConsoleKey> {ConsoleKey.S, ConsoleKey.DownArrow};

        private static ICollection<ConsoleKey> UpKeys { get; } =
            new List<ConsoleKey> {ConsoleKey.W, ConsoleKey.UpArrow};

        private static ICollection<ConsoleKey> SelectKeys { get; } =
            new List<ConsoleKey> {ConsoleKey.Spacebar};

        public AskForChoice(string header, params TResponseType[] options) : this(header, null, options)
        {
        }

        public AskForChoice(string header, AskForChoiceOptions option, params TResponseType[] options)
        {
            Question = header;
            Parameters = option ?? new AskForChoiceOptions();
            Options = options.Select(i => new ConsoleRadioCheck<TResponseType>(Parameters.NameResolver.Invoke(i), i))
                .ToList();
            Position = new ConsolePosition(Console.CursorTop, Console.CursorLeft);
        }

        public AskForChoiceOptions Parameters { get; }
        public string Question { get; }

        public List<TResponseType> Draw()
        {
            Console.CursorVisible = false;

            while (true)
            {
                Position.Write(Question);

                if (CurrentIndex < 0) ChangeIndex(0);

                for (var index = 0; index < Options.Count; index++)
                {
                    var consoleRadioCheck = Options[index];
                    Position.WriteCleaningLine(consoleRadioCheck.ToString(), index + 1);
                }

                var key = Console.ReadKey(true);

                if (DownKeys.Contains(key.Key)) DownIndex();

                if (UpKeys.Contains(key.Key)) UpIndex();

                if (SelectKeys.Contains(key.Key)) ToggleCurrent();

                if (key.Key == ConsoleKey.Enter)
                {
                    var consoleRadioChecks = Options.Where(i => i.IsChecked).ToList();

                    if (Parameters.Required && consoleRadioChecks.Count <= 0) continue;
                    Console.CursorVisible = true;
                    return consoleRadioChecks.Select(i => i.Value).ToList();
                }
            }
        }

        private void Clear()
        {
        }

        private void ToggleCurrent()
        {
            if (IndexExist(CurrentIndex))
            {
                if (!Parameters.CanChooseMultiple) Options.ForEach(i => i.IsChecked = false);

                Options[CurrentIndex].IsChecked = !Options[CurrentIndex].IsChecked;
            }
        }

        private void DownIndex()
        {
            var newIndex = CurrentIndex + 1;
            if (newIndex < Options.Count)
                ChangeIndex(newIndex);
        }

        private void UpIndex()
        {
            var newIndex = CurrentIndex - 1;

            if (newIndex > -1)
                ChangeIndex(newIndex);
        }

        private void ChangeIndex(int newIndex)
        {
            if (IndexExist(CurrentIndex))
                Options[CurrentIndex].IsHovering = false;

            if (IndexExist(newIndex))
                Options[newIndex].IsHovering = true;

            CurrentIndex = newIndex;
        }

        private bool IndexExist(int index)
        {
            return Options.ElementAtOrDefault(index) != null;
        }
    }
}