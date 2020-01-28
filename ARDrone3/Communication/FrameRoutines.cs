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
        /// <param name="sequenceNumber"><see cref="SequenceNumber"/>, который будет присвоен фрейму.</param>
        /// <param name="index">Смещение в возвращаемом массиве после завершения работы метода (кодирования заголовка фрейма)</param>
        internal static byte [] EncodeFrameHeader (INetworkFrame frameToEncode, byte sequenceNumber, out int index)
        {
            frameToEncode.SequenceNumber = sequenceNumber;

            byte [] encodedByteArray = new byte [frameToEncode.TotalSize];

            encodedByteArray [0] = (byte)frameToEncode.DataType;
            encodedByteArray [1] = (byte)frameToEncode.TargetBufferId;
            encodedByteArray [2] = sequenceNumber;

            BitConverter.GetBytes (frameToEncode.TotalSize).CopyTo (encodedByteArray, Const.Communication.IndexToEncodeFrameTotalSize);

            index = Const.Communication.IndexAfterEncodingFrameHeader;

            return encodedByteArray;
        }
    }
}