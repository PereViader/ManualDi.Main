namespace ManualDi.Main
{
    public static class DiContainerBindingExtensions
    {
        public static ITypeBinding<T, T> Bind<T>(this IDiContainerBindings diContainerBindings)
        {
            ITypeBinding<T, T> typeBinding = DefaultTypeBindingFactory.Create<T, T>();
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }

        public static ITypeBinding<T, Y> Bind<T, Y>(this IDiContainerBindings diContainerBindings)
        {
            ITypeBinding<T, Y> typeBinding = DefaultTypeBindingFactory.Create<T, Y>();
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }

        public static ITypeBinding<T, Y> Bind<T, Y>(this IDiContainerBindings diContainerBindings, ITypeBinding<T, Y> typeBinding)
        {
            diContainerBindings.AddBinding(typeBinding);
            return typeBinding;
        }
    }
}
