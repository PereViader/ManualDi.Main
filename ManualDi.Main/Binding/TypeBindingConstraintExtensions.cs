using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class TypeBindingConstraintExtensions
    {
        public static TypeBinding<TInterface, TConcrete> WithMetadata<TInterface, TConcrete, TMetadata>(this TypeBinding<TInterface, TConcrete> typeBinding, object key, TMetadata value)
            where TConcrete : TInterface
        {
            typeBinding.Metadata ??= new Dictionary<object, object>();
            typeBinding.Metadata.Add(key, value!);
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> WithMetadata<TInterface, TConcrete>(this TypeBinding<TInterface, TConcrete> typeBinding, object key)
            where TConcrete : TInterface
        {
            typeBinding.Metadata ??= new Dictionary<object, object>();
            typeBinding.Metadata.Add(key, key);
            return typeBinding;
        }
    }
}
