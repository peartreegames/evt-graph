using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PeartreeGames.EvtGraph
{
    public class EvtUnityEventReaction : EvtReaction
    {
        [SerializeField] private UnityEvent unityEvent;
        public override IEnumerator React(EvtTrigger trigger)
        {
            unityEvent?.Invoke();
            yield break;
        }
    }
}