namespace ManualDi.Main.Initialization
{
    public interface IBindingInitializer
    {
        void Injest(ITypeBinding typeBinding, object instance);
        void InitializeAllQueued(IDiContainer container);
    }
}
