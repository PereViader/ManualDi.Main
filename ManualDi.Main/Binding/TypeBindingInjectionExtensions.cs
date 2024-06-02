namespace ManualDi.Main
{
    public static class TypeBindingInjectionExtensions
    {
        public static TypeBinding<TInterface, TConcrete> Inject<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding, 
            InjectionDelegate<TConcrete> injectionDelegate)
        {
            typeBinding.InjectionDelegates += injectionDelegate;
            return typeBinding;
        }
    }
}
