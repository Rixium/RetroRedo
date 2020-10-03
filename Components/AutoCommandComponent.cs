using System.Collections.Generic;
using RetroRedo.Commands;
using RetroRedo.Entities;
using RetroRedo.Services;

namespace RetroRedo.Components
{
    public class AutoCommandComponent : IComponent
    {
        private readonly ITurnService _turnService;
        private CommandSetComponent _commandSetComponent;
        public Queue<ICommand> CommandQueue { get; set; } = new Queue<ICommand>();

        public IEntity Entity { get; set; }

        public AutoCommandComponent(ITurnService turnService)
        {
            _turnService = turnService;
        }

        public void Begin()
        {
            _commandSetComponent = Entity.GetComponent<CommandSetComponent>();
            _commandSetComponent.UndoAll();
            CommandQueue = _commandSetComponent.GetAsQueue();
        }

        public void ForceFinish()
        {
            while (CommandQueue.Count > 0)
            {
                CommandQueue.TryDequeue(out var nextCommand);
                nextCommand?.Do(Entity);
            }
        }

        public void Update()
        {
            if (_turnService.PlayersTurn) return;
            CommandQueue.TryDequeue(out var nextCommand);
            nextCommand?.Do(Entity);
        }
    }
}