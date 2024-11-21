using System;
using System.Collections;
using Game.Develop.CommonServices.SceneManagement;
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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
        }
    }
}
