using System;
using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtFloatObjectCondition : EvtCondition
    {
        [SerializeField] private EvtFloatObject variable;
        [SerializeField] private EvtComparisonOperator compOp;
        [SerializeField] private float target;

        public override bool CheckIsSatisfied(EvtTrigger trigger) => compOp switch
        {
            EvtComparisonOperator.Equal => Math.Abs(variable.Value - target) < Mathf.Epsilon,
            EvtComparisonOperator.NotEqual => Math.Abs(variable.Value - target) > Mathf.Epsilon,
            EvtComparisonOperator.LessThan => variable.Value < target,
            EvtComparisonOperator.GreaterThan => variable.Value > target,
            EvtComparisonOperator.LessThanOrEqual => variable.Value <= target,
            EvtComparisonOperator.GreaterThanOrEqual => variable.Value >= target,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}