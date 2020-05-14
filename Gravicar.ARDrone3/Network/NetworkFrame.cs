namespace Gravicar.ARDrone3.Network
{
    internal abstract class NetworkFrame
    {
        private const byte DefaultSequenceNumber = 0;

        protected byte [] _encoded;

        protected NetworkFrame (EFrameDataType dataType, EFrameTargetBufferId targetBufferId, int frameTotalSize)
        {
            _encoded = new byte [frameTotalSize]; 

            FrameKit.EncodeFrameHeaderTo (_encoded, dataType, targetBufferId, DefaultSequenceNumber, frameTotalSize);
        }
    }
}