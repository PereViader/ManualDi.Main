using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualDi.Main.TypeFactories
{
    public class ConstraintAllContainerTypeFactory<T, Y> : ITypeFactory<List<Y>>
        where T : Y
    {
        public Action<IResolutionConstraints> Constraints { get; }

        public ConstraintAllContainerTypeFactory(Action<IResolutionConstraints> constraints)
        {
            Constraints = constraints;
        }

        public List<Y> Create(IDiContainer container)
        {
            return container.ResolveAll<T>(Constraints).Cast<Y>().ToList();
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
