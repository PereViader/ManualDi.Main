namespace ManualDi.Main.TypeFactories
{
    public interface ITypeFactory<T> : ITypeFactory
    {
        new T Create(IDiContainer container);
    }

    public interface ITypeFactory
    {
        object Create(IDiContainer container);
    }
}
