using System;

namespace ManualDi.Main
{
    public static class ResolutionExtensions
    {
        public static IResolutionConstraints WhereMetadata(this IResolutionConstraints resolution, object flag)
        {
            var previous = resolution.TypeMetadata;
            if (previous == null)
            {
                resolution.TypeMetadata = x => x.Has(flag);
            }
            else
            {
                resolution.TypeMetadata = x => previous.Invoke(x) && x.Has(flag);
            }
            return resolution;
        }

        public static IResolutionConstraints WhereMetadata<T>(this IResolutionConstraints resolution, object key, T value)
        {
            var previous = resolution.TypeMetadata;
            if (previous == null)
            {
                resolution.TypeMetadata = x => Check(key, value, x);
            }
            else
            {
                resolution.TypeMetadata = x => previous.Invoke(x) && Check(key, value, x);
            }
            return resolution;


            static bool Check<Y>(object key, Y value, ITypeMetadata x)
            {
                return x.TryGet<Y>(key, out var objValue) && value.Equals(objValue);
            }
        }

        public static IResolutionConstraints WhereMetadata(this IResolutionConstraints resolution, Func<ITypeMetadata, bool> metadata)
        {
            var previous = resolution.TypeMetadata;

            if (previous == null)
            {
                resolution.TypeMetadata = metadata;
            }
            else
            {
                resolution.TypeMetadata = x => previous.Invoke(x) && metadata.Invoke(x);
            }

            return resolution;
        }
    }
}
