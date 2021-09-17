using ManualDi.Main.TypeScopes;

namespace ManualDi.Main
{
    public static class TypeScopeExtensions
    {
        public static ITypeBinding<TInterface, TConcrete> Transient<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.TypeScope = TransientTypeScope.Instance;
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> Single<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.TypeScope = SingleTypeScope.Instance;
            return typeBinding;
        }
    }
}
