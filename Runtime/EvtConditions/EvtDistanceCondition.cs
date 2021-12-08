using System;
using UnityEngine;

namespace EvtGraph
{
    public class EvtDistanceCondition : EvtCondition
    {
        [SerializeField] private Transform from;
        [SerializeField] private Transform to;
        [SerializeField] private EvtComparisonOperator compOp;
        [SerializeField] private float distance;
        public override bool CheckIsSatisfied(EvtTrigger trigger)
        {
            var fromPosition = from.position;
            var toPosition = to.position;
            return compOp switch
            {
                EvtComparisonOperator.Equal => Vector3.Distance(fromPosition, toPosition) - distance < Mathf.Epsilon,
                EvtComparisonOperator.NotEqual => Vector3.Distance(fromPosition, toPosition) - distance > Mathf.Epsilon,
                EvtComparisonOperator.LessThan => Vector3.Distance(fromPosition, toPosition) < distance,
                EvtComparisonOperator.GreaterThan => Vector3.Distance(fromPosition, toPosition) > distance,
                EvtComparisonOperator.LessThanOrEqual => Vector3.Distance(fromPosition, toPosition) <= distance,
                EvtComparisonOperator.GreaterThanOrEqual => Vector3.Distance(fromPosition, toPosition) >= distance,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}