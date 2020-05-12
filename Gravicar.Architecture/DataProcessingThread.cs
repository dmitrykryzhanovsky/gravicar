using System;
using System.Threading;

namespace Gravicar.Architecture
{
    /// <summary>
    /// Инкапсулирует функциональность для работы с каналами (потоками) данных (команды, данные с сенсоров, видеопоток, подтверждения операций и т.д.)
    /// </summary>
    internal abstract class DataProcessingThread : IDisposable
    {
        private Thread                  _dataProcessingThread;
        private CancellationTokenSource _cancellationTokenSource;

        protected bool IsCancellationRequested
        {
            get
            {
                return _cancellationTokenSource.IsCancellationRequested;
            }
        }

        protected DataProcessingThread ()
        {
            _dataProcessingThread    = new Thread (Loop);
            _cancellationTokenSource = null;

            _dataProcessingThread.IsBackground = false;
        }

        internal void Start ()
        {
            Prestart ();

            _cancellationTokenSource = new CancellationTokenSource ();

            _dataProcessingThread.Start ();
        }

        protected abstract void Prestart ();

        internal void Cancel ()
        {
            _cancellationTokenSource.Cancel ();

            FinalizeCancelling ();
        }

        protected abstract void FinalizeCancelling ();

        public virtual void Dispose ()
        {
            if (_cancellationTokenSource != null) _cancellationTokenSource.Dispose ();
        }

        /// <summary>
        /// Цикл работы потока, в котором происходит обработка данных.
        /// </summary>
        protected abstract void Loop ();
    }
}