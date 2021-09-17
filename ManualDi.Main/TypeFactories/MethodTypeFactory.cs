namespace ManualDi.Main.TypeFactories
{
    public class MethodTypeFactory<TInterface, TConcrete> : ITypeFactory<TInterface>
        where TConcrete : TInterface
    {
        public FactoryMethodDelegate<TConcrete> FactoryMethodDelegate { get; }

        public MethodTypeFactory(FactoryMethodDelegate<TConcrete> factoryMehtodDelegate)
        {
            FactoryMethodDelegate = factoryMehtodDelegate;
        }

        public TInterface Create(IDiContainer container)
        {
            return FactoryMethodDelegate.Invoke(container);
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
