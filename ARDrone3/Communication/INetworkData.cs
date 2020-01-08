namespace ARDrone3.Communication
{
    public interface INetworkData
    {
        EFrameDataType DataType { get; }

        EFrameTargetBufferId TargetBufferId { get; }

        int FrameTotalSize { get; }

        void EncodeTo (out byte [] array, byte sequenceNumber);
    }
}