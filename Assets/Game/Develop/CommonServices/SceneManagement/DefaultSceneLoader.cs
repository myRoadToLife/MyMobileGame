using System.Collections;
using Game.Develop.CommonServices.SceneManagment;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Develop.CommonServices.SceneManagement
{
    public class DefaultSceneLoader : ISceneLoader
    {
        public IEnumerator LoadAsync(SceneId sceneId, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneId.ToString(), loadSceneMode);

            while (waitLoading != null && waitLoading.isDone == false)
                yield return null;
        }
    }
}
