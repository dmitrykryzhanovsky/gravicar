using System;
using System.Collections.Generic;

namespace Gravicar.Infrastructure
{
    internal class CommandQueue : Queue<CommandInLine>
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
                Enqueue (new CommandInLine (command, _idCounter++, executionDuration));
            }
        }

        internal CommandInLine PullCommand ()
        {
            lock (_queueLocker)
            {
                return Dequeue ();
            }
        }
    }
}