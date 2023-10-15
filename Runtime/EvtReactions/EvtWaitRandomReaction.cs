using System.Collections;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtWaitRandomReaction : EvtReaction
    {
        public new static string DisplayName => "Time/Wait Random";
        [SerializeField] private float minDelay;
        [SerializeField] private float maxDelay;
        private float _remainingDelay;
        public override IEnumerator React(EvtTrigger trigger)
        {
            _remainingDelay = Random.Range(minDelay, maxDelay);
            while (_remainingDelay > 0)
            {
                _remainingDelay -= Time.deltaTime;
                yield return null;
            }
        }
    }
}