using System.Collections.Generic;

namespace ManualDi.Main.Scopes
{
    public class SingleTypeResolver : ITypeResolver
    {
        public Dictionary<object, object> Instances { get; } = new();
        
        public ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding)
        {
            if (Instances.TryGetValue(typeBinding, out var singleInstance))
            {
                return ResolvedInstance.Reused(singleInstance);
            }

            var instance = typeBinding.Create(container);
            Instances[typeBinding] = instance;

            return ResolvedInstance.New(instance);
        }
    }
}
