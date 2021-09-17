using ManualDi.Main.Disposing;
using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeResolvers;
using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public class DiContainer : IDiContainer
    {
        public Dictionary<Type, List<ITypeBinding>> TypeBindings { get; } = new Dictionary<Type, List<ITypeBinding>>();
        public List<ITypeResolver> TypeResolvers { get; } = new List<ITypeResolver>();
        public IDiContainer ParentDiContainer { get; set; }
        public IBindingInjector BindingInjector { get; set; }
        public IBindingInitializer BindingInitializer { get; set; }
        public IBindingDisposer BindingDisposer { get; set; }

        private bool nextResolveIsRootResolve = true;
        private bool disposedValue;

        public ITypeBinding<TInterface, TConcrete> Bind<TInterface, TConcrete>(ITypeBinding<TInterface, TConcrete> typeBinding)
        {
            Type type = typeof(TInterface);
            if (!TypeBindings.TryGetValue(type, out var bindings))
            {
                bindings = new List<ITypeBinding>();
                TypeBindings[type] = bindings;
            }

            bindings.Add(typeBinding);

            return typeBinding;
        }


        public T Resolve<T>()
        {
            return Resolve<T>(resolutionConstraints: null);
        }

        public T Resolve<T>(IResolutionConstraints resolutionConstraints)
        {
            var typeBinding = GetTypeForConstraint<T>(resolutionConstraints);
            if (!typeBinding.IsError)
            {
                return ResolveTyped(typeBinding.Value);
            }

            if (ParentDiContainer != null)
            {
                return ParentDiContainer.Resolve<T>();
            }

            throw new InvalidOperationException($"There was no binding with requested constraints for {typeof(T).FullName}");
        }

        private T ResolveTyped<T>(ITypeBinding<T> typeBinding)
        {
            return (T)ResolveUntyped(typeBinding);
        }

        private object ResolveUntyped(ITypeBinding typeBinding)
        {
            var typeResolver = GetResolverFor(typeBinding);

            bool isRootResolve = this.nextResolveIsRootResolve;
            this.nextResolveIsRootResolve = false;

            var instance = typeResolver.Resolve(this, typeBinding, BindingInjector, BindingInitializer);

            if (isRootResolve)
            {
                BindingInjector.InjectAllQueued(this);
                BindingInitializer.InitializeAllQueued(this);
                this.nextResolveIsRootResolve = true;
            }

            return instance;
        }

        private Result<ITypeBinding<T>> GetTypeForConstraint<T>(IResolutionConstraints resolutionConstraints)
        {
            if (!TypeBindings.TryGetValue(typeof(T), out var bindings) || bindings.Count == 0)
            {
                return new Result<ITypeBinding<T>>(new InvalidOperationException($"There are no bindings for type {typeof(T).FullName}"));
            }

            if (resolutionConstraints == null)
            {
                return new Result<ITypeBinding<T>>((ITypeBinding<T>)bindings[0]);
            }

            foreach (var binding in bindings)
            {
                var typeBinding = (ITypeBinding<T>)binding;
                if (resolutionConstraints.Accepts(typeBinding))
                {
                    return new Result<ITypeBinding<T>>(typeBinding);
                }
            }

            return new Result<ITypeBinding<T>>(new InvalidOperationException("No binding could satisfy constraint"));
        }

        private Result<List<ITypeBinding<T>>> GetAllTypeForConstraint<T>(IResolutionConstraints resolutionConstraints)
        {
            if (!TypeBindings.TryGetValue(typeof(T), out var bindings) || bindings.Count == 0)
            {
                return new Result<List<ITypeBinding<T>>>(new InvalidOperationException($"There are no bindings for type {typeof(T).FullName}"));
            }

            var typeBindings = new List<ITypeBinding<T>>();

            if (resolutionConstraints == null)
            {
                foreach (var typeBinding in bindings)
                {
                    typeBindings.Add((ITypeBinding<T>)typeBinding);
                }
                return new Result<List<ITypeBinding<T>>>(typeBindings);
            }

            foreach (var binding in bindings)
            {
                var typeBinding = (ITypeBinding<T>)binding;
                if (resolutionConstraints.Accepts(typeBinding))
                {
                    typeBindings.Add(typeBinding);
                }
            }

            if (typeBindings.Count == 0)
            {
                new Result<List<ITypeBinding<T>>>(new InvalidOperationException("No binding could satisfy constraint"));
            }

            return new Result<List<ITypeBinding<T>>>(typeBindings);
        }



        private ITypeResolver GetResolverFor(ITypeBinding typeBinding)
        {
            foreach (var resolver in TypeResolvers)
            {
                if (resolver.IsResolverFor(typeBinding))
                {
                    return resolver;
                }
            }

            throw new InvalidOperationException($"Could not find resolver for type binding of type {typeBinding.GetType().FullName}");
        }

        public List<T> ResolveAll<T>()
        {
            return ResolveAll<T>(resolutionConstraints: null);
        }

        public List<T> ResolveAll<T>(IResolutionConstraints resolutionConstraints)
        {
            var typeBindings = GetAllTypeForConstraint<T>(resolutionConstraints).GetValueOrThrowIfError();
            var resolutions = ResolveAll(typeBindings);

            if (ParentDiContainer != null)
            {
                var parentResolutions = ParentDiContainer.ResolveAll<T>(resolutionConstraints);
                resolutions.AddRange(parentResolutions);
            }

            return resolutions;
        }

        private List<T> ResolveAll<T>(List<ITypeBinding<T>> typeBindings)
        {
            var resolved = new List<T>();
            foreach (var typeBinding in typeBindings)
            {
                resolved.Add(ResolveTyped(typeBinding));
            }
            return resolved;
        }

        public void FinishBinding()
        {
            foreach (var bindings in TypeBindings)
            {
                foreach (var binding in bindings.Value)
                {
                    if (!binding.IsLazy)
                    {
                        ResolveUntyped(binding);
                    }
                }
            }
        }

        public void QueueDispose(Action disposeAction)
        {
            BindingDisposer.QueueDispose(disposeAction);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    BindingDisposer.DisposeAll();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
