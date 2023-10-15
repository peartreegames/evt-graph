using System.Collections;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtWaitReaction : EvtReaction
    {
        public new static string DisplayName => "Time/Wait";
        [SerializeField] private float delay;
        private float _remainingDelay;
        public override IEnumerator React(EvtTrigger trigger)
        {
            _remainingDelay = delay;
            while (_remainingDelay > 0)
            {
                _remainingDelay -= Time.deltaTime;
                yield return null;
            }
        }
    }
}