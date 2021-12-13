using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class DiContainerResolutionResolveAllGenericExtensions
    {
        public static List<T> ResolveAll<T>(this IDiContainer diContainer)
        {
            return diContainer.ResolveAll<T>(resolutionConstraints: null);
        }

        public static List<Y> ResolveAll<T, Y>(this IDiContainer diContainer)
        {
            return diContainer.ResolveAll<T, Y>(resolutionConstraints: null);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<T>(resolutionConstraints);
        }

        public static List<Y> ResolveAll<T, Y>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<T, Y>(resolutionConstraints);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, IResolutionConstraints resolutionConstraints)
        {
            return diContainer.ResolveAll<T>(typeof(T), resolutionConstraints);
        }

        public static List<Y> ResolveAll<T, Y>(this IDiContainer diContainer, IResolutionConstraints resolutionConstraints)
        {
            return diContainer.ResolveAll<Y>(typeof(T), resolutionConstraints);
        }
    }
}
