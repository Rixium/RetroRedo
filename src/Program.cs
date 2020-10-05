using System;

namespace RetroRedo
{
    public static class Program
    {

        [STAThread]
        public static void Main()
        {
            // ReSharper disable once ConvertToUsingDeclaration
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}