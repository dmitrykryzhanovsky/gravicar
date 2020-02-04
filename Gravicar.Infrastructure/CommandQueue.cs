using System;
using System.Collections.Generic;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Очередь команд.
    /// </summary>
    internal class CommandQueue : Queue<QueuedCommand>
    {
        /// <summary>
        /// Локер для предотвращения одновременных операций с очередью разными потоками.
        /// </summary>
        private readonly object _queueLocker = new object ();

        /// <summary>
        /// Счётчик идентификаторов команд в очереди. Инкрементируется при постановке в очереди новой команды.
        /// </summary>
        private int _idCounter;

        #region constructors

        internal CommandQueue ()
        {
            _idCounter = 0;
        }

        #endregion

        /// <summary>
        /// Ставит команду в очередь на исполнение.
        /// </summary>
        /// <param name="command">Команда, которую необходимо поставить в очередь.</param>
        /// <param name="executionDuration">Длительность времени выполнения команды.</param>
        internal void PushCommand (DroneCommand command, TimeSpan executionDuration)
        {
            lock (_queueLocker)
            {
                Enqueue (new QueuedCommand (command, _idCounter++, executionDuration));
            }
        }

        /// <summary>
        /// Извлекает команду – первую в очереди на исполнение.
        /// </summary>
        internal QueuedCommand PullCommand ()
        {
            lock (_queueLocker)
            {
                return Dequeue ();
            }
        }
    }
}