using ManualDi.Main.TypeScopes;

namespace ManualDi.Main.TypeResolvers
{
    public class TransientTypeResolver : ITypeResolver
    {
        public bool IsResolverFor(ITypeBinding typeBinding)
        {
            return typeBinding.TypeScope is TransientTypeScope;
        }

        public ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding)
        {
            var instance = typeBinding.TypeFactory.Create(container);

            return ResolvedInstance.New(instance);
        }
    }
}
