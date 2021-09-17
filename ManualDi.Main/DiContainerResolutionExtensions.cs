using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public static class DiContainerResolutionExtensions
    {
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
    }
}
