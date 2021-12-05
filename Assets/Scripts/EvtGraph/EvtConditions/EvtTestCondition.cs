using UnityEngine;

namespace EvtGraph
{
    public class EvtTestCondition : EvtCondition
    {
        [SerializeField] private bool value;
        public override bool CheckIsSatisfied(EvtTrigger trigger)
        {
            return value;
        }
    }
}