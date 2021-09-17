namespace ManualDi.Main
{
    public static class ConstraintExtensions
    {
        public static ITypeBinding<T, Y> WithMetadata<T, Y, TMetadata>(this ITypeBinding<T, Y> typeBinding, object key, TMetadata value)
            where Y : T
        {
            typeBinding.TypeMetadata.Set(key, value);
            return typeBinding;
        }

        public static ITypeBinding<T, Y> WithMetadata<T, Y>(this ITypeBinding<T, Y> typeBinding, object key)
            where Y : T
        {
            typeBinding.TypeMetadata.Set<object>(key, null);
            return typeBinding;
        }
    }
}
