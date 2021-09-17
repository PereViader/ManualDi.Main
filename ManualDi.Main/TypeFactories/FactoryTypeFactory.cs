namespace ManualDi.Main.TypeFactories
{
    public class FactoryTypeFactory<TFactory, TInterface, TConcrete> : ITypeFactory<TInterface>
        where TFactory : IFactory<TConcrete>
        where TConcrete : TInterface
    {
        public TInterface Create(IDiContainer container)
        {
            IFactory<TConcrete> factory = container.Resolve<TFactory>();
            TConcrete value = factory.Create();
            return value;
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
