using System;
using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtIntObjectCondition : EvtCondition
    {
        public new static string DisplayName => "Variable/Int";
        [SerializeField] private EvtIntObject variable;
        [SerializeField] private EvtComparisonOperator compOp;
        [SerializeField] private int target;

        public override bool CheckIsSatisfied(EvtTrigger trigger) => compOp switch
        {
            EvtComparisonOperator.Equal => variable.Value == target,
            EvtComparisonOperator.NotEqual => variable.Value != target,
            EvtComparisonOperator.LessThan => variable.Value < target,
            EvtComparisonOperator.GreaterThan => variable.Value > target,
            EvtComparisonOperator.LessThanOrEqual => variable.Value <= target,
            EvtComparisonOperator.GreaterThanOrEqual => variable.Value >= target,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}