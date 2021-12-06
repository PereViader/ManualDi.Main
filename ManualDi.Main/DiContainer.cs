using ManualDi.Main.Disposing;
using ManualDi.Main.Initialization;
using ManualDi.Main.TypeResolvers;
using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public sealed class DiContainer : IDiContainer
    {
        public Dictionary<Type, List<ITypeBinding>> TypeBindings { get; set; }
        public List<ITypeResolver> TypeResolvers { get; set; }
        public IDiContainer ParentDiContainer { get; set; }
        public IBindingInitializer BindingInitializer { get; set; }
        public IBindingDisposer BindingDisposer { get; set; }

        private bool isResolving = false;

        private bool hasBeenInitialized = false;
        private bool disposedValue;

        public void Init()
        {
            if (hasBeenInitialized)
            {
                throw new InvalidOperationException("Container has already finished binding. Make sure you bind everything only when creating it");
            }

            hasBeenInitialized = true;

            foreach (var bindings in TypeBindings)
            {
                foreach (var binding in bindings.Value)
                {
                    if (!binding.IsLazy)
                    {
                        ResolveBinding(binding);
                    }
                }
            }
        }

        public T Resolve<T>(IResolutionConstraints resolutionConstraints)
        {
            return (T)Resolve(typeof(T), resolutionConstraints);
        }

        public object Resolve(Type type, IResolutionConstraints resolutionConstraints)
        {
            var typeBinding = GetTypeForConstraint(type, resolutionConstraints);
            if (!typeBinding.IsError)
            {
                return ResolveBinding(typeBinding.Value);
            }

            if (ParentDiContainer != null)
            {
                return ParentDiContainer.Resolve(type, resolutionConstraints);
            }

            throw new InvalidOperationException($"There was no binding with requested constraints for {type.FullName}");
        }

        private object ResolveBinding(ITypeBinding typeBinding)
        {
            var typeResolver = GetResolverFor(typeBinding);

            bool wasResolving = this.isResolving;
            isResolving = true;

            var resolvedInstance = typeResolver.Resolve(this, typeBinding);
            var instance = resolvedInstance.Instance;

            if (!resolvedInstance.IsNew)
            {
                isResolving = wasResolving;
                return instance;
            }

            typeBinding.TypeInjection?.Invoke(instance, this);
            BindingInitializer.Injest(typeBinding, instance);

            isResolving = wasResolving;

            if (!isResolving)
            {
                BindingInitializer.InitializeAllQueued(this);
            }

            return instance;
        }

        private Result<ITypeBinding> GetTypeForConstraint(Type type, IResolutionConstraints resolutionConstraints)
        {
            if (!TypeBindings.TryGetValue(type, out var bindings) || bindings.Count == 0)
            {
                return new Result<ITypeBinding>(new InvalidOperationException($"There are no bindings for type {type.FullName}"));
            }

            if (resolutionConstraints == null)
            {
                return new Result<ITypeBinding>(bindings[0]);
            }

            foreach (var binding in bindings)
            {
                if (resolutionConstraints.Accepts(binding))
                {
                    return new Result<ITypeBinding>(binding);
                }
            }

            return new Result<ITypeBinding>(new InvalidOperationException("No binding could satisfy constraint"));
        }

        private Result<List<ITypeBinding>> GetAllTypeForConstraint(Type type, IResolutionConstraints resolutionConstraints)
        {
            if (!TypeBindings.TryGetValue(type, out var bindings) || bindings.Count == 0)
            {
                return new Result<List<ITypeBinding>>(new InvalidOperationException($"There are no bindings for type {type.FullName}"));
            }

            var typeBindings = new List<ITypeBinding>();

            if (resolutionConstraints == null)
            {
                foreach (var typeBinding in bindings)
                {
                    typeBindings.Add(typeBinding);
                }
                return new Result<List<ITypeBinding>>(typeBindings);
            }

            foreach (var binding in bindings)
            {
                if (resolutionConstraints.Accepts(binding))
                {
                    typeBindings.Add(binding);
                }
            }

            if (typeBindings.Count == 0)
            {
                return new Result<List<ITypeBinding>>(new InvalidOperationException("No binding could satisfy constraint"));
            }

            return new Result<List<ITypeBinding>>(typeBindings);
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

        public void ResolveAll<T>(IResolutionConstraints resolutionConstraints, List<T> resolutions)
        {
            DoResolveAll(typeof(T), resolutionConstraints, resolutions);
        }

        public void ResolveAll(Type type, IResolutionConstraints resolutionConstraints, List<object> resolutions)
        {
            DoResolveAll(type, resolutionConstraints, resolutions);
        }

        private void DoResolveAll<T>(Type type, IResolutionConstraints resolutionConstraints, List<T> resolutions)
        {
            var typeBindings = GetAllTypeForConstraint(type, resolutionConstraints).GetValueOrThrowIfError();

            foreach (var typeBinding in typeBindings)
            {
                var resolved = ResolveBinding(typeBinding);
                resolutions.Add((T)resolved);
            }

            if (ParentDiContainer != null)
            {
                ParentDiContainer.ResolveAll(resolutionConstraints, resolutions);
            }
        }

        public void QueueDispose(Action disposeAction)
        {
            BindingDisposer.QueueDispose(disposeAction);
        }

        public void Dispose()
        {
            if (disposedValue)
            {
                return;
            }
            disposedValue = true;

            BindingDisposer.DisposeAll();
        }
    }
}
