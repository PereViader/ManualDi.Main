using System;
using System.Collections.Generic;

namespace ManualDi.Main.Initialization
{
    public class BindingInitializer : IBindingInitializer
    {
        private readonly List<Action<IDiContainer>> bindingInitializationCommands = new List<Action<IDiContainer>>();

        public void Injest(ITypeBinding typeBinding, object instance)
        {
            var bindingInitialization = typeBinding.BindingInitialization;
            if (bindingInitialization == null)
            {
                return;
            }

            bindingInitializationCommands.Add((IDiContainer container) => bindingInitialization.Initialize(instance, container));
        }

        public void InitializeAllQueued(IDiContainer container)
        {
            for (int i = bindingInitializationCommands.Count - 1; i >= 0; i--)
            {
                bindingInitializationCommands[i].Invoke(container);
            }
            bindingInitializationCommands.Clear();
        }
    }
}
