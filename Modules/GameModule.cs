using Autofac;
using RetroRedo.Content;
using RetroRedo.Screen;

namespace RetroRedo.Modules
{
    public class GameModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContentChest>().As<IContentChest>();
            builder.RegisterType<ScreenProvider>().As<IScreenProvider>();
            builder.RegisterType<ScreenService>().As<IScreenService>();
            base.Load(builder);
        }
    }
}