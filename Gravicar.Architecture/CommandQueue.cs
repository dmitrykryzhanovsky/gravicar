using System;
using System.Collections.Generic;

namespace Gravicar.Architecture
{
    /// <summary>
    /// Очередь команд.
    /// </summary>
    public class CommandQueue : Queue<CommandInQueue>
    {
        private readonly object _queueLocker = new object ();

        private int _idCounter;

        public CommandQueue () : base ()
        {
            _idCounter = 0;
        }

        public void PushCommand (Command command, TimeSpan executionDuration)
        {
            lock (_queueLocker)
            {
                Enqueue (new CommandInQueue (command, _idCounter++, executionDuration));
            }
        }

        public CommandInQueue PullCommand ()
        {
            lock (_queueLocker)
            {
                return Dequeue ();
            }
        }
    }
}