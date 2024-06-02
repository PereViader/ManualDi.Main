using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class DiContainerResolveAllExtensions
    {
        public static List<TConcrete> ResolveAll<TConcrete>(this IDiContainer diContainer)
        {
            return diContainer.ResolveAll<TConcrete>(resolutionConstraints: null);
        }

        public static List<TConcrete> ResolveAll<TInterface, TConcrete>(this IDiContainer diContainer)
        {
            return diContainer.ResolveAll<TInterface, TConcrete>(resolutionConstraints: null);
        }

        public static List<TInterface> ResolveAll<TInterface>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<TInterface>(resolutionConstraints);
        }

        public static List<TConcrete> ResolveAll<TInterface, TConcrete>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);
            return diContainer.ResolveAll<TInterface, TConcrete>(resolutionConstraints);
        }

        public static List<TConcrete> ResolveAll<TConcrete>(this IDiContainer diContainer, IResolutionConstraints? resolutionConstraints)
        {
            return diContainer.ResolveAll<TConcrete>(typeof(TConcrete), resolutionConstraints);
        }

        public static List<TConcrete> ResolveAll<TInterface, TConcrete>(this IDiContainer diContainer, IResolutionConstraints? resolutionConstraints)
        {
            return diContainer.ResolveAll<TConcrete>(typeof(TInterface), resolutionConstraints);
        }
        
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

        public static List<object> ResolveAll(this IDiContainer diContainer, Type type, IResolutionConstraints? resolutionConstraints)
        {
            return diContainer.ResolveAll<object>(type, resolutionConstraints);
        }

        public static List<T> ResolveAll<T>(this IDiContainer diContainer, Type type, IResolutionConstraints? resolutionConstraints)
        {
            var resolutions = new List<T>();
            diContainer.ResolveAllContainer<T>(type, resolutionConstraints, resolutions);
            return resolutions;
        }
    }
}
