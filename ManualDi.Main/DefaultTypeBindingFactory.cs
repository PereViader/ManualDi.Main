using ManualDi.Main.TypeScopes;

namespace ManualDi.Main
{
    public static class DefaultTypeBindingFactory
    {
        public static ITypeBinding<TInterface, TConcrete> Create<TInterface, TConcrete>()
        {
            return new TypeBinding<TInterface, TConcrete>()
            {
                TypeScope = SingleTypeScope.Instance,
                TypeMetadata = new TypeMetadata(),
                IsLazy = true
            };
        }
    }
}
