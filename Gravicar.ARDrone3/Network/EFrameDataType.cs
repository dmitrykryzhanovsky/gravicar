﻿namespace Gravicar.ARDrone3.Network
{
    internal enum EFrameDataType : byte
    {
        /// <summary>
        /// Подтверждение ранее пересланных данных.
        /// </summary>
        Acknowledgment = 1,

        /// <summary>
        /// Обычные данные, не требующие подтверждения и без дополнительного приоритета.
        /// </summary>
        Common = 2,

        /// <summary>
        /// Данные с более высоким приоритетом.
        /// </summary>
        LowLatency = 3,

        /// <summary>
        /// Данные, требующие подтверждения.
        /// </summary>
        RequiringForAcknowledgment = 4
    }
}