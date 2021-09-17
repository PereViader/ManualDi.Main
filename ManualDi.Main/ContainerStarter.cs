using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public class ContainerStarter : IContainerStarter
    {
        private readonly List<IInstaller> installers = new List<IInstaller>();
        private readonly List<Func<IDiContainer, IInstaller>> delegatedInstallers = new List<Func<IDiContainer, IInstaller>>();
        private IContainerBuilder containerBuilder;

        public IContainerStarter WithContainerBuilder(IContainerBuilder containerBuilder)
        {
            if (containerBuilder != null)
            {
                throw new InvalidOperationException("Container builder is already set");
            }

            this.containerBuilder = containerBuilder;
            return this;
        }

        public IContainerStarter WithInstaller(IInstaller installer)
        {
            installers.Add(installer);
            return this;
        }

        public IContainerStarter WithDelegatedInstaller(Func<IDiContainer, IInstaller> delegatedInstaller)
        {
            delegatedInstallers.Add(delegatedInstaller);
            return this;
        }

        public IContainerStarter WithDelegatedInstallers(IEnumerable<Func<IDiContainer, IInstaller>> delegatedInstallers)
        {
            this.delegatedInstallers.AddRange(delegatedInstallers);
            return this;
        }

        public IContainerStarter WithInstallers(IEnumerable<IInstaller> installers)
        {
            this.installers.AddRange(installers);
            return this;
        }

        public IDiContainer Start()
        {
            if (containerBuilder == null)
            {
                containerBuilder = new ContainerBuilder();
            }

            IDiContainer container = containerBuilder.Build();

            foreach (IInstaller installer in installers)
            {
                installer.Install(container);
            }

            foreach (Func<IDiContainer, IInstaller> delegatedInstaller in delegatedInstallers)
            {
                IInstaller installer = delegatedInstaller.Invoke(container);
                installer.Install(container);
            }

            container.FinishBinding();

            return container;
        }
    }
}
