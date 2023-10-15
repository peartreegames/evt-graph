using System;
using System.Collections;
using UnityEngine;
using PeartreeGames.Evt.Variables;

namespace PeartreeGames.Evt.Graph
{
    public class EvtFloatReaction : EvtReaction
    {
        public new static string DisplayName => "Variable/Float";
        [SerializeField] private EvtFloat variable;
        [SerializeField] private EvtArithmeticOperator arithOp;
        [SerializeField] private float value;

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