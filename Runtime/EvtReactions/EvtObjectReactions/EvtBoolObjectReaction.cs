using System.Collections;
using UnityEngine;

namespace EvtGraph
{
    public class EvtBoolObjectReaction : EvtReaction
    {
        [SerializeField] private EvtBoolObject variable;
        [SerializeField] private bool value;
        public override IEnumerator React(EvtTrigger trigger)
        {
            variable.Value = value;
            yield break;
        }
    }
}