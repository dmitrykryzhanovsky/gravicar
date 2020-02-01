using System;

using Gravicar.Bebop2Quadrotor;

namespace runtimeTest.Bebop2Quadrotor.ConsoleApplication
{
    internal static class DiscoverTest
    {
        internal static void Run ()
        {
            Bebop2QuadrotorVehicle drone = new Bebop2QuadrotorVehicle ();

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