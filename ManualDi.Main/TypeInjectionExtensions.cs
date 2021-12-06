using ManualDi.Main.Injection;

namespace ManualDi.Main
{
    public delegate void InjectionDelegate<T>(T instance, IDiContainer diContainer);

    public static class TypeInjectionExtensions
    {
        public static ITypeBinding<T, Y> Inject<T, Y>(this ITypeBinding<T, Y> typeBinding, InjectionDelegate<Y> injectionDelegate)
        {
            typeBinding.TypeInjection += new MethodTypeInjection<Y>(injectionDelegate).Inject;
            return typeBinding;
        }
    }
}
