using ManualDi.Main.Initialization;

namespace ManualDi.Main
{
    public static class TypeInitializationExtensions
    {
        public static ITypeBinding<TInterface, TConcrete> Initialize<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            InitializationDelegate<TConcrete> initializationDelegate
            )
        {
            typeBinding.TypeInitialization += new BindingInitialization<TConcrete>(initializationDelegate).Initialize;
            return typeBinding;
        }
    }
}
