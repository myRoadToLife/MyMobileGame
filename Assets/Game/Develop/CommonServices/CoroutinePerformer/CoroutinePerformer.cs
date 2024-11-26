using System.Collections;
using UnityEngine;

namespace Game.Develop.CommonServices.CoroutinePerformer
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer
    {
        private void Awake()
            => DontDestroyOnLoad(this);

        public Coroutine StartPerformer(IEnumerator coroutineFunction)
            => StartCoroutine(coroutineFunction);

        public void StopPerformer(Coroutine coroutine)
            => StopCoroutine(coroutine);
        
        
    }
    
}
