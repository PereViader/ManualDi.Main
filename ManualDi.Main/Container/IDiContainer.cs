using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IDiContainer : IDisposable
    {
        /// <summary>
        /// Non generic resolution of a binding for its registered instance
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <param name="type">The type of the binding to resolve</param>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <returns>The resolved instance for the binding</returns>
        bool TryResolveContainer(Type type, IResolutionConstraints? resolutionConstraints, out object resolution);

        /// <summary>
        /// Non generic resolution of all bindings for their registered instances
        /// </summary>
        /// <remarks>There are more versions of this Resolution using extension methods <see cref="DiContainerResolutionExtensions"/></remarks>
        /// <param name="type">The type of the binding to resolve</param>
        /// <param name="resolutionConstraints">Filter bindings to resolve according to these constraints. May be null</param>
        /// <param name="resolutions">The list of resolutions to be populated from the operations done inside. Resolutions are added at the end of the list</param>
        /// <returns>The resolved instances for the binding</returns>
        void ResolveAllContainer<TResolutionList>(Type type, IResolutionConstraints? resolutionConstraints, List<TResolutionList> resolutions);

        /// <summary>
        /// Queues for disposal a disposable. They will be called in order when disposing the container.
        /// </summary>
        /// <param name="disposable">The disposable to queue</param>
        void QueueDispose(IDisposable disposable);
        
        /// <summary>
        /// Queues for disposal an action. They will be called in order when disposing the container.
        /// </summary>
        /// <param name="disposableAction">The disposable action to queue</param>
        void QueueDispose(Action disposableAction);
    }
}
