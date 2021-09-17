using System;

namespace ManualDi.Main.TypeFactories
{
    public class ConstraintContainerTypeFactory<T, Y> : ITypeFactory<Y>
        where T : Y
    {
        public Action<IResolutionConstraints> Constraints { get; }

        public ConstraintContainerTypeFactory(Action<IResolutionConstraints> constraints)
        {
            Constraints = constraints;
        }

        public Y Create(IDiContainer container)
        {
            return container.Resolve<T>(Constraints);
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
