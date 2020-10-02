using Autofac;
using RetroRedo.Content;

namespace RetroRedo.Modules
{
    public class GameModule : Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContentChest>().As<IContentChest>();
            base.Load(builder);
        }
    }
}