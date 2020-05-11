using System;
using System.Collections.Generic;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Очередь команд.
    /// </summary>
    internal class CommandQueue : Queue<CommandInQueue>
    {
        private readonly object _queueLocker = new object ();

        private int _idCounter;

        internal CommandQueue () : base ()
        {
            _idCounter = 0;
        }

        internal void PushCommand (Command command, TimeSpan executionDuration)
        {
            lock (_queueLocker)
            {
                Enqueue (new CommandInQueue (command, _idCounter++, executionDuration));
            }
        }

        internal CommandInQueue PullCommand ()
        {
            lock (_queueLocker)
            {
                return Dequeue ();
            }
        }
    }
}