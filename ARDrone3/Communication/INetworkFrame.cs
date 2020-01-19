namespace Gravicar.ARDrone3.Communication
{
    /// <summary>
    /// Интерфейс, который должны реализовывать все сущности, пересылаемые по сети в виде фреймов согласно документации ARDrone3 (ARSDK_Protocols).
    /// </summary>
    public interface INetworkFrame
    {
        /// <summary>
        /// Тип данных, переселяемых в фрейме.
        /// </summary>
        EFrameDataType DataType { get; }

        /// <summary>
        /// Дополнительное поле, уточняющее тип данных, пересылаемых в фрейме.
        /// </summary>
        EFrameTargetBufferId TargetBufferId { get; }

        /// <summary>
        /// Последовательный номер фрейма. Инкрементируется, когда пересылаются новые данные. Если осуществляется повторная попытка пересылки старых данных, 
        /// этот номер не изменяется.
        /// </summary>
        byte SequenceNumber { get; set; }

        /// <summary>
        /// Общий размер фрейма в байтах.
        /// </summary>
        int TotalSize { get; }

        /// <summary>
        /// Кодирует фрейм (и заголовок, и данные) в виде массива байтов.
        /// </summary>
        /// <param name="sequenceNumber"><see cref="SequenceNumber"/>, который будет присвоен фрейму.</param>
        byte [] Encode (byte sequenceNumber);
    }
}