using UnityEngine;
using PeartreeGames.Evt.Variables;

namespace PeartreeGames.Evt.Graph
{
    public class EvtBoolCondition : EvtCondition
    {
        public new static string DisplayName => "Variable/Bool";
        [SerializeField] private EvtBool variable;
        [SerializeField] private bool target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}