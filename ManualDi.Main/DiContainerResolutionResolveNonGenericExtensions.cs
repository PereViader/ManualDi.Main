using System;

namespace ManualDi.Main
{
    public static class DiContainerResolutionResolveNonGenericExtensions
    {
        public static object Resolve(this IDiContainer diContainer, Type type)
        {
            return diContainer.Resolve(type, resolutionConstraints: null);
        }

        public static object Resolve(this IDiContainer diContainer, Type type, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);

            return diContainer.Resolve(type, resolutionConstraints);
        }

        public static object Resolve(this IDiContainer diContainer, Type type, IResolutionConstraints resolutionConstraints)
        {
            if (!diContainer.TryResolveContainer(type, resolutionConstraints, out object resolution))
            {
                throw new InvalidOperationException($"Could not resolve {type.FullName}");
            }

            return resolution;
        }
    }
}
