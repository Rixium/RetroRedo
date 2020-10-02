using System;
using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RetroRedo.Modules
{
    public class MonoGameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>();
            builder.RegisterType<ContentManager>();
            builder.RegisterType<GameServiceContainer>().As<IServiceProvider>();
            base.Load(builder);
        }
    }
}