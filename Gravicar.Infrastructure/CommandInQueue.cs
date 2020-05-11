using System;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Инкапсулирует команду в очереди команд.
    /// </summary>
    internal class CommandInQueue
    {
        /// <summary>
        /// Статус команды, находящейся в очереди команд.
        /// </summary>
        private enum ECommandInQueueStatus : int
        {
            /// <summary>
            /// Команда ещё не выполнялась / не отправлялась.
            /// </summary>
            NotTakenYet,

            /// <summary>
            /// Команда ставится / находится на обработке / выполнении в данный момент.
            /// </summary>
            BeingProcessed,

            /// <summary>
            /// Команда выполнена / отправлена (либо была снята с выполнения).
            /// </summary>
            Passed
        }

        private static readonly DateTime ExecutionStartTimeDefaultValue = DateTime.MaxValue;

        private static readonly TimeSpan ExecutionDurationDefaultValue = TimeSpan.MaxValue;

        /// <summary>
        /// Исходная команда, которую необходимо передать на дрон.
        /// </summary>
        /// <remarks>Имеется ввиду команда сама по себе, не привязанная к очереди.</remarks>
        internal Command Command { get; private set; }

        /// <summary>
        /// Идентификатор команды в очереди.
        /// </summary>
        private int Id { get; set; }

        /// <summary>
        /// Состояние команды.
        /// </summary>
        private ECommandInQueueStatus Status { get; set; }

        /// <summary>
        /// Момент, когда должно начаться выполнение команды.
        /// </summary>
        private DateTime ExecutionStartTime { get; set; }

        /// <summary>
        /// Длительность времени выполнения команды.
        /// </summary>
        private TimeSpan ExecutionDuration { get; set; }

        /// <summary>
        /// Возвращает TRUE, если время, отведённое для выполнения команды, ещё не истекло.
        /// </summary>
        internal bool ExecutionTimeNotExpired
        {
            get
            {
                return (DateTime.Now - ExecutionStartTime <= ExecutionDuration);
            }
        }

        internal CommandInQueue (Command command, int id, TimeSpan executionDuration)
        {
            Command            = command;
            Id                 = id;
            Status             = ECommandInQueueStatus.NotTakenYet;
            ExecutionStartTime = ExecutionStartTimeDefaultValue;
            ExecutionDuration  = executionDuration;
        }

        internal CommandInQueue (Command command, int id) : this (command, id, ExecutionDurationDefaultValue)
        {
        }

        /// <summary>
        /// Переводим команду на обработку / выполнение.
        /// </summary>
        internal void Execute ()
        {
            if (Status != ECommandInQueueStatus.NotTakenYet) throw new InvalidOperationException ();

            Status             = ECommandInQueueStatus.BeingProcessed;
            ExecutionStartTime = DateTime.Now;
        }

        /// <summary>
        /// Переводим команду в состояние «Выполнена / отправлена» (либо снимаем с выполнения).
        /// </summary>
        internal void Finish ()
        {
            if (Status != ECommandInQueueStatus.BeingProcessed) throw new InvalidOperationException ();

            Status = ECommandInQueueStatus.Passed;
        }
    }
}