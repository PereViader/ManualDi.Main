using System;

namespace ManualDi.Main
{
    public sealed class ResolutionConstraints : IResolutionConstraints
    {
        public Func<ITypeBinding, bool>? ResolutionConstraintDelegate { get; set; }

        public bool Accepts(ITypeBinding typeBinding)
        {
            if (ResolutionConstraintDelegate is null)
            {
                return true;
            }

            return ResolutionConstraintDelegate.Invoke(typeBinding);
        }
    }
}
