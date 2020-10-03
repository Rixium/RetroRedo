using Autofac;
using RetroRedo.Components;
using RetroRedo.Content;
using RetroRedo.Input;
using RetroRedo.Maps;
using RetroRedo.Screen;
using RetroRedo.Services;
using RetroRedo.Window;

namespace RetroRedo.Modules
{
    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScreenProvider>().As<IScreenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<WindowSettings>().As<IWindowSettings>().InstancePerLifetimeScope();
            builder.RegisterType<MapParser>().As<IMapParser>().InstancePerLifetimeScope();
            builder.RegisterType<MapRenderer>().As<IMapRenderer>();

            RegisterContentLoaders(builder);
            RegisterServices(builder);
            RegisterScreenTypes(builder);
            RegisterComponents(builder);

            base.Load(builder);
        }

        private static void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerMovementComponent>();
            builder.RegisterType<CommandSetComponent>();
            builder.RegisterType<AutoCommandComponent>();
        }

        private void RegisterContentLoaders(ContainerBuilder builder)
        {
            builder.RegisterType<ContentChest>().As<IContentChest>().InstancePerLifetimeScope();
            builder.RegisterType<MapLoader>().As<IMapLoader>().InstancePerLifetimeScope();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ScreenService>().As<IScreenService>().InstancePerLifetimeScope();
            builder.RegisterType<InputService>().As<IInputService>().SingleInstance();
            builder.RegisterType<GameTimeService>().As<IGameTimeService>().SingleInstance();
            builder.RegisterType<GameStateService>().As<IGameStateService>().InstancePerLifetimeScope();
            builder.RegisterType<MapEntityHistoryService>().As<IMapEntityHistoryService>().InstancePerDependency();
            builder.RegisterType<TurnService>().As<ITurnService>().SingleInstance();
        }

        private static void RegisterScreenTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameScreen>().As<IScreen>();
            builder.RegisterType<MainMenuScreen>().As<IScreen>();
            builder.RegisterType<SplashScreen>().As<IScreen>();
            builder.RegisterType<MapTransitionScreen>().As<IScreen>();
        }
    }
}