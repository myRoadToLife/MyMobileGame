using Game.Develop.CommonServices.AssetsManagement;
using Game.Develop.Configs.Common.Wallet;

namespace Game.Develop.CommonServices.ConfigsManagment
{
    public class ConfigsProviderService
    {
        private ResourcesAssetLoader _resourcesAssetLoader;

        public ConfigsProviderService(ResourcesAssetLoader resourcesAssetLoader)
        {
            _resourcesAssetLoader = resourcesAssetLoader;
        }
        
        public StartWalletConfig StartWalletConfig  { get; set; }

        public void LoadAll()
        {
            LoadStartConfig();
        }
        
        private void LoadStartConfig()
        => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");
    }
}
