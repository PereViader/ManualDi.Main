using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeFactories;
using ManualDi.Main.TypeScopes;
using System;

namespace ManualDi.Main
{
    public delegate void RegisterDisposeDelegate(Action disposeAction);

    public interface ITypeBinding
    {
        ITypeMetadata TypeMetadata { get; set; }
        ITypeScope TypeScope { get; set; }
        ITypeFactory TypeFactory { get; set; }
        UntypedInjectionDelegate TypeInjection { get; set; }
        UntypedInitializationDelegate TypeInitialization { get; set; }
        bool IsLazy { get; set; }
    }

    public interface ITypeBinding<TInterface> : ITypeBinding
    {
    }

    public interface ITypeBinding<TInterface, TConcrete> : ITypeBinding<TInterface>
    {
    }
}
