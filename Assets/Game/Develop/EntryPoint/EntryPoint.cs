using Game.Develop.CommonServices.AssetsManagement;
using Game.Develop.CommonServices.CoroutinePerformer;
using Game.Develop.CommonServices.LoadingScreen;
using Game.Develop.CommonServices.SceneManagement;
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

            //Регистрация сервисов на целый проект
            //Аналог global context из популярных DI фреймворков
            //Самый родительский контейнер для всех будущих

            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);

            RegisterLoadingCurtain(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);

            //все регистрации прошли
            projectContainer.Resolve<ICoroutinePerformer>().StartPerformer(_gameBootstrap.Run(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void RegisterSceneSwitcher(DiContainer container)
        {
            container.RegisterAsSingle(c => new SceneSwitcher(
                c.Resolve<ICoroutinePerformer>(), 
                c.Resolve<ILoadingCurtain>(), 
                c.Resolve<ISceneLoader>(), 
                c));
        }

        private void RegisterResourcesAssetLoader(DiContainer diContainer)
            => diContainer.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer(DiContainer diContainer)
        {
            diContainer.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                CoroutinePerformer coroutinePerformerPrefab =
                    resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterLoadingCurtain(DiContainer diContainer)
        {
            diContainer.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandardLoadingCurtain standardLoadingCurtainPrefab =
                    resourcesAssetLoader.LoadResource<StandardLoadingCurtain>(InfrastructureAssetPaths.LoadingCurtainPath);

                return Instantiate(standardLoadingCurtainPrefab);
            });
        }

        private void RegisterSceneLoader(DiContainer projectContainer)
            => projectContainer.RegisterAsSingle<ISceneLoader>(c => new DefaultSceneLoader());
    }
}
