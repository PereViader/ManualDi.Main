using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeFactories;
using ManualDi.Main.TypeScopes;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public class TypeBinding<TInterface, TConcrete> : ITypeBinding<TInterface, TConcrete>
    {
        public ITypeMetadata TypeMetadata { get; set; }
        public ITypeScope TypeScope { get; set; }
        public ITypeFactory Factory { get; set; }
        public List<ITypeInjection> TypeInjections { get; set; }
        public IBindingInitialization BindingInitialization { get; set; }
        public bool IsLazy { get; set; }
    }
}
