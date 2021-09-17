namespace ManualDi.Main
{
    public interface IContainerBuilder
    {
        IContainerBuilder WithParentContainer(IDiContainer diContainer);
        IDiContainer Build();
    }
}
