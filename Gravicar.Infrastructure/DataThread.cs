using System;
using System.Threading;

namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Поток для пересылки данных.
    /// </summary>
    internal class DataThread : IDisposable
    {
        private Thread                  _dataTransferThread;
        private CancellationTokenSource _cancellationTokenSource;

        internal bool IsCancellationRequested
        {
            get
            {
                return _cancellationTokenSource.IsCancellationRequested;
            }
        }

        internal DataThread (ThreadStart loop)
        {
            _dataTransferThread      = new Thread (loop);
            _cancellationTokenSource = null;

            _dataTransferThread.IsBackground = false;
        }
        
        internal void Start ()
        {
            _cancellationTokenSource = new CancellationTokenSource ();

            _dataTransferThread.Start ();
        }

        internal void Cancel ()
        {
            _cancellationTokenSource.Cancel ();
        }

        public void Dispose ()
        {
            if (_cancellationTokenSource != null) _cancellationTokenSource.Dispose ();
        }
    }
}