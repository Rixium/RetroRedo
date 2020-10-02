using Autofac;
using Microsoft.Xna.Framework;

namespace RetroRedo.Modules
{
    public class MonoGameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>().As<Game>();
            base.Load(builder);
        }
    }
}