using Game.Develop.DI;
using UnityEngine;

namespace Game.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;
        
        private void Awake()
        {
            SetupAppSettings();
            DiContainer projectContainer = new DiContainer();
            
            //Регистрация сервисов на целый прект
            //Аналог global context из популярных DI фреймворков
            //Самый родительский контейнер для всех будущих
            
            //_gameBootstrap через сервис корутин запустим Run и передадим
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}
