using System.Collections.Generic;
using RetroRedo.Commands;
using RetroRedo.Entities;
using RetroRedo.Services;

namespace RetroRedo.Components
{
    public class AutoCommandComponent : IComponent
    {
        private CommandSetComponent _commandSetComponent;
        private Queue<ICommand> CommandQueue { get; set; } = new Queue<ICommand>();

        public IEntity Entity { get; set; }

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
            if (TurnService.PlayersTurn) return;

            CommandQueue.TryDequeue(out var nextCommand);
            nextCommand?.Do(Entity);
        }
    }
}