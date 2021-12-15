using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtStringObjectCondition : EvtCondition
    {
        [SerializeField] private EvtStringObject variable;
        [SerializeField] private string target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}