namespace ManualDi.Main.Injection
{
    public class MethodTypeInjection<T> : ITypeInjection
    {
        public InjectionDelegate<T> InjectionDelegate { get; }

        public MethodTypeInjection(InjectionDelegate<T> injectionDelegate)
        {
            InjectionDelegate = injectionDelegate;
        }

        public void Inject(object instance, IDiContainer container)
        {
            T typedInstance = (T)instance;
            InjectionDelegate.Invoke(typedInstance, container);
        }
    }
}
