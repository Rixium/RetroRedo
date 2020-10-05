using System.Collections.Generic;
using System.Linq;
using RetroRedo.Commands;
using RetroRedo.Entities;

namespace RetroRedo.Components
{
    public class CommandSetComponent : IComponent
    {
        public IEntity Entity { get; set; }

        private readonly Stack<ICommand> _commandStack = new Stack<ICommand>();

        public void Begin()
        {
        }

        public void Update()
        {
        }

        public void PushCommand(ICommand command)
        {
            _commandStack.Push(command);

            command.Do(Entity);
        }

        public void UndoAll()
        {
            var commandStackList = _commandStack.ToList();
            foreach (var command in commandStackList)
            {
                command.Undo(Entity);
            }
        }

        public Queue<ICommand> GetAsQueue()
        {
            var queue = new Queue<ICommand>();
            var reversed = _commandStack.Reverse();

            foreach (var command in reversed)
            {
                queue.Enqueue(command);
            }

            return queue;
        }
        
    }
}