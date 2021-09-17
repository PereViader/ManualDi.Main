using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
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

        public object Resolve(IDiContainer container, ITypeBinding typeBinding, IBindingInjector bindingInjector, IBindingInitializer bindingInitializer)
        {
            if (Instances.TryGetValue(typeBinding, out var singleInstance))
            {
                return singleInstance;
            }

            var instance = typeBinding.Factory.Create(container);
            Instances[typeBinding] = instance;

            bindingInjector.Injest(typeBinding, instance);
            bindingInitializer.Injest(typeBinding, instance);

            return instance;
        }
    }
}
