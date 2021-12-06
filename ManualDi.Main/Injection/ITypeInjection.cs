namespace ManualDi.Main.Injection
{
    public delegate void UntypedInjectionDelegate(object instance, IDiContainer container);

    public interface ITypeInjection
    {
        void Inject(object instance, IDiContainer container);
    }
}
