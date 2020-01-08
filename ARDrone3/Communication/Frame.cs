using System;
using System.Runtime.InteropServices;

namespace ARDrone3.Communication
{
    /// <summary>
    /// Фрейм данных, пересылаемого по сети, в соответствии со спецификацией ARDrone3.
    /// </summary>
    public abstract class Frame
    {
        /// <summary>
        /// Тип данных, содержащихся в фрейме.
        /// </summary>
        private EFrameDataType DataType { get; set; }

        /// <summary>
        /// Фактически дополнительное поле, характеризующее данные, содержащиеся в фрейме.
        /// </summary>
        internal EFrameTargetBufferId TargetBufferId { get; set; }

        /// <summary>
        /// Некий последовательный номер фрейма. Инкрементируется, когда отправляются новые данные, но не когда те же самые данные 
        /// пытаются отправить повторно.
        /// </summary>
        internal byte SequenceNumber { get; set; }

        /// <summary>
        /// Общий размер фрейма - и заголовок, и данные. В формате Little endian.
        /// </summary>
        protected int TotalSize { get; set; }

        protected Frame (EFrameDataType dataType, EFrameTargetBufferId targetBufferId, byte sequenceNumber, int totalSize)
        {
            DataType       = dataType;
            TargetBufferId = targetBufferId;
            SequenceNumber = sequenceNumber;
            TotalSize      = totalSize;
        }

        /// <summary>
        /// Кодирует данные фрейма в виде массива байтов.
        /// </summary>
        /// <param name="array">Массив, в который складываются данные фрейма.</param>
        /// <returns>Смещение относительно начала массива после того, как отработает данный метод.</returns>
        /// <remarks>В данной (базовой) реализации в массив <paramref name="array"/> записывается только заголовок фрейма.</remarks>
        public virtual int EncodeTo (byte [] array)
        {
            array [0] = (byte)DataType;
            array [1] = (byte)TargetBufferId;
            array [2] = SequenceNumber;

            BitConverter.GetBytes (TotalSize).CopyTo (array, SizeOfFirstThreeFields ());

            return SizeOfHeader ();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        // Статические методы для вычисления размеров заголовка фрейма и его отдельных блоков.
        //////////////////////////////////////////////////////////////////////////////////////

        private static int SizeOfFirstThreeFields ()
        {
            return Marshal.SizeOf<EFrameDataType> () + Marshal.SizeOf<EFrameTargetBufferId> () + Marshal.SizeOf<byte> ();
        }

        internal static int SizeOfHeader ()
        {
            return SizeOfFirstThreeFields () + Marshal.SizeOf<int> ();
        }
    }
}