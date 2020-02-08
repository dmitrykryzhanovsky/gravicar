namespace Gravicar.Infrastructure
{
    /// <summary>
    /// Инкапсулирует функциональность для организации очереди команд и их пересылки по сети.
    /// </summary>
    public class CommandChannel : DataChannel
    {
        private CommandQueue  _queue;
        private CommandInLine _commandBeingExecutedRightNow;

        public CommandChannel () : base ()
        {
            _queue = new CommandQueue ();
        }

        protected override void StartPreparation ()
        {
            _commandBeingExecutedRightNow = null;
        }

        protected override void CancelFinalization ()
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
            _queue.PushCommand (command, CommandInLine.ExecutionDurationDefaultValue);
        }

        protected override void Loop ()
        {
        }
    }
}