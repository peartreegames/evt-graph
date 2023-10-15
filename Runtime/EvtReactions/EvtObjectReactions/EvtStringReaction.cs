using System.Collections;
using PeartreeGames.Evt.Variables;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtStringReaction : EvtReaction
    {
        public new static string DisplayName => "Variable/String";
        [SerializeField] private EvtString variable;
        [SerializeField] private string value;
        public override IEnumerator React(EvtTrigger trigger)
        {
            variable.Value = value;
            yield break;
        }
    }
}