using System;

namespace ManualDi.Main
{
    public interface IResolutionConstraints
    {
        Func<ITypeBinding, bool>? ResolutionConstraintDelegate { get; set; }

        bool Accepts(ITypeBinding typeBinding);
    }
}
