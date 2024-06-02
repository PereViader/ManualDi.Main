using System;
using System.Collections.Generic;
using ManualDi.Main.Scopes;

namespace ManualDi.Main
{
    public delegate T CreateDelegate<out T>(IDiContainer diContainer);
    public delegate void InitializationDelegate<in T>(T instance, IDiContainer container);
    public delegate void InjectionDelegate<in T>(T instance, IDiContainer diContainer);

    public interface ITypeBinding
    {
        Type InterfaceType { get; }
        Type ConcreteType { get; }
        ITypeScope TypeScope { get; set; }
        bool IsLazy { get; set; }
        public Dictionary<object, object>? Metadata { get; set; }

        object Create(IDiContainer container);
        public bool NeedsInitialize { get; }
        void Initialize(object instance, IDiContainer container);
        void Inject(object instance, IDiContainer container);
    }
}
