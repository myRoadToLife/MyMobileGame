using System.Collections;
using Game.Develop.CommonServices.SceneManagment;
using UnityEngine.SceneManagement;

namespace Game.Develop.CommonServices.SceneManagement
{
    public interface ISceneLoader
    {
        IEnumerator LoadAsync(SceneId sceneId, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    }
}
