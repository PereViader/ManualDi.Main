using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class DiContainerResolutionResolveAllNonGenericExtensions
    {
        public static List<object> ResolveAll(this IDiContainer diContainer, Type type)
        {
            return diContainer.ResolveAll<object>(type, resolutionConstraints: null);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Type type)
        {
            return diContainer.ResolveAll<T>(type, resolutionConstraints: null);
        }

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> configureResolutionConstraints)
        {
            var resolutionConstraints = new ResolutionConstraints();
            configureResolutionConstraints.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<object>(type, resolutionConstraints);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> configureResolutionConstraints)
        {
            var resolutionConstraints = new ResolutionConstraints();
            configureResolutionConstraints.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<T>(type, resolutionConstraints);
        }

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints)
        {
            return diContainer.ResolveAll<object>(type, resolutionConstraints);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints)
        {
            var resolutions = new List<T>();
            diContainer.ResolveAllContainer<T>(type, resolutionConstraints, resolutions);
            return resolutions;
        }
    }
}
