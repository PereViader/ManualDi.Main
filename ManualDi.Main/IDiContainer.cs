using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IDiContainer : IDisposable
    {
        ITypeBinding<T, Y> Bind<T, Y>(ITypeBinding<T, Y> typeBinding);

        T Resolve<T>();
        T Resolve<T>(IResolutionConstraints resolutionConstraints);

        List<T> ResolveAll<T>();
        List<T> ResolveAll<T>(IResolutionConstraints resolutionConstraints);


        void FinishBinding();

        void QueueDispose(Action disposeAction);
    }
}
