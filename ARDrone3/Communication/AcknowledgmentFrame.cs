namespace Gravicar.ARDrone3.Communication
{
    /// <summary>
    /// Фрейм – подтверждение данных, ранее пересланных по сети.
    /// </summary>
    public class AcknowledgmentFrame : INetworkFrame
    {
        public EFrameDataType DataType
        {
            get
            {
                return EFrameDataType.Acknowledgment;
            }
        }

        public EFrameTargetBufferId TargetBufferId { get; private set; }

        public byte SequenceNumber { get; set; }

        public int TotalSize
        {
            get
            {
                return Const.Communication.AcknowledgmentFrameTotalSize;
            }
        }

        /// <summary>
        /// <see cref="SequenceNumber"/> подтверждаемого фейма.
        /// </summary>
        private byte FrameToConfirmSequenceNumber { get; set; }

        public AcknowledgmentFrame (INetworkFrame frameToConfirm)
        {
            // Чтобы отправить подтверждение, нужно к TargetBufferId исходных данных прибавить 128, а SequenceNumber исходных данных переслать как данные 
            // подтверждающего фрейма.

            TargetBufferId               = frameToConfirm.TargetBufferId + 128;
            FrameToConfirmSequenceNumber = frameToConfirm.SequenceNumber;
        }

        public void EncodeTo (byte [] array, byte sequenceNumber)
        {
            int index = FrameRoutines.EncodeFrameHeaderTo (this, array, sequenceNumber);

            array [index] = FrameToConfirmSequenceNumber;
        }
    }
}