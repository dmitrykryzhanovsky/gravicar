using System;

namespace runtimeTest.Bebop2Quadrotor.ConsoleApplication
{
    internal static class Program
    {
        private static void Main (string [] args)
        {
            DiscoverTest.Run ();

            Console.WriteLine ("\nPress any key...");
            Console.ReadKey ();
        }
    }
}