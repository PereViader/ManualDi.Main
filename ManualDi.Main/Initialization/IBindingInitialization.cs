namespace ManualDi.Main.Initialization
{
    public interface IBindingInitialization
    {
        void Initialize(object instance, IDiContainer container);
    }
}
