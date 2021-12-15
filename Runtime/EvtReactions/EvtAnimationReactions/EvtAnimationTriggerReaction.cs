using System;
using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{

    public class EvtAnimationTriggerReaction : EvtReaction
    {
        public new static string DisplayName => "Animation/Trigger";
        [SerializeField] private Animator animator;
        [SerializeField] private string parameterName;
        public override IEnumerator React(EvtTrigger trigger)
        {
            animator.SetTrigger(parameterName);
            yield break;
        }
    }
}