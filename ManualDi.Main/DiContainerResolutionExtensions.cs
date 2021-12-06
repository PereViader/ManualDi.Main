using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class DiContainerResolutionExtensions
    {
        public static object Resolve(this IDiContainer diContainer, Type type)
        {
            return diContainer.Resolve(type, resolutionConstraints: null);
        }

        public static T Resolve<T>(this IDiContainer diContainer)
        {
            return diContainer.Resolve<T>(resolutionConstraints: null);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer)
        {
            var resolutions = new List<T>();
            diContainer.ResolveAll<T>(resolutionConstraints: null, resolutions);
            return resolutions;
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, IResolutionConstraints resolutionConstraints)
        {
            var resolutions = new List<T>();
            diContainer.ResolveAll<T>(resolutionConstraints, resolutions);
            return resolutions;
        }

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type)
        {
            var resolutions = new List<object>();
            diContainer.ResolveAll(type, resolutionConstraints: null, resolutions);
            return resolutions;
        }

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints)
        {
            var resolutions = new List<object>();
            diContainer.ResolveAll(type, resolutionConstraints, resolutions);
            return resolutions;
        }

        public static T Resolve<T>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);

            return diContainer.Resolve<T>(resolutionConstraints);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<T>(resolutionConstraints);
        }

        public static object Resolve(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);

            return diContainer.Resolve(type, resolutionConstraints);
        }

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll(type, resolutionConstraints);
        }
    }
}
