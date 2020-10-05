using Microsoft.Xna.Framework.Input;
using RetroRedo.Commands;
using RetroRedo.Entities;
using RetroRedo.Services;

namespace RetroRedo.Components
{
    public class PlayerMovementComponent : IComponent
    {
        
        public IEntity Entity { get; set; }

        public void Begin()
        {
            Game1.Input.OnKeyPressed(Keys.D, () =>
            {
                var map = Entity.CurrentMap;

                if (map.TileIsOpen(Entity.X + 1, Entity.Y))
                {
                    DoCommand(new MoveCommand(1, 0));
                }
            });
            
            Game1.Input.OnKeyPressed(Keys.A, () =>
            {
                var map = Entity.CurrentMap;

                if (map.TileIsOpen(Entity.X - 1, Entity.Y))
                {
                    DoCommand(new MoveCommand(-1, 0));
                }
            });
            
            Game1.Input.OnKeyPressed(Keys.S, () =>
            {
                var map = Entity.CurrentMap;

                if (map.TileIsOpen(Entity.X, Entity.Y + 1))
                {
                    DoCommand(new MoveCommand(0, 1));
                }
            });
            
            Game1.Input.OnKeyPressed(Keys.W, () =>
            {
                var map = Entity.CurrentMap;

                if (map.TileIsOpen(Entity.X, Entity.Y - 1))
                {
                    DoCommand(new MoveCommand(0, -1));
                }
            });
            
            Game1.Input.OnKeyPressed(Keys.Space, () =>
            { 
                DoCommand(new WaitCommand());
            });
        }

        private void DoCommand(ICommand command)
        {
            var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
            commandSetComponent.PushCommand(command);
            TurnService.PlayersTurn = false;
        }

        public void Update()
        {
        }
    }
}