using System;

namespace ManualDi.Main
{
    public static class DiContainerResolutionTryResolveNonGenericExtensions
    {
        public static bool TryResolve(this IDiContainer diContainer, Type type, out object resolution)
        {
            return diContainer.TryResolve(type, resolutionConstraints: null, out resolution);
        }

        public static bool TryResolve(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> configureResolutionConstraints, out object resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            configureResolutionConstraints.Invoke(resolutionConstraints);

            return diContainer.TryResolve(type, resolutionConstraints, out resolution);
        }

        public static bool TryResolve(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints, out object resolution)
        {
            return diContainer.TryResolveContainer(type, resolutionConstraints, out resolution);
        }
    }
}
