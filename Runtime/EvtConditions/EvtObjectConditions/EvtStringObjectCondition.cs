using PeartreeGames.EvtVariables;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtStringObjectCondition : EvtCondition
    {
        public new static string DisplayName => "Variable/String";
        [SerializeField] private EvtStringObject variable;
        [SerializeField] private string target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}