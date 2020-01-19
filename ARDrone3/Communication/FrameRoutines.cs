using System;

namespace Gravicar.ARDrone3.Communication
{
    /// <summary>
    /// Методы для работы с фреймами.
    /// </summary>
    internal static class FrameRoutines
    {
        /// <summary>
        /// Кодирует заголовок фрейма в виде массива байтов.
        /// </summary>
        /// <param name="frameToEncode">Фрейм, который нужно закодировать.</param>
        /// <param name="array">Массив, в который записывается байтовое представление фрейма.</param>
        /// <param name="sequenceNumber"><see cref="SequenceNumber"/>, который будет присвоен фрейму.</param>
        /// <returns>Возвращает смещение в массиве <paramref name="array"/> после завершения работы методы (кодирования заголовка фрейма).</returns>
        internal static int EncodeFrameHeaderTo (INetworkFrame frameToEncode, byte [] array, byte sequenceNumber)
        {
            frameToEncode.SequenceNumber = sequenceNumber;

            array [0] = (byte)frameToEncode.DataType;
            array [1] = (byte)frameToEncode.TargetBufferId;
            array [2] = sequenceNumber;

            BitConverter.GetBytes (frameToEncode.TotalSize).CopyTo (array, Const.Communication.IndexToEncodeFrameTotalSize);

            return Const.Communication.FrameHeaderSize;
        }
    }
}