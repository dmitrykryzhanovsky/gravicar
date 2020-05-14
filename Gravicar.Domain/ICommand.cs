namespace Gravicar
{
    /// <summary>
    /// Базовый интерфейс для команд дрона.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Кодирует команду в виде массива байтов для передачи её по сети.
        /// </summary>
        byte [] Encode ();
    }
}