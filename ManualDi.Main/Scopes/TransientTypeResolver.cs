namespace ManualDi.Main.Scopes
{
    public class TransientTypeResolver : ITypeResolver
    {
        public ResolvedInstance Resolve(IDiContainer container, ITypeBinding typeBinding)
        {
            var instance = typeBinding.Create(container);

            return ResolvedInstance.New(instance);
        }
    }
}
