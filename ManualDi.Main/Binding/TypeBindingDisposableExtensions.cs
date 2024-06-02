using System;

namespace ManualDi.Main
{
    public delegate Action GetDisposeDelegate<in T>(T instance, IDiContainer container);

    public static class TypeBindingDisposableExtensions
    {
        public static TypeBinding<TInterface, TConcrete> Dispose<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
            where TConcrete : IDisposable
        {
            return Dispose(typeBinding, GetDispose);
        }

        private static Action GetDispose<T>(T instance, IDiContainer container) where T : IDisposable
        {
            return instance.Dispose;
        }

        public static TypeBinding<TInterface, TConcrete> Dispose<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding,
            GetDisposeDelegate<TConcrete> getDisposeDelegate
            )
        {
            typeBinding.Inject((o, c) => c.QueueDispose(getDisposeDelegate.Invoke(o, c)));
            return typeBinding;
        }
    }
}
