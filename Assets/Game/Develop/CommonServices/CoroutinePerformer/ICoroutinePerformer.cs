using System.Collections;
using UnityEngine;

namespace Game.Develop.CommonServices.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        Coroutine StartPerformer(IEnumerator coroutineFunction);
        void StopPerformer(Coroutine coroutine);
    }
}
