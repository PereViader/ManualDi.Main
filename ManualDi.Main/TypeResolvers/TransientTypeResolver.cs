using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeScopes;

namespace ManualDi.Main.TypeResolvers
{
    public class TransientTypeResolver : ITypeResolver
    {
        public bool IsResolverFor(ITypeBinding typeBinding)
        {
            return typeBinding.TypeScope is TransientTypeScope;
        }

        public object Resolve(IDiContainer container, ITypeBinding typeBinding, IBindingInjector bindingInjector, IBindingInitializer bindingInitializer)
        {
            var instance = typeBinding.Factory.Create(container);

            bindingInjector.Injest(typeBinding, instance);
            bindingInitializer.Injest(typeBinding, instance);

            return instance;
        }
    }
}
