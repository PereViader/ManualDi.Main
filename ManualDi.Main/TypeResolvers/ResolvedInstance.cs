namespace ManualDi.Main.TypeResolvers
{
    public struct ResolvedInstance
    {
        public object Instance { get; }
        public bool IsNew { get; }

        private ResolvedInstance(object instance, bool isNew)
        {
            Instance = instance;
            IsNew = isNew;
        }

        public static ResolvedInstance New(object instance)
        {
            return new ResolvedInstance(instance, true);
        }

        public static ResolvedInstance Reused(object instance)
        {
            return new ResolvedInstance(instance, false);
        }
    }
}
