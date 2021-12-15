using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtBoolObjectCondition : EvtCondition
    {
        [SerializeField] private EvtBoolObject variable;
        [SerializeField] private bool target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}