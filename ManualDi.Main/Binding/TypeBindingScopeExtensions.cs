using ManualDi.Main.Scopes;

namespace ManualDi.Main
{
    public static class TypeBindingScopeExtensions
    {
        public static TypeBinding<TInterface, TConcrete> Transient<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.TypeScope = TransientTypeScope.Instance;
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> Single<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.TypeScope = SingleTypeScope.Instance;
            return typeBinding;
        }
    }
}
