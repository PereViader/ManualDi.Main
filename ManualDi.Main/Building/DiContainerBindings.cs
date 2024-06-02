using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public delegate void ContainerDelegate(IDiContainer diContainer);
    
    public sealed class DiContainerBindings
    {
        private readonly List<Action> disposeActions = new();
        private readonly List<ContainerDelegate> initializationDelegates = new();
        private readonly List<ContainerDelegate> injectDelegates = new();
        
        public Dictionary<Type, List<ITypeBinding>> TypeBindings { get; } = new();
        public IReadOnlyList<Action> DisposeActions => disposeActions;
        public IReadOnlyList<ContainerDelegate> InitializationDelegates => initializationDelegates;
        public IReadOnlyList<ContainerDelegate> InjectDelegates => injectDelegates;

        public void AddBinding(ITypeBinding typeBinding)
        {
            Type type = typeBinding.InterfaceType;
            if (!TypeBindings.TryGetValue(type, out var bindings))
            {
                bindings = new List<ITypeBinding>();
                TypeBindings[type] = bindings;
            }

            bindings.Add(typeBinding);
        }
        
        public void QueueInjection(ContainerDelegate containerDelegate)
        {
            injectDelegates.Add(containerDelegate);
        }

        public void QueueInitialization(ContainerDelegate containerDelegate)
        {
            initializationDelegates.Add(containerDelegate);
        }
        
        public void QueueDispose(Action action)
        {
            disposeActions.Add(action);
        }
    }
}
