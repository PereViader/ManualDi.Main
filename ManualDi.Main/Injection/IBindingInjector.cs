namespace ManualDi.Main.Injection
{
    public interface IBindingInjector
    {
        void InjectAllQueued(IDiContainer diContainer);
        void Injest(ITypeBinding typeBinding, object instance);
    }
}
