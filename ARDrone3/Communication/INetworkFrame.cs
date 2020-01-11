namespace Gravicar.ARDrone3.Communication
{
    public interface INetworkFrame
    {
        EFrameDataType DataType { get; }

        EFrameTargetBufferId TargetBufferId { get; }

        byte SequenceNumber { get; set; }

        int TotalSize { get; }

        void EncodeTo (byte [] array, byte sequenceNumber);
    }
}