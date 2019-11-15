using HammingCorrector.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace HammingCorrector
{
    public class HammingCorrectorModule : IModule
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