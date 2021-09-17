namespace ManualDi.Main
{
    public static class DiContainerExtensions
    {
        public static ITypeBinding<T, T> Bind<T>(this IDiContainer diContainer)
        {
            return Bind<T, T>(diContainer);
        }

        public static ITypeBinding<TInterface, TConcrete> Bind<TInterface, TConcrete>(this IDiContainer diContainer)
        {
            return diContainer.Bind(DefaultTypeBindingFactory.Create<TInterface, TConcrete>());
        }
    }
}
