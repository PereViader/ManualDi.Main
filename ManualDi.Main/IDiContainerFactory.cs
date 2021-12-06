namespace ManualDi.Main
{
    public interface IDiContainerFactory
    {
        IDiContainer Create(IDiContainerBindings diContainerBinder, IDiContainer parentDiContainer);
    }
}
