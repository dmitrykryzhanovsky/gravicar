namespace Gravicar
{
    public abstract class Command
    {
        protected Command ()
        {
        }

        public abstract byte [] Encode ();
    }
}