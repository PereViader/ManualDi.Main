using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualDi.Main
{
    public static class TypeBindingFromExtensions
    {
        public static TypeBinding<List<TInterface>, List<TConcrete>> FromContainerAll<TInterface, TConcrete>(
            this TypeBinding<List<TInterface>, List<TConcrete>> typeBinding
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateInterfaceDelegate = c => c.ResolveAll<TConcrete>().Cast<TInterface>().ToList();
            return typeBinding;
        }

        public static TypeBinding<List<TInterface>, List<TConcrete>> FromContainerAll<TInterface, TConcrete>(
            this TypeBinding<List<TInterface>, List<TConcrete>> typeBinding,
            Action<IResolutionConstraints> constraints
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateInterfaceDelegate = c => c.ResolveAll<TConcrete>(constraints).Cast<TInterface>().ToList();
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> FromContainer<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateConcreteDelegate = c => c.Resolve<TConcrete>();
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> FromContainer<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding,
            Action<IResolutionConstraints> constraints
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateConcreteDelegate = c => c.Resolve<TConcrete>(constraints);
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> FromInstance<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding,
            TConcrete instance
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateConcreteDelegate = _ => instance;
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> FromMethod<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding,
            CreateDelegate<TConcrete> createDelegate
            )
            where TConcrete : TInterface
        {
            typeBinding.CreateConcreteDelegate = createDelegate;
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> FromMethodUnsafe<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding,
            CreateDelegate<TConcrete> createDelegate
            )
        {
            typeBinding.CreateConcreteDelegate = c =>
            {
                TConcrete concreteObj = createDelegate.Invoke(c);

                if (concreteObj is not TInterface)
                {
                    throw new InvalidOperationException("MethodUnsafe could not cast provided factory method");
                }

                return concreteObj;
            };
            
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> Lazy<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.IsLazy = true;
            return typeBinding;
        }

        public static TypeBinding<TInterface, TConcrete> NonLazy<TInterface, TConcrete>(
            this TypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.IsLazy = false;
            return typeBinding;
        }
    }
}
