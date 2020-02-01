using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3
{
    internal static class Const
    {
        internal static class Communication
        {
            /// <summary>
            /// Размер заголовка фрейма – первые три поля (DataType, TargetBufferId, SequenceNumber).
            /// </summary>
            private const int FrameHeaderBeginningSize = sizeof (EFrameDataType) + sizeof (EFrameTargetBufferId) + sizeof (byte);

            /// <summary>
            /// Смещение поля «Общий размер фрейма» относительно его начала.
            /// </summary>
            internal const int IndexToEncodeFrameTotalSize = FrameHeaderBeginningSize;

            /// <summary>
            /// Размер заголовка фрейма – все поля (DataType, TargetBufferId, SequenceNumber, TotalSize).
            /// </summary>
            internal const int FrameHeaderSize = FrameHeaderBeginningSize + sizeof (int);

            /// <summary>
            /// Смещение относительно начала фрейма после его заголовка.
            /// </summary>
            internal const int IndexAfterEncodingFrameHeader = FrameHeaderSize;

            /// <summary>
            /// Общий размер подтверждающего фрейма.
            /// </summary>
            internal const int AcknowledgmentFrameTotalSize = FrameHeaderSize + sizeof (byte);
        }

        internal static class Commands
        {
            /// <summary>
            /// Размер заголовка команды – первые два поля (ProjectId, ClassId).
            /// </summary>
            private const int CommandHeaderBeginningSize = sizeof (ECommandProjectId) + sizeof (ECommandClassId);

            /// <summary>
            /// Смещение поля CommandId относительно начала фрейма.
            /// </summary>
            internal const int IndexToEncodeCommandId = Communication.IndexAfterEncodingFrameHeader + CommandHeaderBeginningSize;

            /// <summary>
            /// Размер заголовка команды – все поля (ProjectId, ClassId, CommandId).
            /// </summary>
            private const int CommandHeaderSize = CommandHeaderBeginningSize + sizeof (ECommandId);

            /// <summary>
            /// Общий размер фрейма команды, не содержащей параметры.
            /// </summary>
            internal const int CommandNoParametersSize = Communication.FrameHeaderSize + CommandHeaderSize;

            /// <summary>
            /// Смещение параметров команды относительно начала фрейма.
            /// </summary>
            internal const int IndexToEncodeCommandParameters = Communication.IndexAfterEncodingFrameHeader + CommandHeaderSize;
        }
    }
}