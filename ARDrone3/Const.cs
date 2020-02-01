using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Communication;

namespace Gravicar.ARDrone3
{
    internal static class Const
    {
        /// <summary>
        /// Параметры сетевых подключений для Parrot ARDrone3.
        /// </summary>
        internal static class NetworkConnection
        {
            /// <summary>
            /// Сетевой адрес дрона.
            /// </summary>
            internal const string DroneHostAddress = "192.168.42.1";

            /// <summary>
            /// Порты подключений.
            /// </summary>
            internal static class Port
            {
                /// <summary>
                /// От управляющего клиента к устройству (дрону).
                /// </summary>
                internal static class CToV
                {
                    /// <summary>
                    /// Порт для поиска дрона и установки с ним первичного соединения.
                    /// </summary>
                    internal const int Discover = 44444;

                    /// <summary>
                    /// Порт для доставки команд от контроллера на дрон.
                    /// </summary>
                    internal const int SendCommands = 54321;
                }

                /// <summary>
                /// От устройства (дрона) к управляющему клиенту.
                /// </summary>
                internal static class VToC
                {
                    internal const int Port = 43210;

                    internal const int Stream = 55004;

                    internal const int Control = 55004;
                }
            }
        }

        /// <summary>
        /// Поиск дрона и установка с ним первичного соединения.
        /// </summary>
        internal static class Discovering
        {            
            /// <summary>
            /// JSON-запрос, отправляемый на дрон при поиске и установке первичного соединения.
            /// </summary>
            internal const string CToV_Json = "{{\"controller_type\":\"{0}\", \"controller_name\":\"{1}\", \"d2c_port\":\"{2}\", \"arstream2_client_stream_port\":\"{3}\",\"arstream2_client_control_port\":\"{4}\"}}";

            internal const string ControllerTypeDefault = "controller_type";

            internal const string ControllerNameDefault = "controller_name";

            /// <summary>
            /// Значение поля status в JSON-запросе, получаемом от дрона после поиска и установки первичного соединения, которое 
            /// соответствует успешному завершению операции.
            /// </summary>
            internal const int VToC_ConnectionIsCorrect = 0;
        }

        /// <summary>
        /// Кодирование данных при пересылке по сети.
        /// </summary>
        internal static class Encoding
        {
            internal static class Frame
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

            internal static class Command
            {
                /// <summary>
                /// Размер заголовка команды – первые два поля (ProjectId, ClassId).
                /// </summary>
                private const int CommandHeaderBeginningSize = sizeof (ECommandProjectId) + sizeof (ECommandClassId);

                /// <summary>
                /// Смещение поля CommandId относительно начала фрейма.
                /// </summary>
                internal const int IndexToEncodeCommandId = Frame.IndexAfterEncodingFrameHeader + CommandHeaderBeginningSize;

                /// <summary>
                /// Размер заголовка команды – все поля (ProjectId, ClassId, CommandId).
                /// </summary>
                private const int CommandHeaderSize = CommandHeaderBeginningSize + sizeof (ECommandId);

                /// <summary>
                /// Общий размер фрейма команды, не содержащей параметры.
                /// </summary>
                internal const int CommandNoParametersSize = Frame.FrameHeaderSize + CommandHeaderSize;

                /// <summary>
                /// Смещение параметров команды относительно начала фрейма.
                /// </summary>
                internal const int IndexToEncodeCommandParameters = Frame.IndexAfterEncodingFrameHeader + CommandHeaderSize;
            }
        }
    }
}