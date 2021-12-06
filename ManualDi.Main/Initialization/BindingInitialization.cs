namespace ManualDi.Main.Initialization
{
    public delegate void InitializationDelegate<T>(T instance, IDiContainer container);
    public delegate void UntypedInitializationDelegate(object instance, IDiContainer container);

    public class BindingInitialization<T> : IBindingInitialization
    {
        public InitializationDelegate<T> InitializationDelegate { get; }

        public BindingInitialization(InitializationDelegate<T> initializationDelegate)
        {
            InitializationDelegate = initializationDelegate;
        }

        public void Initialize(object instance, IDiContainer container)
        {
            T typedInstance = (T)instance;
            InitializationDelegate.Invoke(typedInstance, container);
        }
    }
}
