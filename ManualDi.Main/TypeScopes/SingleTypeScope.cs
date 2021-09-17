namespace ManualDi.Main.TypeScopes
{
    public class SingleTypeScope : ITypeScope
    {
        public static SingleTypeScope Instance { get; } = new SingleTypeScope();

        private SingleTypeScope()
        {
        }
    }
}
