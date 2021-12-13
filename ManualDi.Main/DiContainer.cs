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

        public bool TryResolveContainer(Type type, IResolutionConstraints resolutionConstraints, out object resolution)
        {
            if (TryGetTypeForConstraint(type, resolutionConstraints, out ITypeBinding typeBinding))
            {
                resolution = ResolveBinding(typeBinding);
                return true;
            }

            if (ParentDiContainer != null)
            {
                return ParentDiContainer.TryResolveContainer(type, resolutionConstraints, out resolution);
            }

            resolution = default;
            return false;
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

        private bool TryGetTypeForConstraint(Type type, IResolutionConstraints resolutionConstraints, out ITypeBinding typeBinding)
        {
            if (!TypeBindings.TryGetValue(type, out var bindings) || bindings.Count == 0)
            {
                typeBinding = default;
                return false;
            }

            if (resolutionConstraints == null)
            {
                typeBinding = bindings[0];
                return true;
            }

            foreach (var binding in bindings)
            {
                if (resolutionConstraints.Accepts(binding))
                {
                    typeBinding = binding;
                    return true;
                }
            }

            typeBinding = default;
            return false;
        }

        private bool TryGetAllTypeForConstraint(Type type, IResolutionConstraints resolutionConstraints, out List<ITypeBinding> typeBindings)
        {
            if (!TypeBindings.TryGetValue(type, out var bindings) || bindings.Count == 0)
            {
                typeBindings = default;
                return false;
            }

            if (resolutionConstraints == null)
            {
                typeBindings = new List<ITypeBinding>(bindings);
                return true;
            }

            typeBindings = new List<ITypeBinding>();
            foreach (var binding in bindings)
            {
                if (resolutionConstraints.Accepts(binding))
                {
                    typeBindings.Add(binding);
                }
            }

            return typeBindings.Count > 0;
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

        public void ResolveAllContainer<TResolutionList>(Type type, IResolutionConstraints resolutionConstraints, List<TResolutionList> resolutions)
        {
            if (TryGetAllTypeForConstraint(type, resolutionConstraints, out var typeBindings))
            {
                foreach (var typeBinding in typeBindings)
                {
                    var resolved = ResolveBinding(typeBinding);
                    resolutions.Add((TResolutionList)resolved);
                }
            }

            if (ParentDiContainer != null)
            {
                ParentDiContainer.ResolveAllContainer<TResolutionList>(type, resolutionConstraints, resolutions);
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
