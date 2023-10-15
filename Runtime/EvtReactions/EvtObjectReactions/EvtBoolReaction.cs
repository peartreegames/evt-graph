using System.Collections;
using UnityEngine;
using PeartreeGames.Evt.Variables;

namespace PeartreeGames.Evt.Graph
{
    public class EvtBoolReaction : EvtReaction
    {
        public new static string DisplayName => "Variable/Bool";
        [SerializeField] private EvtBool variable;
        [SerializeField] private bool value;
        public override IEnumerator React(EvtTrigger trigger)
        {
            variable.Value = value;
            yield break;
        }
    }
}