using System;

namespace LonelySoul
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SoulGame())
                game.Run();
        }
    }
}
