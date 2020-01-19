using Microsoft.VisualStudio.TestTools.UnitTesting;

using Gravicar.ARDrone3.Commands;

namespace unitTest.ARDrone3
{
    [TestClass]
    public class TakeOffCommandTest
    {
        [TestMethod]
        public void EncodeTo ()
        {
            TakeOffCommand command = new TakeOffCommand ();

            byte [] actual = command.Encode (73);
        }
    }
}