using System;

namespace flightOperation.Gravicar.Bebop2
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