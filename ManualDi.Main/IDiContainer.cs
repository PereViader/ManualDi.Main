using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IDiContainer : IDisposable
    {
        /// <summary>
        /// Generic resolution of a binding for its registered instance
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <typeparam name="T">The type of the binding to resolve</typeparam>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <returns>The resolved instance for the binding</returns>
        T Resolve<T>(IResolutionConstraints resolutionConstraints);

        /// <summary>
        /// Non generic resolution of a binding for its registered instance
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <param name="type">The type of the binding to resolve</param>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <returns>The resolved instance for the binding</returns>
        object Resolve(Type type, IResolutionConstraints resolutionConstraints);

        /// <summary>
        /// Generic resolution of all bindings for their registered instances
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <typeparam name="T">The type of the binding to resolve</typeparam>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <param name="resolutions">The list of resolutions to be populated from the operations done inside. Resolutions are added at the end of the list</param>
        /// <returns>The resolved instances for the binding</returns>
        void ResolveAll<T>(IResolutionConstraints resolutionConstraints, List<T> resolutions);

        /// <summary>
        /// Non generic resolution of all bindings for their registered instances
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <param name="type">The type of the binding to resolve</param>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <param name="resolutions">The list of resolutions to be populated from the operations done inside. Resolutions are added at the end of the list</param>
        /// <returns>The resolved instances for the binding</returns>
        void ResolveAll(Type type, IResolutionConstraints resolutionConstraints, List<object> resolutions);

        /// <summary>
        /// Queues for disposal an action. They will be called in order when disposing the container.
        /// </summary>
        /// <param name="disposeAction">The action to call when disposing</param>
        void QueueDispose(Action disposeAction);
    }
}
