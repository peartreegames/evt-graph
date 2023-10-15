using System;
using System.Collections;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{

    public class EvtAnimationBoolReaction : EvtReaction
    {
        public new static string DisplayName => "Animation/Bool";
        [SerializeField] private Animator animator;
        [SerializeField] private string parameterName;
        [SerializeField] private bool active;
        public override IEnumerator React(EvtTrigger trigger)
        {
            animator.SetBool(parameterName, active);
            yield break;
        }
    }
}