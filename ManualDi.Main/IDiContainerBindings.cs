using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public delegate void InitializationDelegate(IDiContainer diContainer);

    public interface IDiContainerBindings
    {
        Dictionary<Type, List<ITypeBinding>> TypeBindings { get; }
        IReadOnlyList<Action> DisposeActions { get; }
        IReadOnlyList<InitializationDelegate> InitializationDelegates { get; }

        void AddBinding<T>(ITypeBinding<T> typeBinding);
        void QueueDispose(Action action);
        void QueueInitialization(InitializationDelegate initializationDelegate);
    }
}
