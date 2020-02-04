using System.Net.Sockets;

namespace Gravicar.Communication
{
    internal class TcpCommunicationChannel : CommunicationChannel
    {
        private TcpClient _connection;

        internal TcpCommunicationChannel ()
        {
            _connection = new TcpClient ();
        }

        internal override bool OpenConnection (string hostAddress, int port)
        {
            try
            {
                _connection.Connect (hostAddress, port);

                return true;
            }

            catch
            {
                return false;
            }
        }

        internal override void CloseConnection ()
        {
            _connection.Close ();
        }

        public override void Dispose ()
        {
            _connection.Dispose ();
        }

        internal override void SendBytes (byte [] array, int length)
        {
            _connection.GetStream ().Write (array, 0, length);
        }
    }
}