namespace ManualDi.Main.Scopes
{
    public class TransientTypeScope : ITypeScope
    {
        public static TransientTypeScope Instance { get; } = new();

        private TransientTypeScope()
        {
        }
    }
}
