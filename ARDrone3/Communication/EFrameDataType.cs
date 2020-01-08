namespace ARDrone3.Communication
{
    /// <summary>
    /// Тип данных, содержащихся в фрейме.
    /// </summary>
    public enum EFrameDataType : byte
    {
        /// <summary>
        /// Подтверждение для ранее пересланных данных.
        /// </summary>
        Acknowledgement = 1,

        /// <summary>
        /// Обычные данные, не требующие подтверждения и без высокого приоритета.
        /// </summary>
        Normal = 2,

        /// <summary>
        /// Данные с высоким приоритетом.
        /// </summary>
        HighPriority = 3,

        /// <summary>
        /// Данные, на которые нужно будет прислать подтверждение.
        /// </summary>
        RequestingAcknowledgement = 4
    }
}