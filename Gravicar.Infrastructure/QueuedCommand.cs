using System;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Инкапсулирует команду в очереди.
    /// </summary>
    internal class QueuedCommand
    {
        /// <summary>
        /// Статус команды, находящейся в очереди команд.
        /// </summary>
        private enum EQueuedCommandStatus : int
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

        internal static readonly TimeSpan ExecutionDurationDefaultValue = TimeSpan.MaxValue;

        /// <summary>
        /// Исходная команда, которую необходимо передать на дрон.
        /// </summary>
        /// <remarks>Имеется ввиду команда сама по себе, не привязанная к очереди.</remarks>
        internal DroneCommand Command { get; set; }

        /// <summary>
        /// Идентификатор команды в очереди.
        /// </summary>
        private int Id { get; set; }

        /// <summary>
        /// Состояние команды.
        /// </summary>
        private EQueuedCommandStatus Status { get; set; }

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

        #region constructors

        internal QueuedCommand (DroneCommand command, int id, TimeSpan executionDuration)
        {
            Command            = command;
            Id                 = id;
            Status             = EQueuedCommandStatus.NotTakenYet;
            ExecutionStartTime = ExecutionStartTimeDefaultValue;
            ExecutionDuration  = executionDuration;
        }

        #endregion

        /// <summary>
        /// Переводим команду на обработку / выполнение.
        /// </summary>
        internal void Execute ()
        {
            if (Status != EQueuedCommandStatus.NotTakenYet) throw new InvalidOperationException ();

            Status             = EQueuedCommandStatus.BeingProcessed;
            ExecutionStartTime = DateTime.Now;
        }

        /// <summary>
        /// Переводим команду в состояние «Выполнена / отправлена» (либо снимаем с выполнения).
        /// </summary>
        internal void Finish ()
        {
            if (Status != EQueuedCommandStatus.BeingProcessed) throw new InvalidOperationException ();

            Status = EQueuedCommandStatus.Passed;
        }
    }
}