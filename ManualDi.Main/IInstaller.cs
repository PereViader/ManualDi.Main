namespace ManualDi.Main
{
    public delegate void InstallDelegate(IDiContainerBindings bindings);

    public interface IInstaller
    {
        void Install(IDiContainerBindings bindings);
    }
}
