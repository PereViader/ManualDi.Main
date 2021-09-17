using System;
using System.Collections.Generic;

namespace ManualDi.Main.Injection
{
    public class BindingInjector : IBindingInjector
    {
        private readonly List<Action<IDiContainer>> injectionCommands = new List<Action<IDiContainer>>();

        public void Injest(ITypeBinding typeBinding, object instance)
        {
            if (typeBinding.TypeInjections == null)
            {
                return;
            }

            injectionCommands.Add((IDiContainer diContainer) =>
            {
                foreach (var typeInjection in typeBinding.TypeInjections)
                {
                    typeInjection.Inject(instance, diContainer);
                }
            });
        }

        public void InjectAllQueued(IDiContainer diContainer)
        {
            while (injectionCommands.Count > 0)
            {
                var index = injectionCommands.Count - 1;
                var injectionCommand = injectionCommands[index];
                injectionCommand.Invoke(diContainer);
                injectionCommands.RemoveAt(index);
            }
        }
    }
}
