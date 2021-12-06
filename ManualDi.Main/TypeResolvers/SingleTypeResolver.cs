using ManualDi.Main.TypeScopes;
using System.Collections.Generic;

namespace ManualDi.Main.TypeResolvers
{
    public class SingleTypeResolver : ITypeResolver
    {
        public Dictionary<object, object> Instances { get; } = new Dictionary<object, object>();

        public bool IsResolverFor(ITypeBinding typeBinding)
        {
            return typeBinding.TypeScope is SingleTypeScope;
        }

        public ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding)
        {
            if (Instances.TryGetValue(typeBinding, out var singleInstance))
            {
                return ResolvedInstance.Reused(singleInstance);
            }

            var instance = typeBinding.TypeFactory.Create(container);
            Instances[typeBinding] = instance;

            return ResolvedInstance.New(instance);
        }
    }
}
