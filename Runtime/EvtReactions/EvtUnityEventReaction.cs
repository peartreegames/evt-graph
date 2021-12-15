using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PeartreeGames.EvtGraph
{
    public class EvtUnityEventReaction : EvtReaction
    {
        public new static string DisplayName => "UnityEvent";
        [SerializeField] private UnityEvent unityEvent;
        public override IEnumerator React(EvtTrigger trigger)
        {
            unityEvent?.Invoke();
            yield break;
        }
    }
}