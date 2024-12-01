using Game.Develop.CommonServices.ConfigsManagment;
using Game.Develop.CommonServices.Wallet;
using Game.Develop.DI;

namespace Game.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, view, _configsProviderService.CurrencyIconsConfig);
    }
}
