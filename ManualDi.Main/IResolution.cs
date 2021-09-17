using System;

namespace ManualDi.Main
{
    public interface IResolutionConstraints
    {
        Func<ITypeMetadata, bool> TypeMetadata { get; set; }

        bool Accepts<T>(ITypeBinding<T> typeBinding);
    }
}
