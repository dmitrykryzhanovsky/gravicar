using Wormhole.Network;

using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Network;

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

            internal static class ConnectionType
            {
                internal static class CToV
                {
                    internal const EConnectionType SendCommands = EConnectionType.Udp;
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

        internal static class Frame
        {
            internal const int HeaderLength = sizeof (EFrameDataType) + sizeof (EFrameTargetBufferId) + sizeof (byte) + sizeof (int);
        }

        internal static class Command
        {
            internal const int HeaderLength = sizeof (ECommandProject) + sizeof (ECommandClass) + sizeof (ECommandId);

            internal const int FrameAndCommandHeaderLength = Frame.HeaderLength + Command.HeaderLength;

            internal static class ParametersSegmentLength
            {
                internal const int TakeOff = 0;

                internal const int Land = 0;
            }
        }
    }
}