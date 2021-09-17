namespace ManualDi.Main.Injection
{
    public class NopTypeInjection<T> : ITypeInjection
    {
        public static NopTypeInjection<T> Instance { get; } = new NopTypeInjection<T>();

        private NopTypeInjection()
        {
        }

        public void Inject(object instance, IDiContainer container)
        {
        }
    }
}
