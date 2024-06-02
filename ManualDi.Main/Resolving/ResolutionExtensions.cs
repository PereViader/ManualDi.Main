using System;

namespace ManualDi.Main
{
    public static class ResolutionExtensions
    {
        public static IResolutionConstraints Where(this IResolutionConstraints resolution, Func<ITypeBinding, bool> typeBindingFunc)
        {
            var previous = resolution.ResolutionConstraintDelegate;
            if (previous is null)
            {
                resolution.ResolutionConstraintDelegate = typeBindingFunc;
            }
            else
            {
                resolution.ResolutionConstraintDelegate = x => previous.Invoke(x) && typeBindingFunc.Invoke(x);
            }

            return resolution;
        }
        
        public static IResolutionConstraints WhereMetadata(this IResolutionConstraints resolution, object flag)
        {
            var previous = resolution.ResolutionConstraintDelegate;
            if (previous is null)
            {
                resolution.ResolutionConstraintDelegate = x => x.Metadata?.ContainsKey(flag) == true;
            }
            else
            {
                resolution.ResolutionConstraintDelegate = x => previous.Invoke(x) && x.Metadata?.ContainsKey(flag) == true;
            }
            return resolution;
        }

        public static IResolutionConstraints WhereMetadata<T>(this IResolutionConstraints resolution, object key, T value)
        {
            var previous = resolution.ResolutionConstraintDelegate;
            if (previous is null)
            {
                resolution.ResolutionConstraintDelegate = x => Check(key, value, x);
            }
            else
            {
                resolution.ResolutionConstraintDelegate = x => previous.Invoke(x) && Check(key, value, x);
            }
            return resolution;


            static bool Check<Y>(object key, Y value, ITypeBinding x)
            {
                return x.Metadata?.TryGetValue(key, out var objValue) == true && value!.Equals(objValue!);
            }
        }
        
        public static IResolutionConstraints WhereMetadata<T>(this IResolutionConstraints resolution, object key, Func<T, bool> predicate)
        {
            var previous = resolution.ResolutionConstraintDelegate;
            if (previous is null)
            {
                resolution.ResolutionConstraintDelegate = x => Check(key, predicate, x);
            }
            else
            {
                resolution.ResolutionConstraintDelegate = x => previous.Invoke(x) && Check(key, predicate, x);
            }
            return resolution;


            static bool Check(object key, Func<T, bool> predicate, ITypeBinding x)
            {
                return x.Metadata?.TryGetValue(key, out var objValue) == true && objValue is T value && predicate.Invoke(value);
            }
        }
    }
}
