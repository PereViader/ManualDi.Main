using ManualDi.Main.Disposing;
using ManualDi.Main.Initialization;
using ManualDi.Main.Injection;
using ManualDi.Main.TypeResolvers;

namespace ManualDi.Main
{
    public class ContainerBuilder : IContainerBuilder
    {
        private IDiContainer parentDiContainer;

        public IContainerBuilder WithParentContainer(IDiContainer diContainer)
        {
            this.parentDiContainer = diContainer;
            return this;
        }

        public IDiContainer Build()
        {
            var container = new DiContainer();

            container.ParentDiContainer = parentDiContainer;

            container.TypeResolvers.Add(new SingleTypeResolver());
            container.TypeResolvers.Add(new TransientTypeResolver());

            container.BindingInjector = new BindingInjector();
            container.BindingInitializer = new BindingInitializer();
            container.BindingDisposer = new BindingDisposer();

            return container;
        }
    }
}
