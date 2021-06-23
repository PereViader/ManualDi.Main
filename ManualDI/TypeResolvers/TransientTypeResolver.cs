﻿using ManualDI.TypeScopes;
using System.Collections.Generic;

namespace ManualDI.TypeResolvers
{
    public class TransientTypeResolver : ITypeResolver
    {
        public bool IsResolverFor(ITypeBinding typeBinding)
        {
            return typeBinding.TypeScope is TransientTypeScope;
        }

        public object Resolve(IDiContainer container, ITypeBinding typeBinding, List<IInjectionCommand> injectionCommands)
        {
            var instance = typeBinding.Factory.Create(container);

            if (typeBinding.TypeInjection != null)
            {
                injectionCommands.Add(new InjectionCommand(typeBinding.TypeInjection, instance));
            }

            return instance;
        }
    }
}
