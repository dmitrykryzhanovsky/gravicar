using System;

using Gravicar.Bebop2;

namespace flightOperation.Gravicar.Bebop2
{
    internal static class DiscoverTest
    {
        internal static void Run ()
        {
            Bebop2Quadrotor drone = new Bebop2Quadrotor ();

            if (drone.Discover (out string responseJson) == true)
            {
                Console.WriteLine ("Discovering is OK");
                Console.WriteLine ("JSON response: {0}", responseJson);
            }

            else
            {
                Console.WriteLine ("Discovering is failed");
                if (responseJson != String.Empty) Console.WriteLine ("JSON response: {0}", responseJson);
            }

            drone.Dispose ();
        }
    }
}