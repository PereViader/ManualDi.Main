using System;

namespace ManualDi.Main.Disposing
{
    public interface IBindingDisposer
    {
        void QueueDispose(Action disposeAction);
        void DisposeAll();
    }
}
