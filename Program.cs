using System;
using Autofac;
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

            var game = container.Resolve<Game1>();
            game.Run();
            
            game.Dispose();
        }
    }
}
