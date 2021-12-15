using System.Collections;
using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtStringObjectReaction : EvtReaction
    {
        [SerializeField] private EvtStringObject variable;
        [SerializeField] private string value;
        public override IEnumerator React(EvtTrigger trigger)
        {
            variable.Value = value;
            yield break;
        }
    }
}