using System;

namespace RSandor_BrickbreakerXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BrickBreaker game = new BrickBreaker())
            {
                game.Run();
            }
        }
    }
#endif
}

