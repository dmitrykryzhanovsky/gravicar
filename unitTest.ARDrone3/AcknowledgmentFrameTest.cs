using Microsoft.VisualStudio.TestTools.UnitTesting;

using Gravicar.ARDrone3.Commands;
using Gravicar.ARDrone3.Communication;

namespace unitTest.ARDrone3
{
    [TestClass]
    public class AcknowledgmentFrameTest
    {
        [TestMethod]
        public void TotalSize ()
        {
            TakeOffCommand      frameToConfirm = new TakeOffCommand ();
            AcknowledgmentFrame acknowledgment = new AcknowledgmentFrame (frameToConfirm);

            int expected = 8;
            int actual   = acknowledgment.TotalSize;

            Assert.AreEqual (expected, actual);
        }

        [TestMethod]
        public void EncodeTo ()
        {
            TakeOffCommand frameToConfirm = new TakeOffCommand ();
            frameToConfirm.Encode (73);

            AcknowledgmentFrame acknowledgment = new AcknowledgmentFrame (frameToConfirm);

            byte [] expected = new byte [] { 1, 138, 42, 8, 0, 0, 0, 73 };
            byte [] actual   = acknowledgment.Encode (42);

            Assert.AreEqual (expected.Length, actual.Length);
            for (int i = 0; i < actual.Length; i++) Assert.AreEqual (expected [i], actual [i]);
        }
    }
}