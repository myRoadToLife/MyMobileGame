using System;
using System.Collections;
using System.Collections.Generic;
using Game.Develop.CommonServices.DataManagement;
using Game.Develop.CommonServices.DataManagement.DataProviders;
using Game.Develop.CommonServices.SceneManagement;
using Game.Develop.CommonServices.Wallet;
using Game.Develop.DI;
using UnityEngine;

namespace Game.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DiContainer _container;

        public IEnumerator Run(DiContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            ProcessRegistration();

            yield return new WaitForSeconds(1f);
        }

        private void ProcessRegistration()
        {
            //делаем регистрации для сцены геймплея
            
            _container.Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));

            if (Input.GetKeyDown(KeyCode.F))
            {
                WalletService wallet = _container.Resolve<WalletService>();
                wallet.Add(CurrencyTypes.Gold,100);
                Debug.Log($"Деняк: {wallet.GetCurrency(CurrencyTypes.Gold).Value}");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _container.Resolve<PlayerDataProvider>().Save();
            }
        }
    }
}
