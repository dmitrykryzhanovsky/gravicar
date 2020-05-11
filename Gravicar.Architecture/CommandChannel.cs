using System;

namespace Gravicar.Architecture
{
    /// <summary>
    /// Инкапсулирует функциональность для организации очереди команд и их пересылки по сети.
    /// </summary>
    public class CommandChannel : DataTransferChannel
    {
        private CommandQueue   _queue;
        private CommandInQueue _commandBeingExecutedRightNow;

        public event Action<Command> SendCommandToDroneEvent;

        public CommandChannel () : base ()
        {
            _queue = new CommandQueue ();
        }

        protected override void Prestart ()
        {
            _commandBeingExecutedRightNow = null;
        }

        protected override void FinalizeCancelling ()
        {
            _commandBeingExecutedRightNow = null;
        }

        public override void Dispose ()
        {
            _queue.Clear ();

            base.Dispose ();
        }

        /// <summary>
        /// Ставит команду в очередь на исполнение.
        /// </summary>
        public void PushCommand (Command command)
        {
            _queue.PushCommand (command, CommandInQueue.ExecutionDurationDefaultValue);
        }

        /// <summary>
        /// Цикл пересылки команд на дрон.
        /// </summary>
        protected override void Loop ()
        {
            // Пока не поступит запрос на остановку потока...
            while (!IsCancellationRequested)
            {
                // Если текущая команда для выполнения не инициализирована...
                if (_commandBeingExecutedRightNow == null)
                {
                    if (IsCancellationRequested) break;

                    // Если в очереди команд есть команды, то берём первую из них и проверяем, не истекло ли её время выполнения. 
                    // Если истекло, то переводим эту команду в статус завершённой без отправки на дрон. Если не истекло, то переводим 
                    // команду на выполнение.
                    if (_queue.Count > 0)
                    {
                        _commandBeingExecutedRightNow = _queue.PullCommand ();

                        if (_commandBeingExecutedRightNow.ExecutionTimeNotExpired) _commandBeingExecutedRightNow.Execute ();

                        else FinishCommandBeingExecuted ();
                    }
                }

                // Если текущая команда для выполнения инициализирована (есть команда на выполнение), то отправляем команду на дрон и 
                // переводим её в статус завершённой (если в это время не поступит запрос на остановку потока).
                if (_commandBeingExecutedRightNow != null)
                {
                    if (IsCancellationRequested) break;

                    SendCommandToDrone ();
                    FinishCommandBeingExecuted ();
                }
            }
        }

        /// <summary>
        /// Отправка команды на дрон.
        /// </summary>
        private void SendCommandToDrone ()
        {
            SendCommandToDroneEvent (_commandBeingExecutedRightNow.Command);
        }

        /// <summary>
        /// Текущая команда переводится в статус «Выполнена / отправлена / снята с выполнения».
        /// </summary>
        private void FinishCommandBeingExecuted ()
        {
            _commandBeingExecutedRightNow.Finish ();
            _commandBeingExecutedRightNow = null;
        }
    }
}