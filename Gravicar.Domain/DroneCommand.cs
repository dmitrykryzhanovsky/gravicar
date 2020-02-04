namespace Gravicar
{
    /// <summary>
    /// Базовый класс для команд, выполняемых дроном.
    /// </summary>
    public abstract class DroneCommand : IDataToSend
    {
        public abstract byte [] Encode ();
    }
}