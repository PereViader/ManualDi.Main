using System;
using System.Collections.Generic;

namespace ManualDi.Main.Initialization
{
    public class BindingInitializer : IBindingInitializer
    {
        private readonly Stack<List<Action<IDiContainer>>> bindingInitializationCommands = new Stack<List<Action<IDiContainer>>>();
        private int nestedCount;

        public void Injest(ITypeBinding typeBinding, object instance)
        {
            if (nestedCount >= bindingInitializationCommands.Count)
            {
                bindingInitializationCommands.Push(new List<Action<IDiContainer>>());
            }

            var bindingInitialization = typeBinding.TypeInitialization;
            if (bindingInitialization == null)
            {
                return;
            }

            var commands = bindingInitializationCommands.Peek();
            commands.Add((IDiContainer container) => bindingInitialization.Invoke(instance, container));
        }

        public void InitializeAllQueued(IDiContainer container)
        {
            nestedCount++;

            var commands = bindingInitializationCommands.Pop();
            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Invoke(container);
            }

            nestedCount--;
        }
    }
}
