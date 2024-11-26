using System.Collections;
using Game.Develop.CommonServices.SceneManagement;
using Game.Develop.DI;
using UnityEngine;

namespace Game.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DiContainer _container;

        public IEnumerator Run(DiContainer container,    GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistration();
            
            Debug.Log($"Подгружаем ресурсы для уровня {gameplayInputArgs.LevelNumber}");
            Debug.Log($"Создаем персонажа");
            Debug.Log($"Сцена готова, можно начинать играть");

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
                _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
        }
    }
}
