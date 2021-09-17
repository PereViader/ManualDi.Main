using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;

namespace ManualDi.Main.TypeResolvers
{
    public interface ITypeResolver
    {
        bool IsResolverFor(ITypeBinding typeBinding);
        object Resolve(IDiContainer container, ITypeBinding typeBinding, IBindingInjector bindingInjector, IBindingInitializer bindingInitializer);
    }
}
