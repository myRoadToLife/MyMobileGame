using System;
using Game.Develop.CommonServices.Wallet;
using Game.Develop.DI;
using Game.Develop.Utils.Reactive;

namespace Game.Develop.CommonUI.Wallet
{
    public class CurrencyPresenter : IInitializable, IDisposable

    {
        //Бизнес логика
        private IReadOnlyVariable<int> _currency;
        private CurrencyTypes _currencyType;
        private CurrencyIconsConfig _currencyIconsConfig;

        //Визуал
        private IconWithText _view;

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType,
            IconWithText currencyView,
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _view = currencyView;
            _currencyIconsConfig = currencyIconsConfig;
        }
        
        public IconWithText View  => _view;

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _currency.Changed += OnCurrencyChanged;
        }

        public void Dispose()
        {
            _currency.Changed -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
