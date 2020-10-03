using System.Collections.Generic;
using RetroRedo.Commands;
using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public class CommandSetComponent : IComponent
    {
        public IEntity Entity { get; set; }

        private readonly Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private readonly Stack<ICommand> _commandStack = new Stack<ICommand>();
        
        public void Begin()
        {
            
        }

        public void Update()
        {
        }

        public void PushCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
            _commandStack.Push(command);

            command.Do(Entity);
        }

        public void Undo()
        {
            _commandStack.TryPop(out var lastCommand);
            lastCommand?.Undo(Entity);
        }
    }
}