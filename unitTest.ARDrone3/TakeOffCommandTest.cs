using Microsoft.VisualStudio.TestTools.UnitTesting;

using Gravicar.ARDrone3.Commands;

namespace unitTest.ARDrone3
{
    [TestClass]
    public class TakeOffCommandTest
    {
        [TestMethod]
        public void TotalSize ()
        {
            TakeOffCommand command = new TakeOffCommand ();

            int expected = 11;
            int actual   = command.TotalSize;

            Assert.AreEqual (expected, actual);
        }

        [TestMethod]
        public void EncodeTo ()
        {
            TakeOffCommand command = new TakeOffCommand ();

            byte [] expected = new byte [] { 2, 10, 73, 11, 0, 0, 0, 1, 0, 1, 0 };
            byte [] actual   = command.Encode (73);

            Assert.AreEqual (expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++) Assert.AreEqual (expected [i], actual [i]);
        }
    }
}