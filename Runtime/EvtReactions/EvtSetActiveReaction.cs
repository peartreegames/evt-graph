using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtSetActiveReaction : EvtReaction
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private bool active;
        public override IEnumerator React(EvtTrigger trigger)
        {
            gameObject.SetActive(active);
            yield break;
        }
    }
}