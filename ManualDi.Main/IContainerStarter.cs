using System;
using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IContainerStarter
    {
        IContainerStarter WithContainerBuilder(IContainerBuilder containerBuilder);
        IContainerStarter WithInstaller(IInstaller installer);
        IContainerStarter WithInstallers(IEnumerable<IInstaller> installers);
        IContainerStarter WithDelegatedInstallers(IEnumerable<Func<IDiContainer, IInstaller>> delegatedInstallers);
        IContainerStarter WithDelegatedInstaller(Func<IDiContainer, IInstaller> delegatedInstaller);
        IDiContainer Start();
    }
}
