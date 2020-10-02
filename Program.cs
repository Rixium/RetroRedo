using System;
using Autofac;
using Microsoft.Xna.Framework;
using RetroRedo.Modules;

namespace RetroRedo
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MonoGameModule>();
            containerBuilder.RegisterModule<GameModule>();
            var container = containerBuilder.Build();

            var scope = container.BeginLifetimeScope();
            var game = scope.Resolve<Game>();
            game.Run();

            game.Dispose();
            scope.Dispose();
        }
    }
}