using System;
using Autofac;
using Microsoft.Xna.Framework;
using RetroRedo.Modules;

namespace RetroRedo
{
    public static class Program
    {
        public static ILifetimeScope Container;

        [STAThread]
        public static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MonoGameModule>();
            containerBuilder.RegisterModule<GameModule>();
            var container = containerBuilder.Build();

            Container = container.BeginLifetimeScope();
            var game = Container.Resolve<Game>();
            game.Run();
            game.Dispose();
            Container.Dispose();
        }
    }
}