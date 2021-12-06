using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IDiContainerBuilder
    {
        IDiContainerBuilder WithContainerFactory(IDiContainerFactory containerBuilder);
        IDiContainerBuilder WithInstaller(IInstaller installer);
        IDiContainerBuilder WithInstallers(IEnumerable<IInstaller> installers);
        IDiContainerBuilder WithInstallDelegate(InstallDelegate installDelegate);
        IDiContainerBuilder WithInstallDelegates(IEnumerable<InstallDelegate> installDelegates);
        IDiContainerBuilder WithParentContainer(IDiContainer diContainer);
        IDiContainer Build();
    }
}
