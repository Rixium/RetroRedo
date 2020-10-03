using Microsoft.Xna.Framework.Input;
using RetroRedo.Commands;
using RetroRedo.Entities;
using RetroRedo.Input;

namespace RetroRedo.Components
{
    public class PlayerMovementComponent : IComponent
    {
        private readonly IInputService _inputService;

        public IEntity Entity { get; set; }

        public PlayerMovementComponent(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Begin()
        {
            _inputService.OnKeyPressed(Keys.D, () =>
            {
                var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
                commandSetComponent.PushCommand(new MoveCommand(1, 0));
            });
            
            _inputService.OnKeyPressed(Keys.A, () =>
            {
                var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
                commandSetComponent.PushCommand(new MoveCommand(-1, 0));
            });
            
            _inputService.OnKeyPressed(Keys.S, () =>
            {
                var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
                commandSetComponent.PushCommand(new MoveCommand(0, 1));
            });
            
            _inputService.OnKeyPressed(Keys.W, () =>
            {
                var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
                commandSetComponent.PushCommand(new MoveCommand(0, -1));
            });
            
            _inputService.OnKeyPressed(Keys.Z, () =>
            {
                var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
                commandSetComponent.Undo();
            });
        }

        public void Update()
        {
        }
    }
}