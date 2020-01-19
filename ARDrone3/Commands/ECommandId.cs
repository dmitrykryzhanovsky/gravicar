namespace Gravicar.ARDrone3.Commands
{
    /// <summary>
    /// Идентификатор команды внутри класса.
    /// </summary>
    public enum ECommandId : ushort
    {
        /// <summary>
        /// Взлёт.
        /// </summary>
        TakeOff = 1,

        /// <summary>
        /// Посадка.
        /// </summary>
        Land = 3
    }
}