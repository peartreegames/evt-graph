using PeartreeGames.Evt.Variables;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtStringCondition : EvtCondition
    {
        public new static string DisplayName => "Variable/String";
        [SerializeField] private EvtString variable;
        [SerializeField] private string target;
        public override bool CheckIsSatisfied(EvtTrigger trigger) => variable.Value == target;
    }
}