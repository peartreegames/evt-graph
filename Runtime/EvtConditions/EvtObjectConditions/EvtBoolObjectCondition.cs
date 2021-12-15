using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtBoolObjectCondition : EvtCondition
    {
        public new static string DisplayName => "Variable/Bool";
        [SerializeField] private EvtBoolObject variable;
        [SerializeField] private bool target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}