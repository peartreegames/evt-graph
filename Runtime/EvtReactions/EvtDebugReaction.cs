using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtDebugReaction : EvtReaction
    {
        public new static string DisplayName => "Debug/Log";
        [SerializeField] private string message;
        public override IEnumerator React(EvtTrigger trigger)
        {
            Debug.Log(message);
            yield break;
        }
    }
}