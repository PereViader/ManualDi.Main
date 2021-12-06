using ManualDi.Main.TypeFactories;
using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public delegate T FactoryMethodDelegate<T>(IDiContainer diContainer);

    public static class TypeFactoryExtensions
    {
        public static ITypeBinding<List<TInterface>, List<TConcrete>> FromContainerAll<TInterface, TConcrete>(
            this ITypeBinding<List<TInterface>, List<TConcrete>> typeBinding
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new ContainerAllTypeFactory<TConcrete, TInterface>();
            return typeBinding;
        }

        public static ITypeBinding<List<TInterface>, List<TConcrete>> FromContainerAll<TInterface, TConcrete>(
            this ITypeBinding<List<TInterface>, List<TConcrete>> typeBinding,
            Action<IResolutionConstraints> constraints
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new ConstraintAllContainerTypeFactory<TConcrete, TInterface>(constraints);
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromContainer<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new ContainerTypeFactory<TConcrete, TInterface>();
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromContainer<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            Action<IResolutionConstraints> constraints
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new ConstraintContainerTypeFactory<TConcrete, TInterface>(constraints);
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromInstance<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            TConcrete instance
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new InstanceTypeFactory<TInterface>(instance);
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromMethod<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            FactoryMethodDelegate<TConcrete> factoryMethodDelegate
            )
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new MethodTypeFactory<TInterface, TConcrete>(factoryMethodDelegate);
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromMethodUnsafe<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding,
            FactoryMethodDelegate<TConcrete> factoryMethodDelegate
            )
        {
            typeBinding.TypeFactory = new MethodUnsafeTypeFactory<TInterface, TConcrete>(factoryMethodDelegate);
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> FromFactory<TFactory, TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
            where TFactory : IFactory<TConcrete>
            where TConcrete : TInterface
        {
            typeBinding.TypeFactory = new FactoryTypeFactory<TFactory, TInterface, TConcrete>();
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> Lazy<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.IsLazy = true;
            return typeBinding;
        }

        public static ITypeBinding<TInterface, TConcrete> NonLazy<TInterface, TConcrete>(
            this ITypeBinding<TInterface, TConcrete> typeBinding
            )
        {
            typeBinding.IsLazy = false;
            return typeBinding;
        }
    }
}
