using Autofac;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Modules
{
    public class GameModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContentChest>().As<IContentChest>().InstancePerLifetimeScope();
            builder.RegisterType<ScreenProvider>().As<IScreenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<ScreenService>().As<IScreenService>().InstancePerLifetimeScope();

            RegisterScreenTypes(builder);
            
            base.Load(builder);
        }

        private static void RegisterScreenTypes(ContainerBuilder builder)
        {
            builder.RegisterType<GameScreen>().As<IScreen>().InstancePerLifetimeScope();
            builder.RegisterType<MainMenuScreen>().As<IScreen>().InstancePerLifetimeScope();
            builder.RegisterType<SplashScreen>().As<IScreen>().InstancePerLifetimeScope();
        }
    }
}