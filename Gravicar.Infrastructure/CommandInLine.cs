using System;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Инкапсулирует команду в очереди команд.
    /// </summary>
    internal class CommandInLine
    {
        /// <summary>
        /// Статус команды, находящейся в очереди команд.
        /// </summary>
        private enum ECommandInLineStatus : int
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
        internal Command Command { get; private set; }

        /// <summary>
        /// Идентификатор команды в очереди.
        /// </summary>
        private int Id { get; set; }

        /// <summary>
        /// Состояние команды.
        /// </summary>
        private ECommandInLineStatus Status { get; set; }

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

        internal CommandInLine (Command command, int id, TimeSpan executionDuration)
        {
            Command            = command;
            Id                 = id;
            Status             = ECommandInLineStatus.NotTakenYet;
            ExecutionStartTime = ExecutionStartTimeDefaultValue;
            ExecutionDuration  = executionDuration;
        }

        /// <summary>
        /// Переводим команду на обработку / выполнение.
        /// </summary>
        internal void Execute ()
        {
            if (Status != ECommandInLineStatus.NotTakenYet) throw new InvalidOperationException ();

            Status             = ECommandInLineStatus.BeingProcessed;
            ExecutionStartTime = DateTime.Now;
        }

        /// <summary>
        /// Переводим команду в состояние «Выполнена / отправлена» (либо снимаем с выполнения).
        /// </summary>
        internal void Finish ()
        {
            if (Status != ECommandInLineStatus.BeingProcessed) throw new InvalidOperationException ();

            Status = ECommandInLineStatus.Passed;
        }
    }
}