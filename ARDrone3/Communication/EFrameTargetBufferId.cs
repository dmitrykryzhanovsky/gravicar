namespace Gravicar.ARDrone3.Communication
{
    /// <summary>
    /// Значения, уточняющие тип данных, пересылаемых в фрейме.
    /// </summary>
    public enum EFrameTargetBufferId : byte
    {
        /// <summary>
        /// Controller to vehicle - обычные данные, не требующие подтверждения (например, команды пилотирования и ориентации камеры).
        /// </summary>
        CToV_Common = 10,

        /// <summary>
        /// Controller to vehicle - данные, требующие подтверждения (события, установки параметров).
        /// </summary>
        CToV_RequiringForAcknowledgment = 11,

        /// <summary>
        /// Controller to vehicle - срочные данные.
        /// </summary>
        CToV_Emergency = 12,

        /// <summary>
        /// Controller to vehicle - видео-поток.
        /// </summary>
        CToV_VideoStream = 13,

        /// <summary>
        /// Vehicle to controller - видео-данные.
        /// </summary>
        VToC_VideoStream = 125,

        /// <summary>
        /// Vehicle to controller - данные, требующие подтверждения (события, установки параметров).
        /// </summary>
        VToC_RequiringForAcknowledgment = 126,

        /// <summary>
        /// Vehicle to controller - обычные данные, не требующие подтверждения (периодические отчёты с устройства).
        /// </summary>
        VToC_Common = 127
    }
}