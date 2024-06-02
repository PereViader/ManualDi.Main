using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    internal sealed class BindingInitializer
    {
        private readonly Stack<List<Action<IDiContainer>>> bindingInitializationCommands = new();
        private int nestedCount;

        public void Queue(ITypeBinding typeBinding, object instance)
        {
            if (nestedCount >= bindingInitializationCommands.Count)
            {
                bindingInitializationCommands.Push(new List<Action<IDiContainer>>());
            }

            if (!typeBinding.NeedsInitialize)
            {
                return;
            }

            var commands = bindingInitializationCommands.Peek();
            commands.Add(c => typeBinding.Initialize(instance, c));
        }

        public void InitializeAllQueued(IDiContainer container)
        {
            nestedCount++;

            var commands = bindingInitializationCommands.Pop();
            foreach (var action in commands)
            {
                action.Invoke(container);
            }

            nestedCount--;
        }
    }
}
