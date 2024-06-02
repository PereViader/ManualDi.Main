using System.Collections.Generic;

namespace ManualDi.Main
{
    public interface IDiContainerBuilder
    {
        IDiContainerBuilder Install(IInstaller installer);
        IDiContainerBuilder Install(IEnumerable<IInstaller> installers);
        IDiContainerBuilder Install(InstallDelegate installDelegate);
        IDiContainerBuilder Install(IEnumerable<InstallDelegate> installDelegates);
        IDiContainerBuilder WithParentContainer(IDiContainer diContainer);
        IDiContainer Build();
    }
}
