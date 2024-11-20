using UnityEngine;

namespace Game.Develop.CommonServices.AssetsManagement
{
    public class ResourcesAssetLoader
    {
        public T LoadResource <T>(string path) where T : Object
            => Resources.Load<T>(path);
    }
}
