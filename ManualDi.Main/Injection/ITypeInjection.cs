namespace ManualDi.Main.Injection
{
    public interface ITypeInjection
    {
        void Inject(object instance, IDiContainer container);
    }
}
