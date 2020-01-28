﻿using System;

using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3.Commands
{
    /// <summary>
    /// Базовый класс для команд.
    /// </summary>
    public abstract class Command : INetworkFrame
    {
        public abstract EFrameDataType DataType { get; }

        public abstract EFrameTargetBufferId TargetBufferId { get; }

        public byte SequenceNumber { get; set; }

        public int TotalSize
        {
            get
            {
                return Const.Commands.CommandNoParametersSize + CommandParametersSize;
            }
        }

        /// <summary>
        /// Размер (в байтах) блока параметров команды при кодировании в байтовый массив. В том случае, сели у команды нет параметров, он равен 0.
        /// </summary>
        protected virtual int CommandParametersSize
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Feature / Project ID команды.
        /// </summary>
        public abstract ECommandProjectId ProjectId { get; }

        /// <summary>
        /// Class ID in the feature / project команды.
        /// </summary>
        public abstract ECommandClassId ClassId { get; }

        /// <summary>
        /// Идентификатор команды внутри класса.
        /// </summary>
        public abstract ECommandId CommandId { get; }

        public byte [] Encode (byte sequenceNumber)
        {
            byte [] encodedByteArray = FrameRoutines.EncodeFrameHeader (this, sequenceNumber, out int index);

            encodedByteArray [index]     = (byte)ProjectId;
            encodedByteArray [index + 1] = (byte)ClassId;

            BitConverter.GetBytes ((ushort)CommandId).CopyTo (encodedByteArray, Const.Commands.IndexToEncodeCommandId);

            EncodeCommandParametersTo (encodedByteArray, Const.Commands.IndexToEncodeCommandParameters);

            return encodedByteArray;
        }

        /// <summary>
        /// Кодирует блок параметров команды в виде массива байтов.
        /// </summary>
        /// <param name="array">Массив, в который записывается байтовое представление фрейма.</param>
        /// <param name="index">Индекс (смещение) в массиве <paramref name="array"/>, с которого начинается запись байтового представления параметров команды.</param>
        protected virtual void EncodeCommandParametersTo (byte [] array, int index)
        {
        }
    }
}