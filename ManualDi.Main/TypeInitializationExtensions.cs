using ManualDi.Main.Initialization;
using System;

namespace ManualDi.Main
{
    public static class TypeInitializationExtensions
    {
        public static ITypeBinding<TInterface, TConcrete> Initialize<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            InitializationDelegate<TConcrete> initializationDelegate
            )
        {
            if (typeBinding.BindingInitialization != null)
            {
                throw new InvalidOperationException($"Initialize can only be called once on {nameof(ITypeBinding)} of type {typeof(TInterface).FullName}");
            }

            typeBinding.BindingInitialization = new BindingInitialization<TConcrete>(initializationDelegate);
            return typeBinding;
        }
    }
}
