using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeFactories;
using ManualDi.Main.TypeScopes;

namespace ManualDi.Main
{
    public class TypeBinding<TInterface, TConcrete> : ITypeBinding<TInterface, TConcrete>
    {
        public ITypeMetadata TypeMetadata { get; set; }
        public ITypeScope TypeScope { get; set; }
        public ITypeFactory TypeFactory { get; set; }
        public UntypedInjectionDelegate TypeInjection { get; set; }
        public UntypedInitializationDelegate TypeInitialization { get; set; }
        public bool IsLazy { get; set; }
    }
}
