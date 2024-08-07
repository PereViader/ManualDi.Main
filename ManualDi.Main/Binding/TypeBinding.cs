﻿using System;
using System.Collections.Generic;
using ManualDi.Main.Scopes;

namespace ManualDi.Main
{
    public sealed class TypeBinding<TInterface, TConcrete> : ITypeBinding
    {
        public Type InterfaceType => typeof(TInterface);
        public Type ConcreteType => typeof(TConcrete);
        public ITypeScope TypeScope { get; set; } = SingleTypeScope.Instance;
        public Dictionary<object, object>? Metadata { get; set; }
        public CreateDelegate<TConcrete>? CreateConcreteDelegate { get; set; }
        public CreateDelegate<TInterface>? CreateInterfaceDelegate { get; set; }
        public InjectionDelegate<TConcrete>? InjectionDelegates { get; set; }
        public InitializationDelegate<TConcrete>? InitializationDelegate { get; set; }
        public bool TryToDispose { get; set; } = true; 
        public bool IsLazy { get; set; } = true;

        public object Create(IDiContainer container)
        {
            if (CreateConcreteDelegate is not null)
            {
                return CreateConcreteDelegate.Invoke(container)!;
            }
            
            if (CreateInterfaceDelegate is not null)
            {
                return CreateInterfaceDelegate.Invoke(container)!;
            }

            throw new InvalidOperationException(
                $"Could not create object for TypeBinding with apparent type {typeof(TInterface).FullName} and concrete type {typeof(TConcrete).FullName}");
        }
        
        public void InjectObject(object instance, IDiContainer container)
        {
            InjectionDelegates?.Invoke((TConcrete)instance, container);
        }

        public bool NeedsInitialize => InitializationDelegate is not null;
        
        public void InitializeObject(object instance, IDiContainer container)
        {
            InitializationDelegate?.Invoke((TConcrete)instance, container);
        }
    }
}
