using System;

namespace ManualDi.Main
{
    public static class DiContainerResolutionResolveGenericExtensions
    {
        public static T Resolve<T>(this IDiContainer diContainer)
        {
            return diContainer.Resolve<T>(resolutionConstraints: null);
        }

        public static T Resolve<T>(this IDiContainer diContainer, Action<IResolutionConstraints> resolution)
        {
            var resolutionConstraints = new ResolutionConstraints();
            resolution.Invoke(resolutionConstraints);

            return diContainer.Resolve<T>(resolutionConstraints);
        }

        public static T Resolve<T>(this IDiContainer diContainer, IResolutionConstraints resolutionConstraints)
        {
            return (T)diContainer.Resolve(typeof(T), resolutionConstraints);
        }
    }
}
