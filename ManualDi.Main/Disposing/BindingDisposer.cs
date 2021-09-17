using System;
using System.Collections.Generic;

namespace ManualDi.Main.Disposing
{
    public class BindingDisposer : IBindingDisposer
    {
        private readonly List<Action> disposeActions = new List<Action>();

        private bool disposing = false;

        public void QueueDispose(Action disposeAction)
        {
            if (disposing)
            {
                throw new InvalidOperationException(
                    "Tried to register a dispose action while disposing"
                    );
            }

            disposeActions.Add(disposeAction);
        }

        public void DisposeAll()
        {
            disposing = true;

            foreach (Action action in disposeActions)
            {
                action.Invoke();
            }

            disposing = false;

            disposeActions.Clear();
        }
    }
}
