using Game.Develop.CommonServices.AssetsManagement;
using Game.Develop.CommonServices.ConfigsManagment;
using Game.Develop.CommonServices.CoroutinePerformer;
using Game.Develop.CommonServices.DataManagement;
using Game.Develop.CommonServices.DataManagement.DataProviders;
using Game.Develop.CommonServices.LoadingScreen;
using Game.Develop.CommonServices.SceneManagement;
using Game.Develop.CommonServices.Wallet;
using Game.Develop.DI;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

namespace Game.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();
            DIContainer projectContainer = new DIContainer();

            //Регистрация сервисов на целый проект
            //Аналог global context из популярных DI фреймворков
            //Самый родительский контейнер для всех будущих

            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);

            RegisterLoadingCurtain(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);

            RegisterSaveLoadService(projectContainer);
            RegisterPlayerDataProvider(projectContainer);

            RegisterWalletService(projectContainer);

            RegisterConfigsProviderService(projectContainer);

            projectContainer.Initialize();

            //все регистрации прошли
            projectContainer.Resolve<ICoroutinePerformer>().StartPerformer(_gameBootstrap.Run(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void RegisterConfigsProviderService(DIContainer container)
            => container.RegisterAsSingle(c => new ConfigsProviderService(c.Resolve<ResourcesAssetLoader>()));

        private void RegisterWalletService(DIContainer container)
            => container.RegisterAsSingle(c => new WalletService(c.Resolve<PlayerDataProvider>())).NonLazy();


        private void RegisterPlayerDataProvider(DIContainer container)
            => container.RegisterAsSingle(c => new PlayerDataProvider(c.Resolve<ISaveLoadService>(),
                c.Resolve<ConfigsProviderService>()));

        private void RegisterSaveLoadService(DIContainer container)
            => container.RegisterAsSingle<ISaveLoadService>(c => new SaveLoadService(new JsonSerializer(), new LocalDataRepository()));

        private void RegisterSceneSwitcher(DIContainer container)
        {
            container.RegisterAsSingle(c => new SceneSwitcher(
                c.Resolve<ICoroutinePerformer>(),
                c.Resolve<ILoadingCurtain>(),
                c.Resolve<ISceneLoader>(),
                c));
        }

        private void RegisterResourcesAssetLoader(DIContainer diContainer)
            => diContainer.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer(DIContainer diContainer)
        {
            diContainer.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                CoroutinePerformer coroutinePerformerPrefab =
                    resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterLoadingCurtain(DIContainer diContainer)
        {
            diContainer.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandardLoadingCurtain standardLoadingCurtainPrefab =
                    resourcesAssetLoader.LoadResource<StandardLoadingCurtain>(InfrastructureAssetPaths.LoadingCurtainPath);

                return Instantiate(standardLoadingCurtainPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer projectContainer)
            => projectContainer.RegisterAsSingle<ISceneLoader>(c => new DefaultSceneLoader());
    }
}
