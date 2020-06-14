namespace Gravicar.ARDrone3.Network
{
    /// <summary>
    /// Базовый класс для фреймов в соответствии с протоколом ARSDK, пересылаемых по сети.
    /// </summary>
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