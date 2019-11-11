using CodeMatrix.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CodeMatrix
{
    public class CodeMatrixModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MatrixRegion", typeof(MatrixView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}