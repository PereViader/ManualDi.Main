using System;

namespace ManualDi.Main
{
    public class ResolutionConstraints : IResolutionConstraints
    {
        public Func<ITypeMetadata, bool> TypeMetadata { get; set; }

        public bool Accepts<T>(ITypeBinding<T> typeBinding)
        {
            if (TypeMetadata == null)
            {
                return true;
            }

            return TypeMetadata.Invoke(typeBinding.TypeMetadata);
        }
    }
}
