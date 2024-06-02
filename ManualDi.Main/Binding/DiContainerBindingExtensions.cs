namespace ManualDi.Main
{
    public static class DiContainerBindingExtensions
    {
        public static TypeBinding<TInterface, TInterface> Bind<TInterface>(this DiContainerBindings diContainerBindings)
        {
            TypeBinding<TInterface, TInterface> typeBinding = new();
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> Bind<TInterface, TConcrete>(this DiContainerBindings diContainerBindings)
        {
            TypeBinding<TInterface, TConcrete> typeBinding = new();
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> Bind<TInterface, TConcrete>(this DiContainerBindings diContainerBindings, TypeBinding<TInterface, TConcrete> typeBinding)
        {
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }
    }
}
