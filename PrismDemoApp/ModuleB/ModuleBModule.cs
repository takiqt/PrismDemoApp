using ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();

            regionManager.RegisterViewWithRegion("RecieverRegion", typeof(ViewB));
            regionManager.RegisterViewWithRegion("SenderRegion", typeof(Home));
            regionManager.RegisterViewWithRegion("RecieverRegion", typeof(Home));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
