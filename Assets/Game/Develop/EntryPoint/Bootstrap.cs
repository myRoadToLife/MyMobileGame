using System.Collections;
using Game.Develop.CommonServices.ConfigsManagment;
using Game.Develop.CommonServices.DataManagement.DataProviders;
using Game.Develop.CommonServices.LoadingScreen;
using Game.Develop.CommonServices.SceneManagement;
using Game.Develop.DI;
using UnityEngine;

namespace Game.Develop.EntryPoint
{
    //Если Entry Point это глобальные регистрации для старта проекта
    //То, Bootstrap уже для инициализации начала работ
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurtain loadingCurtain = container.Resolve<ILoadingCurtain>();
            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();
            loadingCurtain.Show();

            Debug.Log("Начинается инициализация");

            //Инициализация всех сервисов. Подгрузка данных и конфигов(реклама и аналитика)
            container.Resolve<ConfigsProviderService>().LoadAll();
            
            container.Resolve<PlayerDataProvider>().Load();

            yield return new WaitForSeconds(1.5f);

            Debug.Log("Заканчивается инициализация");

            loadingCurtain.Hide();
            //Переход на следующую сцену с помощью сервиса смены сцен
            sceneSwitcher.ProcessSwitchSceneFor(new OutputBootstrapArgs(new MainMenuInputArgs()));
        }
    }
}
