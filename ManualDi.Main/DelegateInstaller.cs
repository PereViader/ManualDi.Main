using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public class DelegateInstaller : IInstaller
    {
        private readonly List<Action<IDiContainer>> containerDelegates = new List<Action<IDiContainer>>();

        public void Add(Action<IDiContainer> action)
        {
            containerDelegates.Add(action);
        }

        public void Add<T>(Action<ITypeBinding<T, T>> action)
        {
            Add<T, T>(action);
        }

        public void Add<T, Y>(Action<ITypeBinding<T, Y>> action)
        {
            void TypeBindingToContainerAdapter(IDiContainer container)
            {
                action.Invoke(container.Bind<T, Y>());
            }

            containerDelegates.Add(TypeBindingToContainerAdapter);
        }

        public void Install(IDiContainer container)
        {
            foreach (var containerDelegate in containerDelegates)
            {
                containerDelegate.Invoke(container);
            }
        }
    }
}
