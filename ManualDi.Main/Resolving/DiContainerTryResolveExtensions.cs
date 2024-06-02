﻿using System;

namespace ManualDi.Main
{
    public static class DiContainerTryResolveExtensions
    {
        public static bool TryResolve<T>(this IDiContainer diContainer, out T resolution)
        {
            return diContainer.TryResolve<T>(resolutionConstraints: null, out resolution);
        }

        public static bool TryResolve<T>(this IDiContainer diContainer, Action<IResolutionConstraints> configureReslutionConstraints, out T resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            configureReslutionConstraints.Invoke(resolutionConstraints);

            return diContainer.TryResolve<T>(resolutionConstraints, out resolution);
        }

        public static bool TryResolve<T>(this IDiContainer diContainer, IResolutionConstraints resolutionConstraints, out T resolution)
        {
            if (!diContainer.TryResolve(typeof(T), resolutionConstraints, out object result))
            {
                resolution = default;
                return false;
            }

            resolution = (T)result;
            return true;
        }
        
        public static bool TryResolve(this IDiContainer diContainer, Type type, out object resolution)
        {
            return diContainer.TryResolveContainer(type, resolutionConstraints: null, out resolution);
        }

        public static bool TryResolve(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> configureResolutionConstraints, out object resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            configureResolutionConstraints.Invoke(resolutionConstraints);

            return diContainer.TryResolveContainer(type, resolutionConstraints, out resolution);
        }

        public static bool TryResolve(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints, out object resolution)
        {
            return diContainer.TryResolveContainer(type, resolutionConstraints, out resolution);
        }
    }
}
