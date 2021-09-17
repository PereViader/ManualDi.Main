using System;

namespace ManualDi.Main
{
    public delegate Action GetDisposeDelegate<T>(T instance, IDiContainer container);

    public static class TypeDisposableExtensions
    {
        public static ITypeBinding<TInterface, TConcrete> RegisterDispose<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
            where TConcrete : IDisposable
        {
            return RegisterDispose(typeBinding, GetDispose);
        }

        private static Action GetDispose<T>(T instance, IDiContainer container) where T : IDisposable
        {
            return instance.Dispose;
        }

        public static ITypeBinding<TInterface, TConcrete> RegisterDispose<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            GetDisposeDelegate<TConcrete> getDisposeDelegate
            )
        {
            typeBinding.Inject((o, c) => c.QueueDispose(getDisposeDelegate.Invoke(o, c)));
            return typeBinding;
        }
    }
}
