using ManualDi.Main.Disposing;
using ManualDi.Main.Initialization;
using ManualDi.Main.TypeResolvers;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public class DiContainerFactory : IDiContainerFactory
    {
        public IDiContainer Create(IDiContainerBindings diContainerBindings, IDiContainer parentDiContainer)
        {
            var diContainer = new DiContainer()
            {
                TypeBindings = diContainerBindings.TypeBindings,
                ParentDiContainer = parentDiContainer,
                TypeResolvers = new List<ITypeResolver>()
                {
                    new SingleTypeResolver(),
                    new TransientTypeResolver()
                },

                BindingInitializer = new BindingInitializer(),
                BindingDisposer = new BindingDisposer(),
            };

            foreach (var action in diContainerBindings.DisposeActions)
            {
                diContainer.QueueDispose(action);
            }

            diContainer.Init();

            foreach (var initializationDelegate in diContainerBindings.InitializationDelegates)
            {
                initializationDelegate.Invoke(diContainer);
            }

            return diContainer;
        }
    }
}
