using System;
using System.Collections;
using Game.Develop.CommonServices.CoroutinePerformer;
using Game.Develop.CommonServices.LoadingScreen;
using Game.Develop.CommonServices.SceneManagment;
using Game.Develop.DI;
using Game.Develop.Gameplay.Infrastructure;
using Game.Develop.MainMenu.Infrastructure;
using Object = UnityEngine.Object;

namespace Game.Develop.CommonServices.SceneManagement
{
    public class SceneSwitcher
    {
        private const string ErrorSceneTransitionMessage = "Данный переход невозможен";

        private readonly DIContainer _projectContainer;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;

        private DIContainer _currentContainer;

        public SceneSwitcher(ICoroutinePerformer coroutinePerformer, ILoadingCurtain loadingCurtain, ISceneLoader sceneLoader, DIContainer projectContainer)
        {
            _coroutinePerformer = coroutinePerformer;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _projectContainer = projectContainer;
        }

        public void ProcessSwitchSceneFor(IOutputSceneArgs outputSceneArgs)
        {
            switch (outputSceneArgs)
            {
                case OutputBootstrapArgs outputBootstrapArgs:
                    _coroutinePerformer.StartPerformer(ProcessSwitchFromBootstrapScene(outputBootstrapArgs));
                    break;
                case OutputMainMenuArgs outputMainMenuArgs:
                    _coroutinePerformer.StartPerformer(ProcessSwitchFromMainMenuScene(outputMainMenuArgs));
                    break;
                case OutputGameplayArgs outputGameplayArgs:
                    _coroutinePerformer.StartPerformer(ProcessSwitchFromGameplayScene(outputGameplayArgs));
                    break;
                default:
                    throw new ArgumentException(nameof(outputSceneArgs));
            }
        }

        private IEnumerator ProcessSwitchFromBootstrapScene(OutputBootstrapArgs outputBootstrapArgs)
        {
            switch (outputBootstrapArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;
                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromMainMenuScene(OutputMainMenuArgs outputMainMenuArgs)
        {
            switch (outputMainMenuArgs.NextSceneInputArgs)
            {
                case GameplayInputArgs gameplayInputArgs:
                    yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;
                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchFromGameplayScene(OutputGameplayArgs outputGameplayArgs)
        {
            switch (outputGameplayArgs.NextSceneInputArgs)
            {
                case MainMenuInputArgs mainMenuInputArgs:
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;
                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchToMainMenuScene(MainMenuInputArgs mainMenuInputArgs)
        {
            _loadingCurtain.Show();

            _currentContainer?.Dispose();

            yield return _sceneLoader.LoadAsync(SceneId.Empty);
            yield return _sceneLoader.LoadAsync(SceneId.MainMenu);

            MainMenuBootstrap mainMenuBootstrap = Object.FindAnyObjectByType<MainMenuBootstrap>();

            if (mainMenuBootstrap == null)
                throw new NullReferenceException(nameof(MainMenuInputArgs));

            _currentContainer = new DIContainer(_projectContainer);

            yield return mainMenuBootstrap.Run(_currentContainer, mainMenuInputArgs);

            _loadingCurtain.Hide();
        }

        private IEnumerator ProcessSwitchToGameplayScene(GameplayInputArgs gameplayInputArgs)
        {
            _loadingCurtain.Show();

            _currentContainer?.Dispose();

            yield return _sceneLoader.LoadAsync(SceneId.Empty);
            yield return _sceneLoader.LoadAsync(SceneId.Gameplay);

            GameplayBootstrap gameplayBootstrap = Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(MainMenuInputArgs));

            _currentContainer = new DIContainer(_projectContainer);

            yield return gameplayBootstrap.Run(_currentContainer, gameplayInputArgs);

            _loadingCurtain.Hide();
        }
    }
}
