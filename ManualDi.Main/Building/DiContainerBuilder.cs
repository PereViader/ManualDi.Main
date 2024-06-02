using System.Collections.Generic;
using System.Linq;

namespace ManualDi.Main
{
    public sealed class DiContainerBuilder : IDiContainerBuilder
    {
        public List<InstallDelegate> InstallDelegates { get; set; } = new();
        public IDiContainer? ParentDiContainer { get; set; }

        public IDiContainerBuilder WithParentContainer(IDiContainer diContainer)
        {
            this.ParentDiContainer = diContainer;
            return this;
        }

        public IDiContainerBuilder Install(IInstaller installer)
        {
            InstallDelegates.Add(installer.Install);
            return this;
        }

        public IDiContainerBuilder Install(IEnumerable<IInstaller> installers)
        {
            IEnumerable<InstallDelegate> installActions = installers
                .Select<IInstaller, InstallDelegate>(x => x.Install);
            InstallDelegates.AddRange(installActions);
            return this;
        }

        public IDiContainerBuilder Install(InstallDelegate installDelegate)
        {
            InstallDelegates.Add(installDelegate);
            return this;
        }

        public IDiContainerBuilder Install(IEnumerable<InstallDelegate> installDelegates)
        {
            InstallDelegates.AddRange(installDelegates);
            return this;
        }

        public IDiContainer Build()
        {
            DiContainerBindings diContainerBindings = new();

            foreach (InstallDelegate installDelegate in InstallDelegates)
            {
                installDelegate.Invoke(diContainerBindings);
            }

            var diContainer = new DiContainer()
            {
                TypeBindings = diContainerBindings.TypeBindings,
                ParentDiContainer = ParentDiContainer,
            };

            foreach (var action in diContainerBindings.DisposeActions)
            {
                diContainer.QueueDispose(action);
            }

            diContainer.Init();
            
            foreach (var injectDelegate in diContainerBindings.InjectDelegates)
            {
                injectDelegate.Invoke(diContainer);
            }

            foreach (var initializationDelegate in diContainerBindings.InitializationDelegates)
            {
                initializationDelegate.Invoke(diContainer);
            }

            return diContainer;
        }
    }
}
