using System.Collections.Generic;

namespace ManualDi.Main
{
    public class InstallDelegateInstaller : IInstaller
    {
        private readonly List<InstallDelegate> installDelegates = new List<InstallDelegate>();

        public void Add(InstallDelegate installDelegate)
        {
            installDelegates.Add(installDelegate);
        }

        public void Install(IDiContainerBindings diContainerBindings)
        {
            foreach (var installDelegate in installDelegates)
            {
                installDelegate.Invoke(diContainerBindings);
            }
        }
    }
}
