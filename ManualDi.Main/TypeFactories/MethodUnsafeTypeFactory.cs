using System;

namespace ManualDi.Main.TypeFactories
{
    public class MethodUnsafeTypeFactory<TInterface, TConcrete> : ITypeFactory<TInterface>
    {
        public FactoryMethodDelegate<TConcrete> FactoryMethodDelegate { get; }

        public MethodUnsafeTypeFactory(FactoryMethodDelegate<TConcrete> factoryMehtodDelegate)
        {
            FactoryMethodDelegate = factoryMehtodDelegate;
        }

        public TInterface Create(IDiContainer container)
        {
            TConcrete concreteObj = FactoryMethodDelegate.Invoke(container);

            if (!(concreteObj is TInterface interfaceObj))
            {
                throw new InvalidOperationException("MethodUnsafe could not cast provided factory method");
            }

            return interfaceObj;
        }

        object ITypeFactory.Create(IDiContainer container)
        {
            return Create(container);
        }
    }
}
