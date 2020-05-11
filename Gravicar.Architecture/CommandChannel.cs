namespace Gravicar.Architecture
{
    /// <summary>
    /// Инкапсулирует функциональность для организации очереди команд и их пересылки по сети.
    /// </summary>
    public class CommandChannel : DataTransferChannel
    {
        private CommandQueue   _queue;
        private CommandInQueue _commandBeingExecutedRightNow;

        public CommandChannel () : base ()
        {
            _queue = new CommandQueue ();
        }

        protected override void Prestart ()
        {
            _commandBeingExecutedRightNow = null;
        }

        protected override void FinalizeCancelling ()
        {
            _commandBeingExecutedRightNow = null;
        }

        public override void Dispose ()
        {
            _queue.Clear ();

            base.Dispose ();
        }

        public void PushCommand (Command command)
        {
            _queue.PushCommand (command, CommandInQueue.ExecutionDurationDefaultValue);
        }

        protected override void Loop ()
        {
        }
    }
}