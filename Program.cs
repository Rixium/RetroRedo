using System;
using Autofac;
using Microsoft.Xna.Framework;
using RetroRedo.Modules;

namespace RetroRedo
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MonoGameModule>();
            containerBuilder.RegisterModule<GameModule>();
            var container = containerBuilder.Build();

            var game = container.Resolve<Game>();
            game.Run();
            
            game.Dispose();
        }
    }
}
