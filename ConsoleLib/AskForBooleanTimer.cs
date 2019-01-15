using System;
using System.Threading;

namespace ConsoleLib
{
    public class AskForBooleanTimer
    {
        private string OldMessage { get; set; }

        private Timer Timer { get; set; }

        private int CurrentTime { get; set; }

        private Func<int, string> Message { get; }

        private int Line { get; }

        private int StartAt { get; }

        public AskForBooleanTimer(int timeout, int line, int startAt, Func<int, string> message)
        {
            Line = line;
            StartAt = startAt;
            Message = message;
            CurrentTime = timeout;
        }

        public void Start()
        {
            if (Timer != null) return;

            Timer = new Timer(state =>
            {
                var res = Message.Invoke(CurrentTime);

                if (OldMessage != null && res?.Length < OldMessage?.Length)
                    ConsoleUtilities.ClearLine(Line, StartAt, OldMessage.Length);

                ConsoleUtilities.SetTemporaryConsolePosition(() => Console.Write(res), Line, StartAt);
                CurrentTime--;

                OldMessage = res;

                if (CurrentTime < 0) Stop();
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void Stop()
        {
            Timer?.Dispose();
            Timer = null;
        }
    }
}