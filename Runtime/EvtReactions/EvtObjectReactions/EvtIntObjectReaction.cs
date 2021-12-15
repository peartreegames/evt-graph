using System;
using System.Collections;
using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtIntObjectReaction : EvtReaction
    {
        public new static string DisplayName => "Variable/Int";
        [SerializeField] private EvtIntObject variable;
        [SerializeField] private EvtArithmeticOperator arithOp;
        [SerializeField] private int value;

        public override IEnumerator React(EvtTrigger trigger)
        {
            var result = arithOp switch
            {
                EvtArithmeticOperator.Sum => variable.Value + value,
                EvtArithmeticOperator.Subtract => variable.Value - value,
                EvtArithmeticOperator.Multiply => variable.Value * value,
                EvtArithmeticOperator.Divide => variable.Value / value,
                EvtArithmeticOperator.Modulo => variable.Value % value,
                _ => throw new ArgumentOutOfRangeException()
            };
            variable.Value = result;
            yield break;
        }
    }
}