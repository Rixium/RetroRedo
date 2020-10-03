using Microsoft.Xna.Framework.Input;
using RetroRedo.Commands;
using RetroRedo.Entities;
using RetroRedo.Input;
using RetroRedo.Services;

namespace RetroRedo.Components
{
    public class PlayerMovementComponent : IComponent
    {
        private readonly IInputService _inputService;
        private readonly ITurnService _turnService;

        public IEntity Entity { get; set; }

        public PlayerMovementComponent(IInputService inputService, ITurnService turnService)
        {
            _inputService = inputService;
            _turnService = turnService;
        }

        public void Begin()
        {
            _inputService.OnKeyPressed(Keys.D, () =>
            {
                DoCommand(new MoveCommand(1, 0));
            });
            
            _inputService.OnKeyPressed(Keys.A, () =>
            {
                DoCommand(new MoveCommand(-1, 0));
            });
            
            _inputService.OnKeyPressed(Keys.S, () =>
            {
                DoCommand(new MoveCommand(0, 1));
            });
            
            _inputService.OnKeyPressed(Keys.W, () =>
            {
                DoCommand(new MoveCommand(0, -1));
            });
            
            _inputService.OnKeyPressed(Keys.Space, () =>
            { 
                DoCommand(new WaitCommand());
            });
        }

        private void DoCommand(ICommand command)
        {
            var commandSetComponent = Entity.GetComponent<CommandSetComponent>();
            commandSetComponent.PushCommand(command);
            _turnService.PlayersTurn = false;
        }

        public void Update()
        {
        }
    }
}