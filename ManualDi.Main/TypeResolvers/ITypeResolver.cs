namespace ManualDi.Main.TypeResolvers
{
    public interface ITypeResolver
    {
        bool IsResolverFor(ITypeBinding typeBinding);
        ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding);
    }
}
