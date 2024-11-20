using UnityEngine;

namespace Game.Develop.CommonServices.LoadingScreen
{
    public class StandardLoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public bool IsShowing => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
