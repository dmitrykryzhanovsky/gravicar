using System;
using System.Threading;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Инкапсулирует функциональность для работы с каналами данных (команды, данные с сенсоров, видеопоток, подтверждения операций и т.д.)
    /// </summary>
    public abstract class DataChannel : IDisposable
    {
        private Thread                  _dataProcessingThread;
        private CancellationTokenSource _cancellationTokenSource;

        protected DataChannel ()
        {
            _dataProcessingThread    = new Thread (Loop);
            _cancellationTokenSource = null;

            _dataProcessingThread.IsBackground = false;
        }

        public void Start ()
        {
            StartPreparation ();

            _cancellationTokenSource = new CancellationTokenSource ();

            _dataProcessingThread.Start ();
        }

        protected abstract void StartPreparation ();

        public void Cancel ()
        {
            _cancellationTokenSource.Cancel ();

            CancelFinalization ();
        }

        protected abstract void CancelFinalization ();

        public virtual void Dispose ()
        {
            if (_cancellationTokenSource != null) _cancellationTokenSource.Dispose ();
        }

        protected abstract void Loop ();
    }
}