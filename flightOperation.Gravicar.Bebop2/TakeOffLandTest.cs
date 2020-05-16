using System;
using System.Threading;

using Gravicar.Bebop2;

namespace flightOperation.Gravicar.Bebop2
{
    internal static class TakeOffLandTest
    {
        internal static void Run ()
        {
            Bebop2Quadrotor drone = new Bebop2Quadrotor ();

            if (drone.StartSession () == true)
            {
                drone.TakeOff ();
                Console.WriteLine ("TakeOff command is pushed");

                Console.WriteLine ("Thread is sleeping for 2 sec");
                Thread.Sleep (2000);

                drone.Land ();
                Console.WriteLine ("Land command is pushed");

                drone.EndSession ();
            }

            else Console.WriteLine ("The network connection is not set up.");

            drone.Dispose ();
        }
    }
}