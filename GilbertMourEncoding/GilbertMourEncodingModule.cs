using GilbertMourEncoding.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GilbertMourEncoding
{
    public class GilbertMourEncodingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("StringEncodingRegion", typeof(GilbertView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}