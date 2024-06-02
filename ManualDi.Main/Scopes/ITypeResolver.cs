namespace ManualDi.Main.Scopes
{
    public interface ITypeResolver
    {
        ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding);
    }
}
