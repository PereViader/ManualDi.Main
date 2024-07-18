using System;

namespace ManualDi.Main
{
    internal class ActionDisposableWrapper : IDisposable
    {
        private Action? action;

        public ActionDisposableWrapper(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            if (action is null)
            {
                return;
            }
            
            action.Invoke();
            action = null;
        }
    }
}