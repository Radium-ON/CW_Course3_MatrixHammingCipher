using HammingCorrectorModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace HammingCorrectorModule
{
    public class HammingCorrectorModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ErrorsCorrectionRegion", typeof(CorrectorView)); 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}