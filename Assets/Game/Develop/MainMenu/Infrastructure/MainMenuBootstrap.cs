using System;
using System.Collections;
using System.Collections.Generic;
using Game.Develop.CommonServices.AssetsManagement;
using Game.Develop.CommonServices.DataManagement;
using Game.Develop.CommonServices.DataManagement.DataProviders;
using Game.Develop.CommonServices.SceneManagement;
using Game.Develop.CommonServices.Wallet;
using Game.Develop.CommonUI.Wallet;
using Game.Develop.DI;
using Game.Develop.MainMenu.UI;
using UnityEngine;

namespace Game.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            ProcessRegistration();

            yield return new WaitForSeconds(1f);
        }

        private void ProcessRegistration()
        {
            //делаем регистрации для сцены геймплея
            _container.RegisterAsSingle(c => new WalletPresenterFactory(c));

            _container.RegisterAsSingle(c =>
            {
                MainMenuUIRoot menuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");
                return Instantiate(menuUIRootPrefab);
            }).NonLazy();


            _container
                .RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
                    .CreateCurrencyPresenter(c.Resolve<MainMenuUIRoot>()._currencyView, CurrencyTypes.Gold))
                .NonLazy();

            _container.Initialize();
        }

        private CurrencyPresenter _currencyPresenter;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                _currencyPresenter?.Dispose();
                MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();

                _currencyPresenter =
                    _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Gold);

                _currencyPresenter.Initialize();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _currencyPresenter?.Dispose();
                MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();

                _currencyPresenter =
                    _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Diamond);

                _currencyPresenter.Initialize();
            }

            if (Input.GetKeyDown(KeyCode.Space))
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));

            if (Input.GetKeyDown(KeyCode.F))
            {
                WalletService wallet = _container.Resolve<WalletService>();
                wallet.Add(CurrencyTypes.Gold, 100);
                Debug.Log($"Деняк: {wallet.GetCurrency(CurrencyTypes.Gold).Value}");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _container.Resolve<PlayerDataProvider>().Save();
            }
        }
    }
}
