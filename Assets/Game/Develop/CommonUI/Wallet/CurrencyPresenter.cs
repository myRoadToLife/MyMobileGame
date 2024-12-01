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
        private IconWithText _currencyView;

        public CurrencyPresenter(
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType,
            IconWithText currencyView,
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyView = currencyView;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _currencyView.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _currency.Changed += OnCurrencyChanged;
        }

        public void Dispose()
        {
            _currency.Changed -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _currencyView.SetText(value.ToString());
    }
}
