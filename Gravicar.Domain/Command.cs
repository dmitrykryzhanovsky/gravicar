namespace Gravicar
{
    /// <summary>
    /// Базовый класс для команд дрона.
    /// </summary>
    public abstract class Command
    {
        protected Command ()
        {
        }

        /// <summary>
        /// Кодирует команду в виде массива байтов для передачи её по сети.
        /// </summary>
        public abstract byte [] Encode ();
    }
}