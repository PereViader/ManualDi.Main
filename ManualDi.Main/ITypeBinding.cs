using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeFactories;
using ManualDi.Main.TypeScopes;
using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public delegate void RegisterDisposeDelegate(Action disposeAction);

    public interface ITypeBinding
    {
        ITypeMetadata TypeMetadata { get; set; }
        ITypeScope TypeScope { get; set; }
        ITypeFactory Factory { get; set; }
        List<ITypeInjection> TypeInjections { get; set; }
        IBindingInitialization BindingInitialization { get; set; }
        bool IsLazy { get; set; }
    }

    public interface ITypeBinding<TInterface> : ITypeBinding
    {
    }

    public interface ITypeBinding<TInterface, TConcrete> : ITypeBinding<TInterface>
    {
    }
}
