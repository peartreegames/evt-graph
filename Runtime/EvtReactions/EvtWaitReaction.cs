using System.Collections;
using UnityEngine;

namespace EvtGraph
{
    public class EvtWaitReaction : EvtReaction
    {
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