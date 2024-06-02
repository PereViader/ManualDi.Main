namespace ManualDi.Main.Scopes
{
    public class SingleTypeScope : ITypeScope
    {
        public static SingleTypeScope Instance { get; } = new();

        private SingleTypeScope()
        {
        }
    }
}
