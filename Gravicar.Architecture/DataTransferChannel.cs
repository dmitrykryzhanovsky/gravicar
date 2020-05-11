﻿using System;
using System.Threading;

namespace Gravicar.Architecture
{
    /// <summary>
    /// Инкапсулирует функциональность для работы с каналами данных (команды, данные с сенсоров, видеопоток, подтверждения операций и т.д.)
    /// </summary>
    internal abstract class DataTransferChannel : IDisposable
    {
        private Thread                  _dataProcessingThread;
        private CancellationTokenSource _cancellationTokenSource;

        protected DataTransferChannel ()
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
        /// Цикл работы потока, связанного с каналом данных.
        /// </summary>
        protected abstract void Loop ();
    }
}