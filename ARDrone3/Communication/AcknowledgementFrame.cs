using System.Runtime.InteropServices;

namespace ARDrone3.Communication
{
    /// <summary>
    /// Фрейм, подтверждающий ранее пересланные данные.
    /// </summary>
    public class AcknowledgementFrame : Frame
    {
        /// <summary>
        /// Последовательный номер фрейма, который нужно подтвердить.
        /// </summary>
        /// <remarks>В подтверждающем фрейме это поле играет роль данных фрейма.</remarks>
        private byte FrameToConfirmSequneceNumber { get; set; }

        public AcknowledgementFrame (Frame frameToConfirm, byte sequenceNumber) : 
            base (EFrameDataType.Acknowledgement, frameToConfirm.TargetBufferId + 128, sequenceNumber, Frame.SizeOfHeader () + Marshal.SizeOf<byte> ())
        {
            // Чтобы отправить подтверждение, нужно к Target buffer ID исходных данных прибавить 128, а Sequence number исходных 
            // данных переслать как данные подтверждающего фрейма.

            FrameToConfirmSequneceNumber = frameToConfirm.SequenceNumber;
        }

        public override int EncodeTo (byte [] array)
        {
            int index = base.EncodeTo (array);

            array [index] = FrameToConfirmSequneceNumber;

            return TotalSize;
        }
    }
}