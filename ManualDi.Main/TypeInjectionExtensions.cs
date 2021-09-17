using ManualDi.Main.Injection;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public delegate void InjectionDelegate<T>(T instance, IDiContainer diContainer);

    public static class TypeInjectionExtensions
    {
        public static ITypeBinding<T, Y> Inject<T, Y>(this ITypeBinding<T, Y> typeBinding, InjectionDelegate<Y> injectionDelegate)
        {
            if (typeBinding.TypeInjections == null)
            {
                typeBinding.TypeInjections = new List<ITypeInjection>();
            }

            typeBinding.TypeInjections.Add(new MethodTypeInjection<Y>(injectionDelegate));
            return typeBinding;
        }
    }
}
