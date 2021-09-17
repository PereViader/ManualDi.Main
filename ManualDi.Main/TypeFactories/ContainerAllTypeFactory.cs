using System.Collections.Generic;
using System.Linq;

namespace ManualDi.Main.TypeFactories
{
    public class ContainerAllTypeFactory<T, Y> : ITypeFactory<List<Y>>
        where T : Y
    {
        public List<Y> Create(IDiContainer container)
        {
            return container.ResolveAll<T>().Cast<Y>().ToList();
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
