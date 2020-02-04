using System;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Контроллер для пересылки данных.
    /// </summary>
    public class DataThreadController : IDisposable
    {
        #region variables

        private CommandQueue  _commandQueue;
        private QueuedCommand _commandBeingExecutedRightNow;

        private DataThread _sendCommandsToDroneThread;

        #endregion

        #region events 

        public event Action<DroneCommand> SendCommandToDroneEvent;

        #endregion

        #region constructors

        public DataThreadController ()
        {
            _commandQueue = new CommandQueue ();

            _sendCommandsToDroneThread = new DataThread (SendCommandsToDroneLoop);
        }

        #endregion

        public void StartThread ()
        {
            _commandBeingExecutedRightNow = null;

            _sendCommandsToDroneThread.Start ();
        }

        public void CancelThread ()
        {
            _sendCommandsToDroneThread.Cancel ();

            _commandBeingExecutedRightNow = null;
        }

        public void Dispose ()
        {
            _sendCommandsToDroneThread.Dispose ();
            _commandQueue.Clear ();
        }

        /// <summary>
        /// Ставит команду в очередь на исполнение.
        /// </summary>
        /// <param name="command">Команда, которую необходимо поставить в очередь.</param>
        /// <remarks>Для длительности выполнения команды используется значение по умолчанию.</remarks>
        public void PushCommand (DroneCommand command)
        {
            PushCommand (command, QueuedCommand.ExecutionDurationDefaultValue);
        }

        /// <summary>
        /// Ставит команду в очередь на исполнение.
        /// </summary>
        /// <param name="command">Команда, которую необходимо поставить в очередь.</param>
        /// <param name="executionDuration">Длительность времени выполнения команды.</param>
        private void PushCommand (DroneCommand command, TimeSpan executionDuration)
        {
            _commandQueue.PushCommand (command, executionDuration);
        }

        /// <summary>
        /// Цикл пересылки команд на дрон.
        /// </summary>
        private void SendCommandsToDroneLoop ()
        {
            // Пока не поступит запрос на остановку потока...
            while (!_sendCommandsToDroneThread.IsCancellationRequested)
            {
                // Если текущая команда для выполнения не инициализирована...
                if (_commandBeingExecutedRightNow == null)
                {
                    if (_sendCommandsToDroneThread.IsCancellationRequested) break;

                    // Если в очереди команд есть команды, то берём первую из них и проверяем, не истекло ли её время выполнения. 
                    // Если истекло, то переводим эту команду в статус завершённой без отправки на дрон. Если не истекло, то переводим 
                    // команду на выполнение.
                    if (_commandQueue.Count > 0)
                    {
                        _commandBeingExecutedRightNow = _commandQueue.PullCommand ();

                        if (_commandBeingExecutedRightNow.ExecutionTimeNotExpired) _commandBeingExecutedRightNow.Execute ();

                        else FinishCommandBeingExecuted ();
                    }
                }

                // Если текущая команда для выполнения инициализирована (есть команда на выполнение), то отправляем команду на дрон и 
                // переводим её в статус завершённой (если в это время не поступит запрос на остановку потока).
                if (_commandBeingExecutedRightNow != null)
                {
                    if (_sendCommandsToDroneThread.IsCancellationRequested) break;

                    SendCommandToDrone ();
                    FinishCommandBeingExecuted ();
                }
            }
        }

        private void SendCommandToDrone ()
        {
            SendCommandToDroneEvent (_commandBeingExecutedRightNow.Command);
        }

        private void FinishCommandBeingExecuted ()
        {
            _commandBeingExecutedRightNow.Finish ();
            _commandBeingExecutedRightNow = null;
        }
    }
}