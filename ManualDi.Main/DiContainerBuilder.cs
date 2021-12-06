using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualDi.Main
{
    public class DiContainerBuilder : IDiContainerBuilder
    {
        private readonly List<InstallDelegate> installDelegates = new List<InstallDelegate>();
        private IDiContainerFactory containerFactory;
        private IDiContainer parentDiContainer;

        public IDiContainerBuilder WithParentContainer(IDiContainer diContainer)
        {
            this.parentDiContainer = diContainer;
            return this;
        }

        public IDiContainerBuilder WithContainerFactory(IDiContainerFactory containerBuilder)
        {
            if (containerBuilder != null)
            {
                throw new InvalidOperationException("Container builder is already set");
            }

            this.containerFactory = containerBuilder;
            return this;
        }

        public IDiContainerBuilder WithInstaller(IInstaller installer)
        {
            installDelegates.Add(installer.Install);
            return this;
        }

        public IDiContainerBuilder WithInstallers(IEnumerable<IInstaller> installers)
        {
            IEnumerable<InstallDelegate> installActions = installers
                .Select<IInstaller, InstallDelegate>(x => x.Install);
            this.installDelegates.AddRange(installActions);
            return this;
        }

        public IDiContainerBuilder WithInstallDelegate(InstallDelegate installDelegate)
        {
            installDelegates.Add(installDelegate);
            return this;
        }

        public IDiContainerBuilder WithInstallDelegates(IEnumerable<InstallDelegate> installDelegates)
        {
            this.installDelegates.AddRange(installDelegates);
            return this;
        }

        public IDiContainer Build()
        {
            if (containerFactory == null)
            {
                containerFactory = new DiContainerFactory();
            }

            var diContainerBindings = new DiContainerBindings();

            foreach (InstallDelegate installDelegate in installDelegates)
            {
                installDelegate.Invoke(diContainerBindings);
            }

            return containerFactory.Create(diContainerBindings, parentDiContainer);
        }
    }
}
