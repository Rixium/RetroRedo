using Autofac;
using RetroRedo.Content;
using RetroRedo.Input;
using RetroRedo.Screen;
using RetroRedo.Window;

namespace RetroRedo.Modules
{
    public class GameModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContentChest>().As<IContentChest>().InstancePerLifetimeScope();
            builder.RegisterType<ScreenProvider>().As<IScreenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<WindowSettings>().As<IWindowSettings>().InstancePerLifetimeScope();

            RegisterServices(builder);
            RegisterScreenTypes(builder);
            
            base.Load(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ScreenService>().As<IScreenService>().InstancePerLifetimeScope();
            builder.RegisterType<InputService>().As<IInputService>().InstancePerLifetimeScope();
        }

        private static void RegisterScreenTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameScreen>().As<IScreen>();
            builder.RegisterType<MainMenuScreen>().As<IScreen>();
            builder.RegisterType<SplashScreen>().As<IScreen>();
        }
    }
}