using Game.Develop.CommonServices.AssetsManagement;
using Game.Develop.CommonUI.Wallet;
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

        public StartWalletConfig StartWalletConfig { get; private set; }
        public CurrencyIconsConfig CurrencyIconsConfig { get; private set; }

        public void LoadAll()
        {
            LoadStartConfig();
            LoadCurrencyIconsConfig();
        }

        private void LoadStartConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");

        private void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig = _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");
    }
}
